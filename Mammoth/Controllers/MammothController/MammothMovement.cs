using MammothHunting.Models;

namespace MammothHunting.Controllers
{
	public class MammothMovement : IMammothMovement
	{
		private readonly int _mapWidth;
		private readonly int _mapHeight;
		private readonly IRandomGenerator _randomGenerator;
		private Pixel _currentTarget;

		public MammothMovement(int mapWidth, int mapHeight, IRandomGenerator randomGenerator)
		{
			_mapWidth = mapWidth;
			_mapHeight = mapHeight;
			_randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
			_currentTarget = GenerateNewTarget();
		}

		public Pixel CurrentTarget => _currentTarget;

		public void MoveMammoth(Mammoth mammoth)
		{
			if (mammoth.Head.X == _currentTarget.X && mammoth.Head.Y == _currentTarget.Y)
			{
				_currentTarget = GenerateNewTarget();
			}
			mammoth.MoveTowards(_currentTarget);
		}

		private Pixel GenerateNewTarget()
		{
			int x = Math.Max(3, Math.Min(_mapWidth - 4, _randomGenerator.Next(3, _mapWidth - 3)));
			int y = Math.Max(2, Math.Min(_mapHeight - 5, _randomGenerator.Next(2, _mapHeight - 4)));
			return new Pixel(x, y, ConsoleColor.DarkGray);
		}
	}

	public interface IRandomGenerator
	{
		int Next(int minValue, int maxValue);
	}

	public class RandomGenerator : IRandomGenerator
	{
		private static readonly Random Random = new Random();
		public int Next(int minValue, int maxValue) => Random.Next(minValue, maxValue);
	}
}