// NOTE: dotnet add package Spectre.Console

using Restaurant_Management_System.enums;
using Restaurant_Management_System.models;
using Restaurant_Management_System.Panels;
using Restaurant_Management_System.Services;
using Spectre.Console;
using static System.Console;

OutputEncoding = System.Text.Encoding.UTF8;

// TODO: DATA SETUP
var admin = new Admin { Username = "admin", Password = "admin", Role = Role.Admin };
var allUsers = new List<Person> { admin };
var reservationService = new ReservationService();
var paymentService = new PaymentService();

// TODO: MAIN LOOP
while (true) {
	AnsiConsole.Clear();
	ShowHeader();

	var selectedRole = PromptRole();
	var (username, password) = PromptCredentials();

	var person = AuthenticateUser(allUsers, username, password, selectedRole);

	if (person == null && selectedRole == Role.User) {
		person = HandleUserRegistration(allUsers, username, password);
		if (person == null) continue; // NOTE: registration canceled
	}

	if (person != null) {
		LaunchPanel(person, allUsers, admin, reservationService, paymentService);
	}
}

// TODO: METHODS
static void ShowHeader() {
	AnsiConsole.Write(
		new FigletText("🍽  RESTAURANT SYSTEM  🍴")
			.Centered()
			.Color(Color.Orange1)
	);
}

static Role PromptRole() {
	var choice = AnsiConsole.Prompt(
		new SelectionPrompt<string>()
			.Title("[green]Select your role[/]:")
			.AddChoices("Admin", "Manager", "User")
	);

	return choice switch {
		"Admin" => Role.Admin,
		"Manager" => Role.Manager,
		_ => Role.User
	};
}

static (string username, string password) PromptCredentials() {
	var username = AnsiConsole.Ask<string>("Enter [yellow]username[/]:");
	var password = AnsiConsole.Prompt(
		new TextPrompt<string>("Enter [yellow]password[/]:")
			.PromptStyle("red")
			.Secret()
	);

	return (username, password);
}

static Person? AuthenticateUser(List<Person> users, string username, string password, Role role) {
	return users.FirstOrDefault(
		u => u.Username == username &&
		u.Password == password &&
		u.Role == role
	);
}

static User? HandleUserRegistration(List<Person> users, string username, string password) {
	AnsiConsole.MarkupLine("[yellow]User not found.[/]");
	if (!AnsiConsole.Confirm("Do you want to register as a new user?")) {
		AnsiConsole.MarkupLine("[red]Login cancelled.[/]");
		Thread.Sleep(1000);
		return null;
	}

	if (users.Any(u => u.Username == username)) {
		AnsiConsole.MarkupLine("[red]Username already exists! Try again.[/]");
		Thread.Sleep(1000);
		return null;
	}

	var newUser = new User {
		Username = username,
		Password = password,
		Role = Role.User
	};
	users.Add(newUser);
	AnsiConsole.MarkupLine("[green]✅  Registration successful! You can now log in.[/]");
	Thread.Sleep(1000);

	return newUser;
}

static void LaunchPanel(
	Person person,
	List<Person> allUsers,
	Admin admin,
	ReservationService reservationService,
	PaymentService paymentService
) {
	switch (person.Role) {
		case Role.Admin:
			AdminPanel.Show(admin, allUsers);
			break;
		case Role.Manager:
			ManagerPanel.Show((Manager)person);
			break;
		case Role.User:
			UserPanel.Show(allUsers, admin, (User)person, reservationService, paymentService);
			break;
		default:
			throw new ArgumentOutOfRangeException();
	}
}
