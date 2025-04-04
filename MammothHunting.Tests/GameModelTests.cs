using MammothHunting.Models;
using Xunit;

namespace MammothHunting.Tests
{
	public class GameModelTests
	{
		[Theory]
		[InlineData(0, 1000)]    // Начало игры: 0 секунд
		[InlineData(1, 990)]     // 1 секунда: 1000 - 1*10 = 990
		[InlineData(5, 950)]     // 5 секунд: 1000 - 5*10 = 950
		[InlineData(10, 900)]    // 10 секунд: 1000 - 10*10 = 900
		[InlineData(15, 850)]    // 15 секунд: 1000 - 15*10 = 850
		[InlineData(20, 800)]    // 20 секунд: 1000 - 20*10 = 800
		[InlineData(25, 750)]    // 25 секунд: 1000 - 25*10 = 750
		[InlineData(29, 710)]    // 29 секунд: 1000 - 29*10 = 710
		[InlineData(30, 700)]    // 30 секунд: 1000 - 30*10 = 700
		[InlineData(31, 690)]    // 31 секунда: 1000 - (30*10 + 1*10) = 690
		[InlineData(35, 650)]    // 35 секунд: 1000 - (30*10 + 5*10) = 650
		[InlineData(40, 600)]    // 40 секунд: 1000 - (30*10 + 10*10) = 600
		[InlineData(45, 550)]    // 45 секунд: 1000 - (30*10 + 15*10) = 550
		[InlineData(50, 500)]    // 50 секунд: 1000 - (30*10 + 20*10) = 500
		[InlineData(60, 400)]    // 60 секунд: 1000 - (30*10 + 30*10) = 400
		[InlineData(70, 300)]    // 70 секунд: 1000 - (30*10 + 40*10) = 300
		[InlineData(80, 200)]    // 80 секунд: 1000 - (30*10 + 50*10) = 200
		[InlineData(90, 100)]    // 90 секунд: 1000 - (30*10 + 60*10) = 100
		[InlineData(100, 0)]     // 100 секунд: 1000 - (30*10 + 70*10) = 0
		[InlineData(150, -500)]     // 150 секунд: счет не уходит ниже 0
		public void CalculateScore_ShouldCalculateCorrectScore(int elapsedSeconds, int expectedScore)
		{
			// Act
			int score = GameModel.CalculateScore(elapsedSeconds);

			// Assert
			Assert.Equal(expectedScore, score);
		}
	}
}