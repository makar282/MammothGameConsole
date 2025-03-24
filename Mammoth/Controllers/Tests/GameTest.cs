// using System;
// using System.IO;
// using MammothHunting.Controllers;

// namespace Mammoth.Tests
// {
// 	class GameTest
// 	{
// 		static void Main(string[] args)
// 		{
// 			TestDrawBorder();
// 			TestReadMovement();
// 			TestEndGame();
// 			TestStartGame();
// 		}

// 		static void TestDrawBorder()
// 		{
// 			// Arrange
// 			var output = new StringWriter();
// 			Console.SetOut(output);

// 			// Act
// 			Game.DrawBorder();

// 			// Assert
// 			var consoleOutput = output.ToString();
// 			if (consoleOutput.Contains("═"))
// 			{
// 				Console.WriteLine("TestDrawBorder passed.");
// 			}
// 			else
// 			{
// 				Console.WriteLine("TestDrawBorder failed.");
// 			}
// 		}

// 		static void TestReadMovement()
// 		{
// 			// Arrange
// 			var input = new StringReader("\u001b[A"); // Симулируем нажатие стрелки вверх
// 			Console.SetIn(input);
// 			var currentDirection = Direction.Right;

// 			// Act
// 			var newDirection = Game.ReadMovement(currentDirection);

// 			// Assert
// 			if (newDirection == Direction.Up)
// 			{
// 				Console.WriteLine("TestReadMovement passed.");
// 			}
// 			else
// 			{
// 				Console.WriteLine("TestReadMovement failed.");
// 			}
// 		}

// 		static void TestEndGame()
// 		{
// 			// Arrange
// 			var output = new StringWriter();
// 			Console.SetOut(output);

// 			// Act
// 			Game.EndGame();

// 			// Assert
// 			var consoleOutput = output.ToString();
// 			if (consoleOutput.Contains("Game Over!"))
// 			{
// 				Console.WriteLine("TestEndGame passed.");
// 			}
// 			else
// 			{
// 				Console.WriteLine("TestEndGame failed.");
// 			}
// 		}

// 		static void TestStartGame()
// 		{
// 			// Arrange
// 			var output = new StringWriter();
// 			Console.SetOut(output);

// 			// Act
// 			var task = Task.Run(() => Game.StartGame());
// 			task.Wait(1000); // Запускаем игру и ждем 1 секунду

// 			// Assert
// 			var consoleOutput = output.ToString();
// 			if (consoleOutput.Contains("═"))
// 			{
// 				Console.WriteLine("TestStartGame passed.");
// 			}
// 			else
// 			{
// 				Console.WriteLine("TestStartGame failed.");
// 			}
// 		}
// 	}
// }