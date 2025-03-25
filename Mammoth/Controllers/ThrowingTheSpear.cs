using MammothHunting.Models;

public class ThrowingTheSpear
{
	private readonly Hunter _hunter;
	private readonly Mammoth _mammoth;
	private Pixel _spear;
	private Direction _direction;
	private bool _isThrown;
	private readonly Action _onHit;
	public bool IsTargetHit { get; private set; }

	private const int SpearSpeedMs = 50;
	private Thread _spearThread;
	private readonly ConsoleColor _spearColor = ConsoleColor.DarkGray;

	public ThrowingTheSpear(Hunter hunter, Mammoth mammoth, Action onHit)
	{
		_hunter = hunter;
		_mammoth = mammoth;
		_isThrown = false;
		_onHit = onHit;
		_direction = Direction.Right;
		_spear = CalculateSpearPosition();
	}

	public void Throw(Direction direction)
	{
		if (_isThrown) return;

		_isThrown = true;
		IsTargetHit = false;
		_direction = direction;
		_spear = CalculateSpearPosition();

		_spearThread = new Thread(MoveSpear);
		_spearThread.Start();
	}

	public void Draw()
	{
		if (!_isThrown)
		{
			_spear = CalculateSpearPosition();
		}
		_spear.Draw();
	}

	// Метод движения копья 
	private Pixel CalculateSpearPosition()
	{
		return _direction switch
		{
			Direction.Left => new Pixel(_hunter.Head.X - 1, _hunter.Head.Y + 1, _spearColor),
			Direction.Right => new Pixel(_hunter.Head.X + 1, _hunter.Head.Y + 1, _spearColor),
			//Direction.Up => new Pixel(_hunter.Head.X, _hunter.Head.Y - 1, _spearColor),
			//Direction.Down => new Pixel(_hunter.Head.X, _hunter.Head.Y + 2, _spearColor),
			_ => new Pixel(_hunter.Head.X + 1, _hunter.Head.Y + 1, _spearColor)
		};
	}

	private void MoveSpear()
	{
		while (_isThrown)
		{
			_spear.Clear();

			int newX = _spear.X;
			int newY = _spear.Y;

			switch (_direction)
			{
				case Direction.Up: newY--; break;
				case Direction.Down: newY++; break;
				case Direction.Left: newX--; break;
				case Direction.Right: newX++; break;
			}

			if (newX <= 1 || newX >= _hunter.MapWidth - 1 ||
				newY <= 1 || newY >= _hunter.MapHeight - 1)
			{
				_isThrown = false;
				_spear.Clear();
				return;
			}

			_spear = new Pixel(newX, newY, _spear.Color);
			_spear.Draw();

			if (CheckMammothHit())
			{
				_isThrown = false;
				IsTargetHit = true;
				_spear.Clear();
				_onHit?.Invoke();
				return;
			}

			Thread.Sleep(SpearSpeedMs);
		}
	}

	private bool CheckMammothHit()
	{
		return _mammoth.Head.X == _spear.X && _mammoth.Head.Y == _spear.Y ||
			   _mammoth.Body.Exists(p => p.X == _spear.X && p.Y == _spear.Y) ||
			   _mammoth.Tusk.Exists(p => p.X == _spear.X && p.Y == _spear.Y);
	}
}