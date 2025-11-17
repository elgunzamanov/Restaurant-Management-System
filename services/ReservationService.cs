using Restaurant_Management_System.enums;
using Restaurant_Management_System.models;

namespace Restaurant_Management_System.Services;

public class ReservationService {
	private readonly List<Reservation> _reservations = [];
	private static int _idCounter = 1;

	public Reservation CreateReservation(
		int tableId, User user, decimal amount, string? setName, PaymentMethod method, DateTime date
	) {
		var reservation = new Reservation {
			Id = _idCounter++,
			TableId = tableId,
			User = user,
			Amount = amount,
			SetName = setName,
			PaymentMethod = method,
			Date = date,
			IsPaid = false
		};

		_reservations.Add(reservation);
		return reservation;
	}

	public IEnumerable<Reservation> GetReservationsByUser(User user) =>
		_reservations.Where(r => r.User == user);

	public void CancelReservation(Reservation reservation) {
		_reservations.Remove(reservation);
	}
}
