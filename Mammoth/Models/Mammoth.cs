using System;
using System.Collections.Generic;

namespace Mammoth.Models
{
	/// <summary>
	/// Класс мамонта
	/// </summary>
	public class Mammoth
	{
		private readonly object _lock = new object();

		/// <summary>
		/// Конструктор мамонта
		/// </summary>
		/// <param name="initialX"></param>
		/// <param name="initialY"></param>
		/// <param name="mammothColor"></param>
		/// <param name="tuskColor"></param>
		public Mammoth(int initialX, int initialY, ConsoleColor mammothColor, ConsoleColor tuskColor)
		{
			_mammothColor = mammothColor;
			_tuskColor = tuskColor;

			// Основное тело
			Body = new List<Pixel>
				{
					 new Pixel(initialX - 2, initialY, mammothColor),
					 new Pixel(initialX - 1, initialY, mammothColor),
					 new Pixel(initialX - 2, initialY + 1, mammothColor),
					 new Pixel(initialX - 1, initialY + 1, mammothColor),
					 new Pixel(initialX - 1, initialY + 2, ConsoleColor.DarkYellow),
					 new Pixel(initialX - 2, initialY + 2, ConsoleColor.DarkYellow),
				};

			// Голова (отдельный пиксель сверху)
			Head = new Pixel(initialX, initialY, mammothColor);

			// Бивень (наискосок справа от головы)
			Tusk = new List<Pixel>
				{
					 new Pixel(initialX, initialY + 1, tuskColor),
					 new Pixel(initialX + 1, initialY + 2, tuskColor),
				};

			Draw();
		}

		/// <summary>
		/// Метод отрисовки мамонта
		/// </summary>
		public void Draw()
		{
			lock (_lock)
			{
				foreach (Pixel pixel in Body)
				{
					pixel.Draw();
				}
				Head.Draw();
				foreach (Pixel pixel in Tusk)
				{
					pixel.Draw();
				}
			}
		}

		/// <summary>
		/// Метод очистки мамонта
		/// </summary>
		public void Clear()
		{
			lock (_lock)
			{
				foreach (Pixel pixel in Body)
				{
					pixel.Clear();
				}
				Head.Clear();
				foreach (Pixel pixel in Tusk)
				{
					pixel.Clear();
				}
			}
		}

		/// <summary>
		/// Метод движения мамонта
		/// </summary>
		/// <param name="currentTarget"></param>
		internal void MoveTowards(Pixel currentTarget)
		{
			lock (_lock)
			{
				Clear();

				int deltaX = 0;
				int deltaY = 0;

				if (Head.X < currentTarget.X)
					deltaX = 1;
				else if (Head.X > currentTarget.X)
					deltaX = -1;

				if (Head.Y < currentTarget.Y)
					deltaY = 1;
				else if (Head.Y > currentTarget.Y)
					deltaY = -1;

				Head = new Pixel(Head.X + deltaX, Head.Y + deltaY, _mammothColor);

				for (int i = 0; i < Body.Count; i++)
				{
					Body[i] = new Pixel(Body[i].X + deltaX, Body[i].Y + deltaY, _mammothColor);
				}

				for (int i = 0; i < Tusk.Count; i++)
				{
					Tusk[i] = new Pixel(Tusk[i].X + deltaX, Tusk[i].Y + deltaY, _tuskColor);
				}

				Draw();
			}
		}

		/// <summary>
		/// тело мамонта
		/// </summary>
		public List<Pixel> Body { get; }
		/// <summary>
		/// голова мамонта
		/// </summary>
		public Pixel Head { get; private set; }
		/// <summary>
		/// бивень мамонта
		/// </summary>
		public List<Pixel> Tusk { get; private set; }

		/// <summary>
		/// цвет тела
		/// </summary>
		private readonly ConsoleColor _mammothColor;
		/// <summary>
		/// цвет бивня
		/// </summary>
		private readonly ConsoleColor _tuskColor;
	}
}