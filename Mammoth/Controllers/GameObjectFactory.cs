using MammothHunting.Models;

namespace MammothWPF.Models
{
	/// <summary>
	/// Фабрика для создания охотников
	/// </summary>
	public static class GameObjectFactory
	{
		public static Hunter CreateHunter(int x, int y, int mapWidth, int mapHeight)
		{
			return new Hunter(
				initialX: x,
				initialY: y,
				headColor: ConsoleColor.DarkYellow,  
				bodyColor: ConsoleColor.White,     
				mapWidth: mapWidth,
				mapHeight: mapHeight
			);
		}

		public static Mammoth CreateMammoth(int x, int y)
		{
			return new Mammoth(
				initialX: x,
				initialY: y,
				mammothColor: ConsoleColor.DarkGray,   
				tuskColor: ConsoleColor.White       
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
