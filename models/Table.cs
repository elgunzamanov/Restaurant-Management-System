namespace Restaurant_Management_System.models;

public class Table {
	public int Id { get; init; }
	public bool IsReserved { get; set; }
	public decimal Price { get; init; } = 50;
}
