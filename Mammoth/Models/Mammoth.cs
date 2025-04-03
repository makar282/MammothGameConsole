using System;
using System.Collections.Generic;
using MammothHunting.Views;

namespace MammothHunting.Models
{
	public class Mammoth
	{
		private readonly ConsoleColor _mammothColor; // Цвет тела мамонта
		private readonly ConsoleColor _tuskColor;    // Цвет бивней

		public List<Pixel> Body { get; }  // Пиксели тела
		public Pixel Head { get; private set; }  // Голова (отдельный пиксель)
		public List<Pixel> Tusk { get; private set; }  // Бивни (2 пикселя)

		public Mammoth(int initialX, int initialY, ConsoleColor mammothColor, ConsoleColor tuskColor)
		{
			_mammothColor = mammothColor;
			_tuskColor = tuskColor;

			// Тело мамонта (6 пикселей)
			Body = new List<Pixel>
			{
				new Pixel(initialX - 2, initialY, mammothColor),
				new Pixel(initialX - 1, initialY, mammothColor),
				new Pixel(initialX - 2, initialY + 1, mammothColor),
				new Pixel(initialX - 1, initialY + 1, mammothColor),
				new Pixel(initialX - 1, initialY + 2, ConsoleColor.DarkYellow),  // Глаза/нос
                new Pixel(initialX - 2, initialY + 2, ConsoleColor.DarkYellow),
			};

			Head = new Pixel(initialX, initialY, mammothColor);  // Голова (1 пиксель)

			// Бивни (2 пикселя под углом)
			Tusk = new List<Pixel>
			{
				new Pixel(initialX, initialY + 1, tuskColor),
				new Pixel(initialX + 1, initialY + 2, tuskColor),
			};

			MammothView.Draw(this);  // Отрисовка при создании
		}

		// Двигает мамонта к цели (пошагово, на 1 пиксель за вызов)
		public void MoveTowards(Pixel currentTarget)
		{
			MammothView.Clear(this);  // Стираем старое положение

			int deltaX = Math.Sign(currentTarget.X - Head.X);  // Направление по X (-1, 0, 1)
			int deltaY = Math.Sign(currentTarget.Y - Head.Y);  // Направление по Y (-1, 0, 1)

			// Обновляем позицию головы, тела и бивней
			Head = new Pixel(Head.X + deltaX, Head.Y + deltaY, _mammothColor);
			for (int i = 0; i < Body.Count; i++)
				Body[i] = new Pixel(Body[i].X + deltaX, Body[i].Y + deltaY, _mammothColor);
			for (int i = 0; i < Tusk.Count; i++)
				Tusk[i] = new Pixel(Tusk[i].X + deltaX, Tusk[i].Y + deltaY, _tuskColor);

			MammothView.Draw(this);  // Отрисовываем новое положение
		}

		// Тестовая версия MoveTowards с кастомными методами отрисовки/очистки
		internal void TestMoveTowards(Pixel target, Action<Mammoth> onClear, Action<Mammoth> onDraw)
		{
			onClear(this);  // Очистка через переданный метод
			int deltaX = Math.Sign(target.X - Head.X);
			int deltaY = Math.Sign(target.Y - Head.Y);

			Head = new Pixel(Head.X + deltaX, Head.Y + deltaY, _mammothColor);
			for (int i = 0; i < Body.Count; i++)
				Body[i] = new Pixel(Body[i].X + deltaX, Body[i].Y + deltaY, _mammothColor);
			for (int i = 0; i < Tusk.Count; i++)
				Tusk[i] = new Pixel(Tusk[i].X + deltaX, Tusk[i].Y + deltaY, _tuskColor);

			onDraw(this);  // Отрисовка через переданный метод
		}
	}
}