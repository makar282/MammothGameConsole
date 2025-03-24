using MammothHunting.Models;
using MammothHunting.Views;
using System.Diagnostics;

namespace MammothHunting.Controllers
{
	// GameController.cs
	public class GameController
	{
		private GameModel _gameModel;
		private Direction _currentDirection;
		private const int FrameDelay = 150;

		public GameController()
		{
			_currentDirection = Direction.Right;
		}

		public void StartGame()
		{
			_gameModel = new GameModel();
			Console.CursorVisible = false;

			var frameStopwatch = new Stopwatch();

			while (!_gameModel.IsGameOver)
			{
				frameStopwatch.Restart();

				try
				{
					// Обработка ввода
					_currentDirection = ReadMovement(_currentDirection);

					// Обновление состояния
					_gameModel.Update(_currentDirection, CheckThrowInput());

					// Отрисовка
					GameView.ClearGameArea();
					GameView.DrawBorder();
					GameView.DrawGameObjects(_gameModel.Hunter, _gameModel.Mammoth);

					// Задержка для стабильного FPS
					while (frameStopwatch.ElapsedMilliseconds < FrameDelay)
					{
						Thread.Sleep(10);
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
				EndGame();
			}
		}

		private bool CheckThrowInput()
		{
			if (!Console.KeyAvailable) return false;

			var key = Console.ReadKey(true).Key;
			if (key == ConsoleKey.Escape)
			{
				MainMenu.Show();
				return false;
			}

			return key == ConsoleKey.Spacebar;
		}

		private void EndGame()
		{
			Console.Clear();
			Console.SetCursorPosition(20, 15);
			Console.WriteLine($"Game Over! Score: {_gameModel.Score}");
			Console.WriteLine("Нажмите любую клавишу...");
			Console.ReadKey();
			MainMenu.Show();
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