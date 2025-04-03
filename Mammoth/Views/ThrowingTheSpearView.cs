using MammothHunting.Models;
using System;

namespace MammothHunting.Views
{
	public class ThrowingTheSpearView
	{
		private readonly ConsoleColor _spearColor = ConsoleColor.DarkGray;

		public Pixel CalculateSpearPosition(Hunter hunter, Direction direction)
		{
			return direction switch
			{
				Direction.Left => new Pixel(hunter.Head.X - 1, hunter.Head.Y + 1, _spearColor),
				Direction.Right => new Pixel(hunter.Head.X + 1, hunter.Head.Y + 1, _spearColor),
				_ => new Pixel(hunter.Head.X + 1, hunter.Head.Y + 1, _spearColor)
			};
		}

		public void Draw(Pixel spear)
		{
			spear.Draw();
		}

		public void Clear(Pixel spear)
		{
			spear.Clear();
		}
	}
}
