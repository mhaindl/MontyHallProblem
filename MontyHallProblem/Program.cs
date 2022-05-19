namespace MontyHallProblem;

public static class Program
{
	private const int _numberOfGames = 1_000_000;

	public static void Main()
	{
		// Create three doors.
		var doors = new[]
		{
			new Door(1, Price.Goat),
			new Door(2, Price.Goat),
			new Door(3, Price.Car)
		}.ToImmutableArray();

		// Case 1: Pick a random door without changing it afterwards. The chance of winning is 1/3.
		PlayGamesWithoutChange(doors);

		// Case 2: Pick a random door but change the door after the host opened one of the two doors containing a goat. The chance of winning is 2/3.
		PlayGamesWithChange(doors);

		Console.ReadLine();
	}

	private static void PlayGamesWithoutChange(ImmutableArray<Door> doors)
	{
		var gamesWon = 0;

		for (var numberOfGame = 1; numberOfGame <= _numberOfGames; numberOfGame++)
		{
			var doorPickedByPlayer = PickRandomDoor(doors);

			if (doorPickedByPlayer.Price == Price.Car)
				gamesWon++;
		}

		Console.WriteLine($"The player won {gamesWon} out of {_numberOfGames} total games without changing their mind.");
	}

	private static void PlayGamesWithChange(ImmutableArray<Door> doors)
	{
		var gamesWon = 0;

		for (var numberOfGame = 1; numberOfGame <= _numberOfGames; numberOfGame++)
		{
			var doorPickedByPlayer = PickRandomDoor(doors);
			// This can be one of two possible goat doors. No randomness is needed here.
			var goatDoorPickedByHost = doors.Except(doorPickedByPlayer).First(x => x.Price == Price.Goat);
			// This can only be the one other door.
			var changedDoorPickedByPlayer = doors.Except(doorPickedByPlayer, goatDoorPickedByHost).Single();

			if (changedDoorPickedByPlayer.Price == Price.Car)
				gamesWon++;
		}

		Console.WriteLine($"The player won {gamesWon} out of {_numberOfGames} total games with changing their mind.");
	}

	private static Door PickRandomDoor(ImmutableArray<Door> doors) =>
		doors[RandomNumberGenerator.GetInt32(doors.Length)];
}

public record Door(int DoorNumer, Price Price);
