namespace MammothHunting.Models
{
	public sealed class User : IUser
	{
		private int _point;
		private static readonly Lazy<User> _instanceHolder = new Lazy<User>(() => new User());

		public string Name { get; set; }

		public int Point
		{
			get => _point;
			set
			{
				if (value >= 0) _point = value;
			}
		}

		public static User Instance => _instanceHolder.Value;

		private User()
		{
			Name = string.Empty;
			_point = 0;
		}

		public void SaveScore(int score)
		{
			Point = score;
			var records = RecordsToFile.ImportJSON().ToList();
			records.Add(new Record(Name, Point));
			records = records.OrderByDescending(r => r.Score).Take(10).ToList(); // Топ-10
			RecordsToFile.ExportJSON(records);
		}
	}
}