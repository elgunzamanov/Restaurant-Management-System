using Restaurant_Management_System.models;
using Restaurant_Management_System.services;
using Spectre.Console;
using static System.Console;

namespace Restaurant_Management_System.Panels;

public static class ManagerPanel {
	public static void Show(Manager manager) {
		while (true) {
			AnsiConsole.Clear();
			AnsiConsole.Write(
				new FigletText("🧑‍💼  MANAGER PANEL")
					.Centered()
					.Color(Color.Yellow)
			);

			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[yellow]Select an option:[/]")
					.AddChoices(
						"Add Table",
						"Show Free Tables",
						"Show All Tables",
						"Manage Sets",
						"Logout"
					)
			);

			switch (choice) {
				case "Add Table":
					ManagerService.AddTable(manager);
					break;
				case "Show Free Tables":
					ManagerService.ShowFreeTables(manager);
					break;
				case "Show All Tables":
					ManagerService.ShowAllTables(manager);
					break;
				case "Manage Sets":
					ManagerService.ManageSets();
					break;
				case "Logout":
					return;
			}

			AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
			ReadKey(true);
		}
	}
}
