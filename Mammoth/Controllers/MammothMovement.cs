using System;
using System.Collections.Generic;
using MammothHunting.Models;

namespace MammothHunting.Controllers
{
	public class MammothMovement
	{
		private readonly int _mapWidth;
		private readonly int _mapHeight;
		private static readonly Random Random = new Random();
		private Pixel _currentTarget;

		public MammothMovement(int mapWidth, int mapHeight)
		{
			_mapWidth = mapWidth;
			_mapHeight = mapHeight;
			_currentTarget = GenerateNewTarget();
		}

		public Pixel CurrentTarget => _currentTarget;

		public void MoveMammoth(Models.Mammoth mammoth)
		{
			if (mammoth.Head.X == _currentTarget.X && mammoth.Head.Y == _currentTarget.Y)
			{
				_currentTarget = GenerateNewTarget();
			}
			mammoth.MoveTowards(_currentTarget);
		}

		private Pixel GenerateNewTarget()
		{
			return new Pixel(Random.Next(2, _mapWidth - 3), Random.Next(2, _mapHeight - 3), ConsoleColor.DarkGray);
		}
	}
}