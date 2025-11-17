using Restaurant_Management_System.models;
using Restaurant_Management_System.services;
using Spectre.Console;
using static System.Console;

namespace Restaurant_Management_System.Panels;

public static class AdminPanel {
	public static void Show(Admin admin, List<Person> allUsers) {
		while (true) {
			AnsiConsole.Clear();
			AnsiConsole.Write(
				new FigletText("👑  ADMIN PANEL")
					.Centered()
					.Color(Color.CadetBlue)
			);

			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("[green]Select an option[/]:")
					.AddChoices(
						"Add Manager",
						"Remove Manager",
						"Show Managers",
						"Add Employee",
						"Show Employees",
						"Show Summary",
						"Logout"
					)
			);

			switch (choice) {
				case "Add Manager": AdminService.AddManager(admin, allUsers); break;
				case "Remove Manager": AdminService.RemoveManager(admin, allUsers); break;
				case "Show Managers": AdminService.ShowManagers(admin); break;
				case "Add Employee": AdminService.AddEmployee(admin); break;
				case "Show Employees": AdminService.ShowEmployees(admin); break;
				case "Show Summary": AdminService.ShowSummary(admin); break;
				case "Logout": return;
			}

			AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
			ReadKey(true);
		}
	}
}
