using System;
using MammothHunting.Models;

namespace MammothHunting.Views
{
	public static class GameView
	{
		// Отрисовка границ
		public static void DrawBorder()
		{
			for (int i = 1; i < GameModel.MapWidth; i++)
			{
				new Pixel(i, 1, ConsoleColor.DarkGreen).Draw();
				new Pixel(i, GameModel.MapHeight - 1, ConsoleColor.DarkGreen).Draw();
			}

			for (int i = 1; i < GameModel.MapHeight; i++)
			{
				new Pixel(1, i, ConsoleColor.DarkGreen).Draw();
				new Pixel(GameModel.MapWidth - 1, i, ConsoleColor.DarkGreen).Draw();
			}
		}

		// Отрисовка охотника и мамонта
		public static void DrawGameObjects(Hunter hunter, Mammoth mammoth)
		{
			HunterView.Draw(hunter);
			MammothView.Draw(mammoth);
		}

		// Очистка игровой области
		public static void ClearGameArea()
		{
			for (int y = 2; y < GameModel.MapHeight - 2; y++)
			{
				Console.SetCursorPosition(3, y);
				Console.Write(new string(' ', GameModel.MapWidth - 3));
			}
		}
	}
}	