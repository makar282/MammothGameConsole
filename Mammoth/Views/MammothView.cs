using System;
using MammothHunting.Models;

namespace MammothHunting.Views
{
	public static class MammothView
	{
		/// <summary>
		/// Метод отрисовки мамонта
		/// </summary>
		public static void Draw(MammothHunting.Models.Mammoth mammoth)
		{
			lock (mammoth)
			{
				foreach (Pixel pixel in mammoth.Body)
				{
					pixel.Draw();
				}
				mammoth.Head.Draw();
				foreach (Pixel pixel in mammoth.Tusk)
				{
					pixel.Draw();
				}
			}
		}

		/// <summary>
		/// Метод очистки мамонта
		/// </summary>
		public static void Clear(MammothHunting.Models.Mammoth mammoth)
		{
			lock (mammoth)
			{
				foreach (Pixel pixel in mammoth.Body)
				{
					pixel.Clear();
				}
				mammoth.Head.Clear();
				foreach (Pixel pixel in mammoth.Tusk)
				{
					pixel.Clear();
				}
			}
		}

		// Временный класс-обёртка для тестов
		public static class MammothViewWrapper
		{
			public static Action<Mammoth> OnDraw { get; set; } = MammothView.Draw;
			public static Action<Mammoth> OnClear { get; set; } = MammothView.Clear;

			public static void Draw(Mammoth m) => OnDraw(m);
			public static void Clear(Mammoth m) => OnClear(m);
		}
	}
}