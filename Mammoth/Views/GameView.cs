using MammothHunting.Controllers;
using MammothHunting.Models;

namespace MammothHunting.Views
{
	public class GameView
	{
		private readonly IMammothView _mammothView;

		public GameView(IMammothView mammothView)
		{
			_mammothView = mammothView ?? throw new ArgumentNullException(nameof(mammothView));
		}

		public static void DrawBorder()
		{
			// Проверяем, чтобы границы не выходили за пределы консоли
			int maxX = Math.Min(GameModel.MapWidth, MainMenu.ScreenWidth) - 1;
			int maxY = Math.Min(GameModel.MapHeight, MainMenu.ScreenHeight) - 1;

			// Верхняя и нижняя границы
			for (int i = 1; i < maxX; i++)
			{
				new Pixel(i, 1, ConsoleColor.DarkGreen).Draw();
				new Pixel(i, maxY - 1, ConsoleColor.DarkGreen).Draw();
			}

			// Левая и правая границы
			for (int i = 1; i < maxY; i++)
			{
				new Pixel(1, i, ConsoleColor.DarkGreen).Draw();
				new Pixel(maxX - 1, i, ConsoleColor.DarkGreen).Draw();
			}
		}

		// Отрисовка всех игровых объектов
		public void DrawGameObjects(Hunter hunter, Mammoth mammoth, ThrowingTheSpearController spear)
		{
			HunterView.Draw(hunter);               // Оставляем статический вызов
			_mammothView.Draw(mammoth);            // Используем интерфейс для Mammoth
												   //spear.Draw(); // Добавляем отрисовку копья
		}

		// Очистка игровой области
		public static void ClearGameArea()
		{
			for (int y = 2; y < GameModel.MapHeight - 2; y++)
			{

				Console.SetCursorPosition(2, y);
				Console.Write(new string(' ', GameModel.MapWidth - 4));
			}
		}
	}
}