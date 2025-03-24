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
	}
}