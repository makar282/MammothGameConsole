using System;
using static System.Console;

namespace MammothHunting.Views
{
	public class Player
	{
		public string Name { get; private set; }

		public void SetName()
		{
			Console.SetCursorPosition(0, 0); // Устанавливаем курсор в начало
			Clear(); // Очищаем экран только здесь

			WriteLine("=== Ввод имени игрока ===");
			Write("Введите ваше имя: ");
			Console.SetCursorPosition(0, 2); // Перемещаем курсор на третью строку
			Name = ReadLine();
			Console.SetCursorPosition(0, 3);
			WriteLine($"Ваше имя: {Name}");
			WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");

			ReadKey();
			MainMenu.Show();
		}
	}
}
