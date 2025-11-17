namespace Restaurant_Management_System.models;

public class Manager : Person {
	public List<Table> Tables { get; } = [];
	public static List<Set> Sets { get; } = [];
}
