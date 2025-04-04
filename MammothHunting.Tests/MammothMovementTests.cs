using MammothHunting.Controllers;
using MammothHunting.Models;
using MammothHunting.Views;
using Xunit;

namespace MammothHunting.Tests
{
	public class MammothMovementTests
	{
		private class MockMammothView : IMammothView
		{
			public void Draw(Mammoth mammoth) { }
			public void Clear(Mammoth mammoth) { }
		}

		private class MockRandomGenerator : IRandomGenerator
		{
			private readonly int[] _values;
			private int _index;

			public MockRandomGenerator(params int[] values)
			{
				_values = values;
				_index = 0;
			}

			public int Next(int minValue, int maxValue)
			{
				if (_index >= _values.Length)
					return _values[_values.Length - 1]; // Последнее значение по умолчанию
				return _values[_index++];
			}
		}

		[Fact]
		public void MoveMammoth_ShouldMoveTowardsTarget()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(5); // Фиксированная цель
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(4, mammoth.Head.X); // 3 + 1
			Assert.Equal(4, mammoth.Head.Y); // 3 + 1 (движение к (5, 5))
		}

		[Fact]
		public void MoveMammoth_ShouldGenerateNewTarget_WhenReached()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(3, 4); // Первая цель: (3, 3), вторая: (4, 4)
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth); // Уже на цели (3, 3), генерируется новая цель

			// Assert
			Assert.NotEqual(4, movement.CurrentTarget.X); // Новая цель должна быть != 3
			Assert.NotEqual(3, movement.CurrentTarget.Y);
		}

		[Fact]
		public void MoveMammoth_ShouldGenerateNewTarget_WhenReached2()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(3); // Фиксированная цель
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);
			var oldTarget = movement.CurrentTarget; // Сохраняем старую цель

			// Act
			movement.MoveMammoth(mammoth); // Уже на цели (3, 3)

			// Assert
			Assert.NotSame(oldTarget, movement.CurrentTarget); // Проверяем, что цель обновилась
		}

		[Fact]
		public void Constructor_ShouldInitializeCurrentTargetWithinBounds()
		{
			// Arrange
			var random = new MockRandomGenerator(5, 5);
			var movement = new MammothMovement(10, 10, random);

			// Act
			var target = movement.CurrentTarget;

			// Assert
			Assert.InRange(target.X, 3, 6); // 3 <= X < 7 (10 - 3)
			Assert.InRange(target.Y, 2, 5); // 2 <= Y < 6 (10 - 4)
		}

		[Fact]
		public void Constructor_ShouldThrowArgumentNullException_WhenRandomGeneratorIsNull()
		{
			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => new MammothMovement(10, 10, null));
		}

		[Fact]
		public void MoveMammoth_ShouldMoveTowardsTarget_WhenNotAtTarget()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(5, 5);
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(4, mammoth.Head.X); // Движение вправо (3 -> 4)
			Assert.Equal(4, mammoth.Head.Y); // Движение вниз (3 -> 4)
		}


		[Fact]
		public void MoveMammoth_ShouldGenerateNewTarget_WhenTargetReached()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(3, 3, 4, 4); // Первая цель (3, 3), новая (4, 4)
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(4, movement.CurrentTarget.X); // Новая цель
			Assert.Equal(4, movement.CurrentTarget.Y);
		}

		[Fact]
		public void GenerateNewTarget_ShouldStayWithinMapBounds_MinValues()
		{
			// Arrange
			var random = new MockRandomGenerator(3, 2); // Минимальные значения
			var movement = new MammothMovement(10, 10, random);

			// Act
			var target = movement.CurrentTarget;

			// Assert
			Assert.Equal(3, target.X); // Минимум для X
			Assert.Equal(2, target.Y); // Минимум для Y
		}

		[Fact]
		public void GenerateNewTarget_ShouldStayWithinMapBounds_MaxValues()
		{
			// Arrange
			var random = new MockRandomGenerator(6, 5); // Максимальные значения (10-3-1, 10-4-1)
			var movement = new MammothMovement(10, 10, random);

			// Act
			var target = movement.CurrentTarget;

			// Assert
			Assert.Equal(6, target.X); // Максимум для X
			Assert.Equal(5, target.Y); // Максимум для Y
		}

		[Fact]
		public void MoveMammoth_ShouldNotMove_IfAlreadyAtTargetAndNewTargetSame()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(3, 3, 3, 3); // Цель остается (3, 3)
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(3, mammoth.Head.X); // Не двигается
			Assert.Equal(3, mammoth.Head.Y);
		}

		[Fact]
		public void MoveMammoth_ShouldMoveHorizontally_WhenYMatches()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(5, 3);
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(4, mammoth.Head.X); // Движение вправо
			Assert.Equal(3, mammoth.Head.Y); // Y не меняется
		}

		[Fact]
		public void MoveMammoth_ShouldMoveVertically_WhenXMatches()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(3, 5);
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(3, mammoth.Head.X); // X не меняется
			Assert.Equal(4, mammoth.Head.Y); // Движение вниз
		}

		[Fact]
		public void CurrentTarget_ShouldReturnCopyOfTarget()
		{
			// Arrange
			var random = new MockRandomGenerator(5, 5);
			var movement = new MammothMovement(10, 10, random);

			// Act
			var target1 = movement.CurrentTarget;
			var target2 = movement.CurrentTarget;

			// Assert
			Assert.NotSame(target1, target2); // Должны быть разные объекты
			Assert.Equal(target1.X, target2.X);
			Assert.Equal(target1.Y, target2.Y);
		}

		[Fact]
		public void MoveMammoth_ShouldMoveUp_WhenTargetAbove()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(3, 2);
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(3, mammoth.Head.X);
			Assert.Equal(2, mammoth.Head.Y); // Движение вверх
		}

		[Fact]
		public void GenerateNewTarget_ShouldSetDarkGrayColor()
		{
			// Arrange
			var random = new MockRandomGenerator(5, 5);
			var movement = new MammothMovement(10, 10, random);

			// Act
			var target = movement.CurrentTarget;

			// Assert
			Assert.Equal(ConsoleColor.DarkGray, target.Color);
		}

		[Fact]
		public void MoveMammoth_ShouldHandleSmallMap()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(3, 2); // Минимальные значения для 6x6
			var movement = new MammothMovement(6, 6, random);
			var mammoth = new Mammoth(3, 2, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth);

			// Assert
			Assert.Equal(3, movement.CurrentTarget.X); // Новая цель сгенерирована
			Assert.Equal(2, movement.CurrentTarget.Y);
		}

		[Fact]
		public void MoveMammoth_ShouldMoveMultipleSteps()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(5, 5);
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(3, 3, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth); // (3, 3) -> (4, 4)
			movement.MoveMammoth(mammoth); // (4, 4) -> (5, 5)

			// Assert
			Assert.Equal(5, mammoth.Head.X);
			Assert.Equal(5, mammoth.Head.Y);
		}

		[Fact]
		public void GenerateNewTarget_ShouldNotExceedMapWidth()
		{
			// Arrange
			var random = new MockRandomGenerator(100, 5); // Превышение ширины
			var movement = new MammothMovement(10, 10, random);

			// Act
			var target = movement.CurrentTarget;

			// Assert
			Assert.True(target.X < 7); // Должно быть меньше _mapWidth - 3
		}

		[Fact]
		public void GenerateNewTarget_ShouldNotExceedMapHeight()
		{
			// Arrange
			var random = new MockRandomGenerator(5, 100); // Превышение высоты
			var movement = new MammothMovement(10, 10, random);

			// Act
			var target = movement.CurrentTarget;

			// Assert
			Assert.True(target.Y < 6); // Должно быть меньше _mapHeight - 4
		}

		[Fact]
		public void MoveMammoth_ShouldGenerateNewTarget_AfterMultipleMoves()
		{
			// Arrange
			var view = new MockMammothView();
			var random = new MockRandomGenerator(5, 5, 4, 4);
			var movement = new MammothMovement(10, 10, random);
			var mammoth = new Mammoth(4, 4, ConsoleColor.Gray, ConsoleColor.White, view);

			// Act
			movement.MoveMammoth(mammoth); // (4, 4) -> (5, 5)
			movement.MoveMammoth(mammoth); // Достижение (5, 5), новая цель (4, 4)

			// Assert
			Assert.Equal(4, movement.CurrentTarget.X);
			Assert.Equal(4, movement.CurrentTarget.Y);
		}
	}
}