using System.Threading;
using MammothHunting.Models;
using MammothHunting.Views;

namespace MammothHunting.Controllers
{
	public class ThrowingTheSpearController
	{
		private readonly ThrowingTheSpearModel _model;
		private readonly ThrowingTheSpearView _view;
		private Thread _movementThread;
		private CancellationTokenSource _cts;
		private const int MaxPositionUpdates = 10; // Максимальное количество обновлений позиции

		public ThrowingTheSpearController(ThrowingTheSpearModel model, ThrowingTheSpearView view)
		{
			_model = model;
			_view = view;
			_cts = new CancellationTokenSource();
		}

		public void Throw(Direction direction)
		{
			if (_model.IsThrown) return;

			StopMovement();

			_model.Throw(direction);

			_cts = new CancellationTokenSource();
			var token = _cts.Token;
			int updateCount = 0; // Счетчик обновлений позиции

			_movementThread = new Thread(() =>
			{
				while (!token.IsCancellationRequested &&
					   _model.IsThrown &&
					   !_model.IsTargetHit &&
					   updateCount < MaxPositionUpdates) // Добавляем условие по количеству обновлений
				{
					if (token.IsCancellationRequested)
						break;

					_model.UpdatePosition();
					_model.Position.Draw();
					updateCount++; // Увеличиваем счетчик

					Thread.Sleep(30);
				}

				// После достижения максимума обновлений сбрасываем копье
				if (updateCount >= MaxPositionUpdates)
				{
					_model.ResetSpear();
				}
			});

			_movementThread.IsBackground = true;
			_movementThread.Start();
		}

		public void StopMovement()
		{
			_cts?.Cancel();

			if (_movementThread != null && _movementThread.IsAlive)
			{
				if (!_movementThread.Join(100))
				{
					_movementThread.Interrupt();
				}
			}
		}

		public bool IsThrown => _model.IsThrown;

		public void Dispose()
		{
			StopMovement();
			_cts?.Dispose();
		}
	}
}