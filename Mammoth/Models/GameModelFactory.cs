using MammothHunting.Controllers;
using MammothHunting.Views;


namespace MammothHunting.Models
// Используемые пространства имен
{
	// Фабрика для создания GameModel с зависимостями
	// Это позволяет легко изменять зависимости в будущем
	// и делает код более чистым и понятным
	// Фабрика для создания GameModel с зависимостями
	public static class GameModelFactory
	{
		public static GameModel Create()
		{
			// Создаем зависимости
			IMammothView mammothView = new MammothView();
			IRandomGenerator randomGenerator = new RandomGenerator();

			// Возвращаем GameModel с готовыми зависимостями
			return new GameModel(mammothView, randomGenerator);
		}
	}
}