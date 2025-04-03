using MammothHunting.Controllers;
using MammothHunting.Views;
using MammothWPF.Models;
using System.Diagnostics;

namespace MammothHunting.Models
{
	public class GameModel
	{
		public const int MapWidth = 70;
		public const int MapHeight = 30;

		public Hunter Hunter { get; }
		public Mammoth Mammoth { get; }
		public int Score { get; private set; } = 1000;
		public bool IsGameOver { get; private set; }

		private readonly MammothMovement _mammothMovement;
		private readonly Stopwatch _stopwatch;
		public ThrowingTheSpearModel SpearModel { get; }
		public ThrowingTheSpearController SpearController { get; }

		public GameModel()
		{
			Hunter = GameObjectFactory.CreateHunter(
				x: 10,
				y: 5,
				mapWidth: MapWidth,
				mapHeight: MapHeight);

			Mammoth = GameObjectFactory.CreateMammoth(
				x: 20,
				y: 20);

			SpearModel = GameObjectFactory.CreateSpear(
			hunter: Hunter,
			mammoth: Mammoth,
			onHit: () => IsGameOver = true);

			//SpearModel = new ThrowingTheSpearModel(Hunter, Mammoth, () => IsGameOver = true);
			_mammothMovement = new MammothMovement(MapWidth, MapHeight);
			SpearController = new ThrowingTheSpearController(SpearModel, new ThrowingTheSpearView());
			_stopwatch = Stopwatch.StartNew();
		}

		public void Update(Direction direction, bool throwSpear)
		{
			if (IsGameOver) return;

			Hunter.Move(direction);
			_mammothMovement.MoveMammoth(Mammoth);

			if (throwSpear)
			{
				SpearController.Throw(direction);
			}

			UpdateScore();
			CheckGameOver();
		}

		public void UpdateScore()
		{
			int elapsed = (int)_stopwatch.Elapsed.TotalSeconds;
			Score = 1000 - Math.Max(0, elapsed - 30) * 10 - Math.Min(30, elapsed) * 10;
		}

		private void CheckGameOver()
		{
			var head = Hunter.Head;
			if (head.X <= 2 || head.X >= MapWidth - 2 ||
				head.Y <= 2 || head.Y >= MapHeight - 4)
			{
				IsGameOver = true;
				Score = 0;
			}
			else if (SpearModel.IsTargetHit)
			{
				IsGameOver = true;
				UpdateScore();
			}
		}
	}
}
