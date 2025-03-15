using System;
using System.Linq;
using static System.Console;
using Mammoth.Models;

namespace Mammoth.Views
{
	public class HighScoresMenu
	{
		public void SetHighScores()
		{
			Clear();
			WriteLine("=== Рекорды ===");

			var records = RecordsToFile.ImportJSON()
				 .OrderByDescending(r => r.Score)
				 .Take(10)
				 .ToList();

			for (int i = 0; i < records.Count; i++)
			{
				WriteLine($"{i + 1}. {records[i].Name} - {records[i].Score} очков");
			}

			WriteLine("================");
			WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");
			ReadKey();
		}
	}
}