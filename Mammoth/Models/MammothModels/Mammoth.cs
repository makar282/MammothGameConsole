using System;
using System.Collections.Generic;
using MammothHunting.Views;
using System;
using System.Collections.Generic;

namespace MammothHunting.Models
{
	public class Mammoth
	{
		private readonly ConsoleColor _mammothColor;
		private readonly ConsoleColor _tuskColor;
		private readonly IMammothView _view; // Зависимость от интерфейса

		public List<Pixel> Body { get; }
		public Pixel Head { get; private set; }
		public List<Pixel> Tusk { get; private set; }

		public Mammoth(int initialX, int initialY, ConsoleColor mammothColor, ConsoleColor tuskColor, IMammothView view)
		{
			_mammothColor = mammothColor;
			_tuskColor = tuskColor;
			_view = view ?? throw new ArgumentNullException(nameof(view)); // Проверка на null

			// Инициализация тела
			Body = new List<Pixel>
			{
				new Pixel(initialX - 2, initialY, mammothColor),
				new Pixel(initialX - 1, initialY, mammothColor),
				new Pixel(initialX - 2, initialY + 1, mammothColor),
				new Pixel(initialX - 1, initialY + 1, mammothColor),
				new Pixel(initialX - 1, initialY + 2, ConsoleColor.DarkYellow),
				new Pixel(initialX - 2, initialY + 2, ConsoleColor.DarkYellow),
			};

			// Инициализация головы
			Head = new Pixel(initialX, initialY, mammothColor);

			// Инициализация бивней
			Tusk = new List<Pixel>
			{
				new Pixel(initialX, initialY + 1, tuskColor),
				new Pixel(initialX + 1, initialY + 2, tuskColor),
			};
			// Убрали вызов MammothView.Draw(this)
		}

		public void MoveTowards(Pixel currentTarget)
		{
			_view.Clear(this); // Используем интерфейс

			int deltaX = Math.Sign(currentTarget.X - Head.X);
			int deltaY = Math.Sign(currentTarget.Y - Head.Y);

			Head = new Pixel(Head.X + deltaX, Head.Y + deltaY, _mammothColor);
			for (int i = 0; i < Body.Count; i++)
				Body[i] = new Pixel(Body[i].X + deltaX, Body[i].Y + deltaY, _mammothColor);
			for (int i = 0; i < Tusk.Count; i++)
				Tusk[i] = new Pixel(Tusk[i].X + deltaX, Tusk[i].Y + deltaY, _tuskColor);

			_view.Draw(this); // Используем интерфейс
		}

		public void TestMoveTowards(Pixel target, Action<Mammoth> onClear, Action<Mammoth> onDraw)
		{
			onClear(this);
			int deltaX = Math.Sign(target.X - Head.X);
			int deltaY = Math.Sign(target.Y - Head.Y);

			Head = new Pixel(Head.X + deltaX, Head.Y + deltaY, _mammothColor);
			for (int i = 0; i < Body.Count; i++)
				Body[i] = new Pixel(Body[i].X + deltaX, Body[i].Y + deltaY, _mammothColor);
			for (int i = 0; i < Tusk.Count; i++)
				Tusk[i] = new Pixel(Tusk[i].X + deltaX, Tusk[i].Y + deltaY, _tuskColor);

			onDraw(this);
		}
	}
}