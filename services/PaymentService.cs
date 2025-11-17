using Restaurant_Management_System.enums;
using Restaurant_Management_System.models;

namespace Restaurant_Management_System.Services;

public class PaymentService {
	public static void ProcessPayment(Reservation reservation, Admin admin) {
		switch (reservation.PaymentMethod) {
			case PaymentMethod.Cash:
				reservation.IsPaid = true;
				admin.Balance += reservation.Amount;
				Console.WriteLine("Payment successful (cash).");
				break;

			case PaymentMethod.CardToCard:
				Console.WriteLine($"Send {reservation.Amount} AZN to admin card: {Admin.CardNumber}");
				Console.Write("Have you sent the money? (y/n): ");

				var ans = Console.ReadLine();

				if (ans?.ToLower() == "y") {
					reservation.IsPaid = true;
					admin.Balance += reservation.Amount;
					Console.WriteLine("Payment successful (card-to-card).");
				} else {
					Console.WriteLine("Payment cancelled.");
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
