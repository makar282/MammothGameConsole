using System;
using System.Collections.Generic;

namespace MammothHunting.Models
{
	public class ThrowingTheSpearModel
	{
		public Pixel _position;
		public Direction CurrentDirection { get; private set; }
		public bool IsThrown { get; set; }
		public bool IsTargetHit { get; private set; }

		private readonly Hunter _hunter;
		private readonly Mammoth _mammoth;
		private readonly Action _onHit;

		public Pixel Position
		{
			get => _position;
			set
			{
				_position.Clear();
				_position = value;
			}
		}

		public ThrowingTheSpearModel(Hunter hunter, Mammoth mammoth, Action onHit)
		{
			_hunter = hunter;
			_mammoth = mammoth;
			_onHit = onHit;
		}

		public void Throw(Direction direction)
		{
			if (IsThrown) return;

			CurrentDirection = direction;
			IsThrown = true;
			IsTargetHit = false;
			Position = new Pixel(
				_hunter.Head.X + (direction == Direction.Left ? -1 : 1),
				_hunter.Head.Y + 1, ConsoleColor.DarkGray);
		}

		public void UpdatePosition()
		{
			if (!IsThrown) return;

			var newX = Position.X + (CurrentDirection == Direction.Left ? -1 : 1);
			var newY = Position.Y;

			if (IsOutOfBounds())
			{
				Position.Clear();
				IsThrown = false;
				return;
			}

			Position = new Pixel(newX, newY, ConsoleColor.DarkGray);

			if (CheckCollision())
			{
				IsTargetHit = true;
				_onHit?.Invoke();
				IsThrown = false;
			}
		}

		private bool CheckCollision()
		{
			return _mammoth.Head.X == Position.X && _mammoth.Head.Y == Position.Y ||
				   _mammoth.Body.Exists(p => p.X == Position.X && p.Y == Position.Y) ||
				   _mammoth.Tusk.Exists(p => p.X == Position.X && p.Y == Position.Y);
		}

		private bool IsOutOfBounds()
		{
			return Position.X <= 1 || Position.X >= _hunter.MapWidth - 1 ||
				   Position.Y <= 1 || Position.Y >= _hunter.MapHeight - 1;
		}

		public void ResetSpear()
		{
			IsThrown = false;
			// Дополнительные действия по сбросу состояния копья
		}
	}
}