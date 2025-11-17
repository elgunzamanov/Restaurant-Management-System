using Restaurant_Management_System.enums;

namespace Restaurant_Management_System.models;

public class Person {
	public string? Username { get; init; }
	public string? Password { get; init; }
	public Role Role { get; init; }
}
