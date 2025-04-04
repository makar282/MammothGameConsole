using MammothHunting.Models;
using MammothHunting.Views;
using System.Diagnostics;

namespace MammothHunting.Controllers
{
	public class GameController
	{
		private GameModel _gameModel;
		private Direction _currentDirection;
		private const int FrameDelay = 150;

		public GameController()
		{
			_currentDirection = Direction.Right; // Начальное направление движения
		}

		public void StartGame()
		{
			_gameModel = GameModelFactory.Create(); // Используем фабрику для создания модели игры
			Console.CursorVisible = false; // Скрываем курсор

			var frameStopwatch = new Stopwatch();
			GameView.DrawBorder(); // Рисуем границу игрового поля

			while (!_gameModel.IsGameOver)
			{
				frameStopwatch.Restart();

				try
				{
					_currentDirection = ReadMovement(_currentDirection); // Читаем направление движения
					bool throwSpear = CheckThrowInput(); // Проверяем ввод для броска копья
					_gameModel.Update(_currentDirection, throwSpear); // Обновляем состояние игры

					while (frameStopwatch.ElapsedMilliseconds < FrameDelay)
					{
						Thread.Sleep(10); // Задержка для обеспечения частоты кадров
					}
				}
				catch (Exception ex)
				{
					Console.Clear();
					Console.WriteLine($"Ошибка: {ex.Message}");
					Console.ReadKey();
					break;
				}
			}

			if (_gameModel.IsGameOver)
			{
				EndGame(); // Завершаем игру
			}
		}

		private bool CheckThrowInput()
		{
			if (!Console.KeyAvailable) return false;
			var key = Console.ReadKey(true).Key;
			if (key == ConsoleKey.Escape)
			{
				MainMenu.Show(); // Показываем главное меню при нажатии Escape
				return false;
			}
			return key == ConsoleKey.Spacebar; // Бросок копья при нажатии пробела
		}

		private void EndGame()
		{
			Console.Clear();
			Console.SetCursorPosition(20, 15);
			Console.WriteLine($"Game Over! Score: {_gameModel.Score}"); // Выводим результат игры
			Console.ReadKey();
			User.Instance.SaveScore(_gameModel.Score); // Сохраняем результат с именем из User
			MainMenu.Show(); // Показываем главное меню
		}

		private Direction ReadMovement(Direction currentDirection)
		{
			if (!Console.KeyAvailable) return currentDirection;
			var key = Console.ReadKey(true).Key;
			return key switch
			{
				ConsoleKey.UpArrow => Direction.Up,
				ConsoleKey.DownArrow => Direction.Down,
				ConsoleKey.LeftArrow => Direction.Left,
				ConsoleKey.RightArrow => Direction.Right,
				_ => currentDirection
			};
		}
	}
}