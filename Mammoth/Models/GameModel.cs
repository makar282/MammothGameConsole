using MammothHunting.Controllers;
using MammothHunting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace MammothHunting.Models
{
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
			public ThrowingTheSpear _throwingTheSpear;
			private readonly Stopwatch _stopwatch;

			public GameModel()
			{
				Hunter = new Hunter(10, 5, ConsoleColor.DarkYellow, ConsoleColor.White, MapWidth, MapHeight);
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
				if (head.X <= 2 || head.X >= MapWidth - 2 ||
					head.Y <= 2 || head.Y >= MapHeight - 1)
				{
					IsGameOver = true;
					Score = 0;
				}
				else if (_throwingTheSpear.IsTargetHit)
				{
					IsGameOver = true;
					UpdateScore();
				}
			}
		}
	}
}