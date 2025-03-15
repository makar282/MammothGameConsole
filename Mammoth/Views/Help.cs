using System;
using static System.Console;

namespace Mammoth.Views
{
	class Help
	{
		public static void Show()
		{
			Clear();
			WriteLine("=== Справка ===");
			WriteLine("Правила игры:");
			WriteLine("1. Используйте стрелки для перемещения охотника.");
			WriteLine("2. Нажмите пробел, чтобы бросить копье.");
			WriteLine("3. Избегайте столкновений с границами карты.");
			WriteLine("4. Попадите копьем в мамонта, чтобы выиграть.");
			WriteLine("5. Нажмите Esc, чтобы вернуться в главное меню.");
			WriteLine("================");
			WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");
			ReadKey();
			MainMenu.Show();
		}
	}
}