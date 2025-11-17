using Restaurant_Management_System.enums;

namespace Restaurant_Management_System.models;

public class Reservation {
	public int Id { get; set; }
	public int TableId { get; init; }
	public User? User { get; init; }
	public decimal Amount { get; init; }
	public PaymentMethod PaymentMethod { get; init; }
	public bool IsPaid { get; set; }
	public string? SetName { get; set; }
	public DateTime Date { get; init; }
}
