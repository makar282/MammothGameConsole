using System.Collections.Generic;
using MammothHunting.Views;

namespace MammothHunting.Models
{
	public class Hunter
	{
		private readonly object _lock = new object();

		public Hunter(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, int mapWidth, int mapHeight)
		{
			Head = new Pixel(initialX, initialY, headColor);
			Body = new List<Pixel>
			{
				new Pixel(initialX, initialY + 1, bodyColor),
				new Pixel(initialX, initialY + 2, bodyColor)
			};

			_headColor = headColor;
			_bodyColor = bodyColor;
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
					case Direction.Up: deltaY = -1; break;
					case Direction.Down: deltaY = 1; break;
					case Direction.Left: deltaX = -1; break;
					case Direction.Right: deltaX = 1; break;
				}

				// Перемещаем голову и тело охотника
				Head = new Pixel(Head.X + deltaX, Head.Y + deltaY, _headColor);
				for (int i = 0; i < Body.Count; i++)
				{
					Body[i] = new Pixel(Body[i].X + deltaX, Body[i].Y + deltaY, _bodyColor);
				}
				// Рисуем охотника
				HunterView.Draw(this);
			}
		}

		public Pixel Head { get; private set; }
		public List<Pixel> Body { get; }
		private readonly ConsoleColor _headColor;
		private readonly ConsoleColor _bodyColor;
		public int MapWidth { get; }
		public int MapHeight { get; }
	}
}