using System;
using System.Threading.Tasks;
using Mammoth.Models;

namespace Mammoth.Controllers
{
	/// <summary>
	/// Класс бросания копья
	/// </summary>
	public class ThrowingTheSpear
	{
		private readonly Hunter _hunter;
		private readonly Models.Mammoth _mammoth;
		private Pixel _spear;
		private Direction _direction;
		private bool _isThrown;
		private Action _onHit;
		public static bool isGameOver = false;

		public ThrowingTheSpear(Hunter hunter, Models.Mammoth mammoth, Action onHit)
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

		// Метод движения копья
		private async Task MoveSpear()
		{
			while (_isThrown)
			{
				_spear.Clear();

				// Перемещаем копьё в зависимости от направления
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
					_isThrown = false;
					_spear.Clear();
					isGameOver = true;
					_onHit?.Invoke();
					return;
				}

				// Проверка на выход за границы карты
				if (_spear.X < 0 || _spear.X >= _hunter.MapWidth || _spear.Y < 0 || _spear.Y >= _hunter.MapHeight)
				{
					_isThrown = false;
					_spear.Clear();
				}

				await Task.Delay(100);
			}
		}
	}
}