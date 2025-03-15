using System;
using static System.Console;

namespace Mammoth.Views
{
	public class Player
	{
		public string Name { get; private set; }

		public void SetName()
		{
			Clear();
			WriteLine("=== Ввод имени игрока ===");
			Write("Введите ваше имя: ");
			Name = ReadLine();
			WriteLine($"Ваше имя: {Name}");
			WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");
			ReadKey();
			MainMenu.Show();
		}
	}
}