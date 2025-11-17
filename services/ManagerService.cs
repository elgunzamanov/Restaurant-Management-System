using Restaurant_Management_System.models;
using Spectre.Console;
using Table = Restaurant_Management_System.models.Table;

namespace Restaurant_Management_System.services;

public static class ManagerService {
	public static void AddTable(Manager manager) {
		Console.Write("Enter table ID: ");
		var id = int.Parse(Console.ReadLine() ?? string.Empty);

		if (manager.Tables.Any(t => t.Id == id)) {
			Console.WriteLine("This table ID already exists!");
			return;
		}

		Console.Write("Enter table price: ");
		var price = decimal.Parse(Console.ReadLine() ?? string.Empty);

		manager.Tables.Add(
			new Table {
				Id = id,
				Price = price
			}
		);

		Console.WriteLine("Table added successfully!");
	}

	public static void ShowAllTables(Manager manager) {
		if (manager.Tables.Count == 0) {
			Console.WriteLine("No tables available.");
			return;
		}

		foreach (var table in manager.Tables) {
			Console.WriteLine(
				$"Table {table.Id} | " +
				$"Price: {table.Price} | " +
				$"Status: {(table.IsReserved ? "Reserved" : "Free")}"
			);
		}
	}

	public static void ShowFreeTables(Manager manager) {
		var freeTables = manager.Tables.Where(t => !t.IsReserved).ToList();

		if (freeTables.Count == 0) {
			Console.WriteLine("No free tables available.");
			return;
		}

		foreach (var table in freeTables) {
			Console.WriteLine($"Table {table.Id} | Price: {table.Price}");
		}
	}

	public static void ManageSets() {
		while (true) {
			AnsiConsole.Clear();
			var action = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Manage Sets[/]")
					.AddChoices(
						"Add Set",
						"View All Sets",
						"Edit Set",
						"Delete Set",
						"Back"
					)
			);

			switch (action) {
				case "Add Set":
					SetService.AddSet();
					break;
				case "View All Sets":
					SetService.ViewAllSets();
					break;
				case "Edit Set":
					SetService.EditSet();
					break;
				case "Delete Set":
					SetService.DeleteSet();
					break;
				case "Back":
					return;
			}
		}
	}
}
