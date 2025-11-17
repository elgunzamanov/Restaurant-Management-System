namespace Restaurant_Management_System.models;

public class Admin : Person {
	public decimal Balance { get; set; }
	public const string CardNumber = "1234 5678 1234 5678";
	public List<Manager> Managers { get; } = [];
	public List<Employee> Employees { get; } = [];
}
