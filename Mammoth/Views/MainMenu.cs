using MammothHunting.Controllers;
using static System.Console;

namespace MammothHunting.Views
{
	public class MainMenu
	{
		// Размер экрана
		public const int ScreenWidth = 80;
		public const int ScreenHeight = 50;

		static Player player = new Player();
		static HighScoresMenu highScoresMenu = new HighScoresMenu();

		static void Main(string[] args)
		{
			SetCursorPosition(0, 0);
			SetConsoleSize(ScreenWidth, ScreenHeight);
			// Устанавливаем размер окна и буфера один раз при запуске программы
			//Console.SetBufferSize(ScreenWidth, ScreenHeight);
			//Console.SetWindowSize(ScreenWidth, ScreenHeight);
			Show();
		}

		public static void Show()
		{
			while (true)
			{
				RedrawMainMenu();

				ConsoleKeyInfo keyInfo = ReadKey(); // true скрывает символ
				switch (keyInfo.Key)
				{
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						StartGame();
						break;
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						ShowPlayer();
						break;
					case ConsoleKey.D3:
					case ConsoleKey.NumPad3:
						ShowHighScores();
						break;
					case ConsoleKey.D4:
					case ConsoleKey.NumPad4:
						ShowHelp();
						break;
					case ConsoleKey.D5:
					case ConsoleKey.NumPad5:
						Exit();
						break;
					default:
						SetCursorPosition(0, 9); // Сообщение выводится всегда в одном месте
						WriteLine("Неверный выбор. Попробуйте снова.");
						Thread.Sleep(1000);
						break;
				}
			}
		}

		private static void SetConsoleSize(int width, int height)
		{
			try
			{
				Console.SetBufferSize(width, height); // Устанавливаем размер буфера
				Console.SetWindowSize(width, height); // Устанавливаем размер окна
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Не удалось установить размер консоли: {ex.Message}");
			}
		}

		static void StartGame()
		{
			SetCursorPosition(0, 0); // Устанавливаем курсор в начало
			Clear();
			// Запускаем новую игру
			var gameController = new GameController();
			gameController.StartGame();
		}

		static void ShowPlayer()
		{
			//SetCursorPosition(0, 0); // Устанавливаем курсор в начало
			//Clear();
			player.SetName();
		}

		static void ShowHighScores()
		{
			SetCursorPosition(0, 0); // Устанавливаем курсор в начало
			Clear();
			highScoresMenu.SetHighScores();
		}

		static void ShowHelp()
		{
			SetCursorPosition(0, 0); // Устанавливаем курсор в начало
			Clear();
			Help.Show();
		}

		static void Exit()
		{
			Clear();
			WriteLine("Выход из игры...");
			Thread.Sleep(1000);
			Environment.Exit(0);
		}

		static void RedrawMainMenu()
		{
			SetCursorPosition(0, 0);
			Clear();
			WriteLine("=== Главное меню ===");
			WriteLine("1. Начать игру");
			WriteLine("2. Игрок");
			WriteLine("3. Рекорды");
			WriteLine("4. Справка");
			WriteLine("5. Выход");
			WriteLine("====================");
			Write("Выберите опцию: ");
		}
	}
}