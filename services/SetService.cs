using Restaurant_Management_System.models;
using Spectre.Console;

namespace Restaurant_Management_System.services;

public static class SetService {
	public static void AddSet() {
		var name = AnsiConsole.Ask<string>("Enter set name:");
		var price = AnsiConsole.Ask<decimal>("Enter set price:");
		var description = AnsiConsole.Ask<string>("Enter description (what’s included in this set):");

		var newSet = new Set {
			Id = Manager.Sets.Count > 0 ? Manager.Sets.Max(s => s.Id) + 1 : 1,
			Name = name,
			Price = price,
			Description = description
		};

		Manager.Sets.Add(newSet);
		AnsiConsole.MarkupLine("[green]✅  Set added successfully![/]");
		Thread.Sleep(500);
	}

	public static void ViewAllSets() {
		if (Manager.Sets.Count == 0) {
			AnsiConsole.MarkupLine("[red]No sets available.[/]");
			Thread.Sleep(1000);
			return;
		}

		var table = new Spectre.Console.Table();
		table.AddColumns("ID", "Name", "Price", "Description");

		foreach (var s in Manager.Sets) {
			table.AddRow(s.Id.ToString(), s.Name, s.Price.ToString("C"), s.Description);
		}

		AnsiConsole.Write(table);
		AnsiConsole.MarkupLine("[grey](Press any key to return)[/]");
		Console.ReadKey();
	}

	public static void EditSet() {
		if (Manager.Sets.Count == 0) {
			AnsiConsole.MarkupLine("[red]No sets available to edit.[/]");
			Thread.Sleep(1000);
			return;
		}

		var id = AnsiConsole.Ask<int>("Enter the ID of the set to edit:");
		var set = Manager.Sets.FirstOrDefault(s => s.Id == id);

		if (set == null) {
			AnsiConsole.MarkupLine("[red]Set not found![/]");
			Thread.Sleep(1000);
			return;
		}

		set.Name = AnsiConsole.Ask("Enter new name (leave empty to keep same):", set.Name);
		set.Price = AnsiConsole.Ask("Enter new price (current: " + set.Price + "):", set.Price);
		set.Description = AnsiConsole.Ask("Enter new description:", set.Description);

		AnsiConsole.MarkupLine("[green]✅  Set updated successfully![/]");
		Thread.Sleep(1000);
	}

	public static void DeleteSet() {
		if (Manager.Sets.Count == 0) {
			AnsiConsole.MarkupLine("[red]No sets available to delete.[/]");
			Thread.Sleep(1000);
			return;
		}

		var id = AnsiConsole.Ask<int>("Enter the ID of the set to delete:");
		var set = Manager.Sets.FirstOrDefault(s => s.Id == id);

		if (set == null) {
			AnsiConsole.MarkupLine("[red]Set not found![/]");
			Thread.Sleep(1000);
			return;
		}

		var confirm = AnsiConsole.Confirm($"Are you sure you want to delete [yellow]{set.Name}[/]?");
		if (confirm) {
			Manager.Sets.Remove(set);
			AnsiConsole.MarkupLine("[green]✅  Set deleted successfully![/]");
		}

		Thread.Sleep(1000);
	}
}
