using Restaurant_Management_System.enums;
using Restaurant_Management_System.models;

namespace Restaurant_Management_System.services;

public static class AdminService {
	public static void AddManager(Admin admin, List<Person> allUsers) {
		Console.Write("Enter new manager username: ");
		var username = Console.ReadLine();

		if (allUsers.Any(u => u.Username == username)) {
			Console.WriteLine("This username already exists!");
			return;
		}

		Console.Write("Enter password: ");
		var password = Console.ReadLine();

		var manager = new Manager { Username = username, Password = password, Role = Role.Manager };
		admin.Managers.Add(manager);
		allUsers.Add(manager);

		Console.WriteLine("Manager added successfully!");
	}

	public static void RemoveManager(Admin admin, List<Person> allUsers) {
		Console.Write("Enter manager username to remove: ");
		var username = Console.ReadLine();

		var manager = admin.Managers.FirstOrDefault(m => m.Username == username);
		if (manager == null) {
			Console.WriteLine("Manager not found!");
			return;
		}

		admin.Managers.Remove(manager);
		allUsers.RemoveAll(u => u.Username == username);

		Console.WriteLine("Manager removed successfully!");
	}

	public static void ShowManagers(Admin admin) {
		if (admin.Managers.Count == 0) {
			Console.WriteLine("No managers available.");
			return;
		}

		Console.WriteLine("Managers:");
		admin.Managers.ForEach(m => Console.WriteLine($"- {m.Username}"));
	}

	public static void AddEmployee(Admin admin) {
		Console.Write("Enter employee username: ");
		var username = Console.ReadLine();

		Console.Write("Enter password: ");
		var password = Console.ReadLine();

		Console.Write("Enter position: ");
		var position = Console.ReadLine();

		admin.Employees.Add(
			new Employee {
				Username = username,
				Password = password,
				Position = position,
				Role = Role.User
			}
		);

		Console.WriteLine("Employee added successfully!");
	}

	public static void ShowEmployees(Admin admin) {
		if (admin.Employees.Count == 0) {
			Console.WriteLine("No employees available.");
			return;
		}

		Console.WriteLine("Employees:");
		admin.Employees.ForEach(e => Console.WriteLine($"- {e.Username} | Position: {e.Position}"));
	}

	public static void ShowSummary(Admin admin) {
		Console.WriteLine($"Admin balance: {admin.Balance} $");
		Console.WriteLine($"Number of managers: {admin.Managers.Count}");
		Console.WriteLine($"Number of employees: {admin.Employees.Count}");
	}
}
