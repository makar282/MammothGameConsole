using MammothHunting.Models;

namespace MammothHunting.Views
{
	public class MammothView : IMammothView
	{
		public void Draw(Mammoth mammoth)
		{
			foreach (var pixel in mammoth.Body)
				pixel.Draw();
			mammoth.Head.Draw();
			foreach (var tuskPixel in mammoth.Tusk)
				tuskPixel.Draw();
		}

		public void Clear(Mammoth mammoth)
		{
			foreach (var pixel in mammoth.Body)
				pixel.Clear();
			mammoth.Head.Clear();
			foreach (var tuskPixel in mammoth.Tusk)
				tuskPixel.Clear();
		}
	}
}