using System;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Console;
using Mammoth.Models;
using Mammoth.Views;
using MammothWPF.Models;

namespace Mammoth.Controllers
{
	/// <summary>
	/// Класс программы
	/// </summary>
	public class Game
	{
		// размер игровой карты
		private const int MapWidth = 51;
		private const int MapHeight = 31;

		// размер экрана
		private const int ScreenWidth = 55;
		private const int ScreenHeight = 35;


		// цвета
		private const ConsoleColor BorderColor = ConsoleColor.DarkGreen;
		private const ConsoleColor HeadColor = ConsoleColor.DarkYellow;
		private const ConsoleColor BodyColor = ConsoleColor.White;
		private const ConsoleColor SpearColor = ConsoleColor.DarkGray;

		// время между кадрами
		private const int FrameMs = 150;

		// максимальный счет
		private const int MaxScore = 1000;

		// счет
		private static int score = MaxScore;


		/// <summary>
		/// Метод для запуска игры
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
		public static void StartGame()
		{
			Console.SetWindowSize(ScreenWidth, ScreenHeight);
			Console.SetBufferSize(ScreenWidth, ScreenHeight);
			bool isGameOver = false;

			while (!isGameOver)
			{
				try
				{
					Clear();
					Console.SetCursorPosition(1, 1);

					DrawBorder();

					Direction currentDirection = Direction.Right;
					var hunter = new Hunter(10, 5, HeadColor, BodyColor, SpearColor, MapWidth, MapHeight);
					var mammoth = new Models.Mammoth(20, 20, mammothColor: ConsoleColor.DarkGray, tuskColor: ConsoleColor.White);
					var mammothMovement = new MammothMovement(MapWidth, MapHeight);
					var throwingTheSpear = new ThrowingTheSpear(hunter, mammoth, () => EndGame());

					var stopwatch = new Stopwatch();
					stopwatch.Start();
					Console.CursorVisible = false;

					while (true)
					{
						var frameStopwatch = new Stopwatch();
						frameStopwatch.Start();

						while (frameStopwatch.ElapsedMilliseconds < FrameMs)
						{
							currentDirection = ReadMovement(currentDirection);

							if (KeyAvailable)
							{
								var key = ReadKey(intercept: true).Key;
								if (key == ConsoleKey.Spacebar)
								{
									throwingTheSpear.Throw(currentDirection);
								}
								else if (key == ConsoleKey.Escape)
								{
									Clear();
									MainMenu.Show();
									return;
								}
							}
						}
						ClearGameArea();
						hunter.Move(currentDirection);
						mammothMovement.MoveMammoth(mammoth);

						int elapsedTime = (int)stopwatch.Elapsed.TotalSeconds;
						if (elapsedTime > 30)
						{
							score = MaxScore - (elapsedTime - 30) * 10;
						}
						else
						{
							score = MaxScore - elapsedTime * 10;
						}

						// Проверка выхода за границы
						if (hunter.Head.X <= 1 || hunter.Head.X > MapWidth - 2 ||
								  hunter.Head.Y <= 1 || hunter.Head.Y > MapHeight - 1 ||
								  ThrowingTheSpear.isGameOver)
						{
							isGameOver = true;
							ClearGameArea();
							Console.Clear();
							hunter.Clear();
							mammoth.Clear();
							EndGame();
							break;
						}
					}
				}
				catch (Exception ex)
				{
					Clear();
					WriteLine($"Произошла ошибка: {ex.Message}");
					WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");
					ReadKey();
					MainMenu.Show();
				}
			}
		}

		/// <summary>
		/// Метод для завершения игры
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>")]
		public static void EndGame()
		{
			Clear();
			SetCursorPosition(ScreenWidth / 2 - 10, ScreenHeight / 2);
			WriteLine($"Game Over! Your score: {score}");

			// Сохранение рекорда
			var records = RecordsToFile.ImportJSON();
			records.Add(new Record(User.Instance.Name, score));
			RecordsToFile.ExportJSON(records);

			// Звук конца игры
			Task.Run(() => Beep(200, 600));
			WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");
			ReadKey();

			MainMenu.Show();

			return; // Останавливаем выполнение метода сразу!
		}


		/// <summary>
		/// Метод для чтения направления движения
		/// </summary>
		public static Direction ReadMovement(Direction currentDirection)
		{
			// если нажата клавиша, то возвращаем направление движения
			if (!KeyAvailable)
			{
				return currentDirection;
			}

			// иначе считываем клавишу
			ConsoleKey key = ReadKey(intercept: true).Key;

			// выбор стороны
			currentDirection = key switch
			{
				ConsoleKey.UpArrow => Direction.Up,
				ConsoleKey.DownArrow => Direction.Down,
				ConsoleKey.LeftArrow => Direction.Left,
				ConsoleKey.RightArrow => Direction.Right,
				_ => currentDirection,
			};

			return currentDirection;
		}

		/// <summary>
		/// Метод для рисования границ
		/// </summary>
		public static void DrawBorder()
		{
			for (int i = 1; i < MapWidth; i++) // Было MapWidth - 1
			{
				new Pixel(i, 1, BorderColor).Draw();
				new Pixel(i, MapHeight - 1, BorderColor).Draw(); // Было MapHeight - 2
			}

			for (int i = 1; i < MapHeight; i++) // Было MapHeight - 1
			{
				new Pixel(1, i, BorderColor).Draw();
				new Pixel(MapWidth - 1, i, BorderColor).Draw();
			}
		}

		/// <summary>
		/// Очищает игровую область, не затрагивая границы
		/// </summary>
		public static void ClearGameArea()
		{
			for (int y = 2; y < MapHeight - 1; y++) // Начинаем с 2, чтобы не стереть верхнюю границу
			{
				SetCursorPosition(2, y);
				Write(new string(' ', MapWidth - 3)); // Заполняем пробелами внутреннюю область
			}
		}

	}
}