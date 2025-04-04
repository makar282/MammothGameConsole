using MammothHunting.Models;
using MammothHunting.Views;
using MammothHunting.Controllers;

namespace MammothHunting.Models
{
	public static class GameObjectFactory
	{
		public static Hunter CreateHunter(int x, int y, int mapWidth, int mapHeight)
		{
			// Предполагаем, что Hunter не изменился и не требует дополнительных зависимостей
			return new Hunter(
				initialX: x,
				initialY: y,
				headColor: ConsoleColor.DarkYellow,
				bodyColor: ConsoleColor.White,
				mapWidth: mapWidth,
				mapHeight: mapHeight
			);
		}

		public static Mammoth CreateMammoth(int x, int y, IMammothView view)
		{
			// Mammoth теперь требует IMammothView
			return new Mammoth(
				initialX: x,
				initialY: y,
				mammothColor: ConsoleColor.DarkGray,
				tuskColor: ConsoleColor.White,
				view: view
			);
		}

		public static ThrowingTheSpearModel CreateSpear(Hunter hunter, Mammoth mammoth, Action onHit)
		{
			return new ThrowingTheSpearModel(
				hunter: hunter,
				mammoth: mammoth,
				onHit: onHit
			);
		}
	}
}