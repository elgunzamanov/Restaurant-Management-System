using Restaurant_Management_System.models;
using Restaurant_Management_System.services;
using Restaurant_Management_System.Services;
using Spectre.Console;
using static System.Console;

namespace Restaurant_Management_System.Panels;

public static class UserPanel {
	public static void Show(
		List<Person> allUsers, Admin admin, User user,
		ReservationService reservationService, PaymentService paymentService
	) {
		while (true) {
			AnsiConsole.Clear();
			AnsiConsole.Write(
				new FigletText("👤  USER PANEL")
					.Centered()
					.Color(Color.Lime)
			);

			// NOTE: Manager must exist
			var manager = allUsers.OfType<Manager>().FirstOrDefault();
			if (manager == null) {
				AnsiConsole.MarkupLine("[red]⚠  No managers available currently.[/]");
				Thread.Sleep(1000);
				return;
			}

			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[green]Select an option[/]:")
					.AddChoices(
						"Reserve Table",
						"Cancel Reservation",
						"Logout"
					)
			);

			switch (choice) {
				case "Reserve Table":
					UserService.ReserveTable(manager, admin, user, reservationService);
					break;

				case "Cancel Reservation":
					UserService.CancelReservation(user, reservationService, admin);
					break;

				case "Logout":
					return;
			}

			AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
			ReadKey(true);
		}
	}
}
