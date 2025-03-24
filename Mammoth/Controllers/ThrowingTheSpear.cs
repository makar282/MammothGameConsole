using MammothHunting.Models;

namespace MammothHunting.Controllers
{
	// Класс бросания копья
	public class ThrowingTheSpear
	{
		private readonly Hunter _hunter;
		private readonly Mammoth _mammoth;
		private Pixel _spear;
		private Direction _direction;
		private bool _isThrown;
		private Action _onHit;
		public bool IsTargetHit { get; private set; }

		public ThrowingTheSpear(Hunter hunter, Mammoth mammoth, Action onHit)
		{
			_hunter = hunter;
			_mammoth = mammoth;
			_spear = hunter.Spear;
			_isThrown = false;
			_onHit = onHit;
		}

		public void Throw(Direction direction)
		{
			_isThrown = true;
			_direction = direction;
			_spear = _hunter.Spear;

			Task.Run(MoveSpear);
		}

		private async Task MoveSpear()
		{
			while (_isThrown)
			{
				_spear.Clear();

				// Перемещаем копьё
				_spear = _direction switch
				{
					Direction.Up => new Pixel(_spear.X, _spear.Y - 1, _spear.Color),
					Direction.Down => new Pixel(_spear.X, _spear.Y + 1, _spear.Color),
					Direction.Left => new Pixel(_spear.X - 1, _spear.Y, _spear.Color),
					Direction.Right => new Pixel(_spear.X + 1, _spear.Y, _spear.Color),
					_ => _spear
				};

				_spear.Draw();

				// Проверка на попадание в мамонта
				if (_mammoth.Head.X == _spear.X && _mammoth.Head.Y == _spear.Y ||
					_mammoth.Body.Exists(pixel => pixel.X == _spear.X && pixel.Y == _spear.Y) ||
					_mammoth.Tusk.Exists(pixel => pixel.X == _spear.X && pixel.Y == _spear.Y))
				{
					IsTargetHit = true;	
					_spear.Clear();
					//_onHit?.Invoke();  // Теперь `onHit` изменит `GameModel.IsGameOver`
					return;
				}

				await Task.Delay(100);
			}
		}
	}

}