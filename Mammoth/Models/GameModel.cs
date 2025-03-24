using MammothHunting.Controllers;
using MammothHunting.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MammothHunting.Models
{
	// GameModel.cs
	public class GameModel
	{
		public const int MapWidth = 51;
		public const int MapHeight = 31;

		public Hunter Hunter { get; }
		public Mammoth Mammoth { get; }
		public int Score { get; private set; } = 1000;
		public bool IsGameOver { get; private set; }

		private readonly MammothMovement _mammothMovement;
		private readonly ThrowingTheSpear _throwingTheSpear;
		private readonly Stopwatch _stopwatch;

		public GameModel()
		{
			Hunter = new Hunter(10, 5, ConsoleColor.DarkYellow, ConsoleColor.White, ConsoleColor.DarkGray, MapWidth, MapHeight);
			Mammoth = new Mammoth(20, 20, ConsoleColor.DarkGray, ConsoleColor.White);
			_mammothMovement = new MammothMovement(MapWidth, MapHeight);
			_throwingTheSpear = new ThrowingTheSpear(Hunter, Mammoth, () => IsGameOver = true);
			_stopwatch = Stopwatch.StartNew();
		}

		public void Update(Direction direction, bool throwSpear)
		{
			if (IsGameOver) return;

			Hunter.Move(direction);
			_mammothMovement.MoveMammoth(Mammoth);

			if (throwSpear)
			{
				_throwingTheSpear.Throw(direction);
			}

			UpdateScore();
			CheckGameOver();
		}

		private void UpdateScore()
		{
			int elapsed = (int)_stopwatch.Elapsed.TotalSeconds;
			Score = 1000 - Math.Max(0, elapsed - 30) * 10 - Math.Min(30, elapsed) * 10;
		}

		private void CheckGameOver()
		{
			var head = Hunter.Head;
			IsGameOver = head.X <= 1 || head.X >= MapWidth - 1 ||
						 head.Y <= 1 || head.Y >= MapHeight - 1 ||
						 _throwingTheSpear.IsTargetHit;
		}
	}
}
