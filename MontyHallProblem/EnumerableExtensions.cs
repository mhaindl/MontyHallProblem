namespace MontyHallProblem;

public static class EnumerableExtensions
{
	// This just eliminates the array creation for the caller and helps to focus on the functional problem.
	public static IEnumerable<Door> Except(this IEnumerable<Door> doors, params Door[] doorToExclude) =>
		doors.Except((IEnumerable<Door>)doorToExclude);
}
