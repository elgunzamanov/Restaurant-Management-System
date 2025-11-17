using Restaurant_Management_System.enums;
using Restaurant_Management_System.models;
using Restaurant_Management_System.Services;
using Spectre.Console;
using Table = Restaurant_Management_System.models.Table;

namespace Restaurant_Management_System.services;

public static class UserService {
	public static void ReserveTable(
		Manager manager, Admin admin, User user,
		ReservationService reservationService
	) {
		Console.Clear();

		var table = SelectFreeTable(manager);
		if (table == null) return;

		var (setName, setPrice) = SelectSetIfAny();

		Console.Write("Enter reservation date and time (yyyy-MM-dd HH:mm): ");
		if (!DateTime.TryParse(Console.ReadLine(), out var reservationDate)) {
			Console.WriteLine("Invalid date/time format.");
			return;
		}

		var total = table.Price + setPrice;
		Console.WriteLine($"Total amount: {total} AZN");

		var paymentMethod = SelectPaymentMethod();

		var reservation = reservationService.CreateReservation(
			table.Id, user, total, setName, paymentMethod, reservationDate
		);

		PaymentService.ProcessPayment(reservation, admin);

		if (!reservation.IsPaid) return;
		table.IsReserved = true;
		Console.WriteLine("Reservation completed successfully!");
	}

	public static void CancelReservation(User user, ReservationService reservationService, Admin admin) {
		var reservations = reservationService.GetReservationsByUser(user);

		var enumerable = reservations.ToList();
		if (enumerable.Count == 0) {
			AnsiConsole.MarkupLine("[yellow]You have no active reservations.[/]");
			Thread.Sleep(1000);
			return;
		}

		var reservation = AnsiConsole.Prompt(
			new SelectionPrompt<Reservation>()
				.Title("[green]Select a reservation to cancel[/]:")
				.PageSize(10)
				.MoreChoicesText("[grey](Move up and down to choose)[/]")
				.AddChoices(enumerable)
				.UseConverter(r => $"Table #{r.TableId} on {r.Date:dd-MM-yyyy HH:mm} ({r.Amount:C})")
		);

		// NOTE: Refund if already paid
		if (reservation.IsPaid) {
			user.Balance += reservation.Amount; // NOTE: Give money back to user
			admin.Balance -= reservation.Amount; // NOTE: Deduct from admin
		}

		reservationService.CancelReservation(reservation);

		AnsiConsole.MarkupLine("[red]Reservation canceled and refund processed (if paid).[/]");
		Thread.Sleep(1000);
	}

	private static Table? SelectFreeTable(Manager manager) {
		var freeTables = manager.Tables.Where(t => !t.IsReserved).ToList();

		if (freeTables.Count == 0) {
			Console.WriteLine("No free tables available.");
			return null;
		}

		Console.WriteLine("Free tables:");
		foreach (var t in freeTables)
			Console.WriteLine($"ID: {t.Id}, Price: {t.Price}");

		Console.Write("Enter Table ID: ");
		if (!int.TryParse(Console.ReadLine(), out int tableId)) {
			Console.WriteLine("Invalid input.");
			return null;
		}

		var table = freeTables.FirstOrDefault(t => t.Id == tableId);
		if (table != null) return table;
		Console.WriteLine("Table not found.");
		return null;
	}

	private static (string? setName, decimal setPrice) SelectSetIfAny() {
		Console.Write("Do you want to order a set? (y/n): ");
		var ans = Console.ReadLine()?.ToLower();

		if (ans != "y") return (null, 0);

		if (Manager.Sets.Count == 0) {
			Console.WriteLine("No sets available.");
			return (null, 0);
		}

		Console.WriteLine("Available Sets:");
		foreach (var s in Manager.Sets) {
			Console.WriteLine($"{s.Id}) {s.Name} - {s.Price} AZN");
		}

		Console.Write("Choose set id: ");
		if (!int.TryParse(Console.ReadLine(), out var sid)) {
			Console.WriteLine("Invalid input.");
			return (null, 0);
		}

		var set = Manager.Sets.FirstOrDefault(s => s.Id == sid);
		return set == null ? (null, 0) : (set.Name, set.Price);
	}

	private static PaymentMethod SelectPaymentMethod() {
		Console.WriteLine("Payment method: 1) Cash  2) Card-to-Card");

		while (true) {
			var input = Console.ReadLine();

			switch (input) {
				case "1":
					return PaymentMethod.Cash;
				case "2":
					return PaymentMethod.CardToCard;
				default:
					Console.Write("Invalid. Try again (1/2): ");
					break;
			}
		}
	}
}
