using MammothHunting.Controllers;
using MammothHunting.Views;
using MammothHunting.Models;
using System.Diagnostics;

namespace MammothHunting.Models
{
	public class GameModel
	{
		// Размеры карты
		public const int MapWidth = 70;
		public const int MapHeight = 30;

		// Игровые объекты
		public Hunter Hunter { get; }
		public Mammoth Mammoth { get; }
		public int Score { get; private set; } = 1000; // Начальный счет
		public bool IsGameOver { get; private set; } // Флаг окончания игры

		// Зависимости
		private readonly IMammothMovement _mammothMovement;
		private readonly Stopwatch _stopwatch;
		public ThrowingTheSpearModel SpearModel { get; }
		public ThrowingTheSpearController SpearController { get; }

		// Конструктор инициализирует все игровые объекты и зависимости
		public GameModel(IMammothView mammothView, IRandomGenerator randomGenerator)
		{
			// Создаем охотника через фабрику
			Hunter = GameObjectFactory.CreateHunter(
				x: 10,
				y: 5,
				mapWidth: MapWidth,
				mapHeight: MapHeight);

			// Создаем мамонта через фабрику с передачей IMammothView
			Mammoth = GameObjectFactory.CreateMammoth(10, 10, mammothView);

			// Создаем копье через фабрику
			SpearModel = GameObjectFactory.CreateSpear(
				hunter: Hunter,
				mammoth: Mammoth,
				onHit: () => IsGameOver = true);

			// Инициализируем зависимости
			_mammothMovement = new MammothMovement(MapWidth, MapHeight, randomGenerator);
			SpearController = new ThrowingTheSpearController(SpearModel, new ThrowingTheSpearView());
			_stopwatch = Stopwatch.StartNew(); // Запускаем таймер
		}

		// Метод обновления состояния игры
		public void Update(Direction direction, bool throwSpear)
		{
			if (IsGameOver) return; // Если игра окончена, выходим

			Hunter.Move(direction); // Двигаем охотника
			_mammothMovement.MoveMammoth(Mammoth); // Двигаем мамонта

			if (throwSpear)
			{
				SpearController.Throw(direction); // Бросаем копье
			}

			UpdateScore(); // Обновляем счет
			CheckGameOver(); // Проверяем окончание игры
		}

		// Метод обновления счета
		public void UpdateScore()
		{
			int elapsed = (int)_stopwatch.Elapsed.TotalSeconds;
			Score = CalculateScore(elapsed);
		}

		public static int CalculateScore(int elapsedSeconds)
		{
			return 1000 - Math.Max(0, elapsedSeconds - 30) * 10 - Math.Min(30, elapsedSeconds) * 10;
		}

		// Метод проверки окончания игры
		private void CheckGameOver()
		{
			var head = Hunter.Head;
			if (head.X <= 2 || head.X >= MapWidth - 2 ||
				head.Y <= 2 || head.Y >= MapHeight - 4)
			{
				IsGameOver = true; // Игра окончена, если охотник вышел за границы
				Score = 0;
			}
			else if (SpearModel.IsTargetHit)
			{
				IsGameOver = true; // Игра окончена, если копье попало в цель
				UpdateScore();
			}
		}
	}
}