using System.Collections.Generic;

namespace Mammoth.Models
{
	/// <summary>
	/// Охотник
	/// </summary>
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

		/// <summary>
		/// Метод перемещения охотника
		/// </summary>
		/// <param name="direction"></param>
		public void Move(Direction direction)
		{
			lock (_lock)
			{
				Clear();

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

				// расположение копья зависит от направления движения
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

				Draw();
			}
		}

		/// <summary>
		/// Метод для отрисовки охотника
		/// </summary>
		public void Draw()
		{
			lock (_lock)
			{
				Head.Draw();
				foreach (var pixel in Body)
				{
					pixel.Draw();
				}
				Spear.Draw();
			}
		}

		/// <summary>
		/// Метод для очистки охотника
		/// </summary>
		public void Clear()
		{
			lock (_lock)
			{
				Head.Clear();
				foreach (var pixel in Body)
				{
					pixel.Clear();
				}
				Spear.Clear();
			}
		}

		/// <summary>
		/// Голова охотника
		/// </summary>
		public Pixel Head { get; private set; }
		/// <summary>
		/// Тело охотника
		/// </summary>
		public List<Pixel> Body { get; }
		/// <summary>
		/// Копье охотника
		/// </summary>
		public Pixel Spear { get; private set; }
		/// <summary>
		/// Цвета охотника
		/// </summary>
		private readonly ConsoleColor _headColor;
		private readonly ConsoleColor _bodyColor;
		private readonly ConsoleColor _spearColor;
		public int MapWidth { get; }
		public int MapHeight { get; }
	}
}