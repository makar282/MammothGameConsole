using System.Collections.Generic;
using MammothHunting.Views;

namespace MammothHunting.Models
{
	public class Hunter
	{
		private readonly object _lock = new object();

		public Hunter(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, ConsoleColor spearColor, int mapWidth, int mapHeight)
		{
			Head = new Pixel(initialX, initialY, headColor);
			Body = new List<Pixel>
			{
					new Pixel(initialX, initialY + 1, bodyColor),
					new Pixel(initialX, initialY + 2, bodyColor)
			};
			Spear = new Pixel(initialX + 1, initialY + 1, spearColor);

			_headColor = headColor;
			_bodyColor = bodyColor;
			_spearColor = spearColor;
			MapWidth = mapWidth;
			MapHeight = mapHeight;
		}

		public void Move(Direction direction)
		{
			lock (_lock)
			{
				HunterView.Clear(this);

				int deltaX = 0;
				int deltaY = 0;

				switch (direction)
				{
					case Direction.Up:
						deltaY = -1;
						break;
					case Direction.Down:
						deltaY = 1;
						break;
					case Direction.Left:
						deltaX = -1;
						break;
					case Direction.Right:
						deltaX = 1;
						break;
				}

				// Перемещаем голову и тело охотника
				Head = new Pixel(Head.X + deltaX, Head.Y + deltaY, _headColor);

				for (int i = 0; i < Body.Count; i++)
				{
					Body[i] = new Pixel(Body[i].X + deltaX, Body[i].Y + deltaY, _bodyColor);
				}

				// Расположение копья зависит от направления движения
				switch (direction)
				{
					case Direction.Left:
						Spear = new Pixel(Head.X - 1, Head.Y + 1, _spearColor);
						break;
					case Direction.Right:
						Spear = new Pixel(Head.X + 1, Head.Y + 1, _spearColor);
						break;
					default:
						// Если направление вверх или вниз, копьё остается где и было
						Spear = new Pixel(Spear.X + deltaX, Spear.Y + deltaY, _spearColor);
						break;
				}
				HunterView.Draw(this);
			}
		}

		public Pixel Head { get; private set; }
		public List<Pixel> Body { get; }
		public Pixel Spear { get; private set; }
		private readonly ConsoleColor _headColor;
		private readonly ConsoleColor _bodyColor;
		private readonly ConsoleColor _spearColor;
		public int MapWidth { get; }
		public int MapHeight { get; }
	}
}