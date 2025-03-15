using System;

namespace Mammoth.Models
{
	// класс Pixel'a 
	public readonly struct Pixel
	{
		private const char PixelChar = '█';

		// конструктор класса Pixel
		public Pixel(int x, int y, ConsoleColor color, int pixelSize = 1)
		{
			X = x;
			Y = y;
			Color = color;
			PixelSize = pixelSize;
		}

		// Pixel symbol
		public ConsoleColor Color { get; }
		// размер пикселя
		public int PixelSize { get; }
		// Pixel X coordinate
		public int X { get; }
		// Pixel Y coordinate
		public int Y { get; }

		// метод рисования пикселей
		public void Draw()
		{
			Console.ForegroundColor = Color;
			Console.SetCursorPosition(X, Y);
			Console.Write(PixelChar);
		}

		// метод очистки поля от пикселя
		public void Clear()
		{
			Console.SetCursorPosition(X, Y);
			Console.Write(' ');
		}
	}
}