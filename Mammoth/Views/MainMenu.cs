using System;
using static System.Console;
using System.Threading;
using Mammoth.Controllers;

namespace Mammoth.Views
{
	class MainMenu
	{
		// размер экрана
		private const int ScreenWidth = 55;
		private const int ScreenHeight = 55;
		static Player player = new Player();
		static HighScoresMenu HighScoresMenu = new HighScoresMenu();

		static void Main(string[] args)
		{
			Show();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
		public static void Show()
		{
			Console.SetWindowSize(ScreenWidth, ScreenHeight);
			Console.SetBufferSize(ScreenWidth, ScreenHeight);

			while (true)
			{
				Clear();
				RedrawMainMenu(); // Теперь вызываем обновление меню

				ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true скрывает символ
				switch (keyInfo.Key)
				{
					case ConsoleKey.D1:
					case ConsoleKey.NumPad1:
						StartGame();
						break;
					case ConsoleKey.D2:
					case ConsoleKey.NumPad2:
						ShowPlayerInfo();
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
						Console.SetCursorPosition(0, 9); // Сообщение выводится всегда в одном месте
						Console.WriteLine("Неверный выбор. Попробуйте снова.");
						Thread.Sleep(1000);
						break;
				}
			}
		}

		static void Clear()
		{
			Console.Clear();
		}

		static void StartGame()
		{
			Clear();
			Console.SetCursorPosition(0, 0);
			Game.StartGame();
		}

		static void ShowPlayerInfo()
		{
			Console.SetCursorPosition(0, 0);
			player.SetName();
		}

		static void ShowHighScores()
		{
			Console.SetCursorPosition(0, 0);
			HighScoresMenu.SetHighScores();
		}

		static void ShowHelp()
		{
			Help.Show();
		}

		static void Exit()
		{
			Console.Clear();
			WriteLine("Выход из игры...");
			Thread.Sleep(1000);
			Environment.Exit(0);
		}

		static void RedrawMainMenu()
		{
			Console.SetCursorPosition(0, 0); // Ставим курсор в левый верхний угол
			Console.WriteLine("=== Главное меню ===     ");
			Console.WriteLine("1. Начать игру           ");
			Console.WriteLine("2. Игрок                 ");
			Console.WriteLine("3. Рекорды               ");
			Console.WriteLine("4. Справка               ");
			Console.WriteLine("5. Выход                 ");
			Console.WriteLine("====================      ");
			Console.Write("Выберите опцию:          ");
			Console.SetCursorPosition(16, 7); // Курсор ставится в точное место
		}

	}
}