using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MammothHunting.Models
{
  /// <summary>
  /// Импортер/экспортер рекордов в файл
  /// </summary>
  public class RecordsToFile
  {
    private const string FilePath = "highscores.json";

    /// <summary>
    /// Закрытый конструктор
    /// </summary>
    private RecordsToFile() { }

    /// <summary>
    /// Экспортирует рекорды в файл в формате JSON
    /// </summary>
    /// <param name="records">Рекорды</param>
    public static void ExportJSON(ICollection<Record> records)
    {
      string jsonString = JsonSerializer.Serialize(records);
      File.WriteAllText(FilePath, jsonString);
    }

    /// <summary>
    /// Импортирует рекорды из файла в коллекцию рекордов в формате JSON
    /// </summary>
    /// <returns>Коллекция рекордов</returns>
    public static ICollection<Record> ImportJSON()
    {
      if (File.Exists(FilePath))
      {
        string jsonString = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<ICollection<Record>>(jsonString) ?? new List<Record>();
      }
      return new List<Record>();
    }
  }
}