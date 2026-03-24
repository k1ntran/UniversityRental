using UniversityRental.Domain;

namespace UniversityRental.Repositories;

public class RentalRepository
{
    private readonly List<Rental> _rentals = new();

    public void Add(Rental rental)
    {
        _rentals.Add(rental);
    }

    public IEnumerable<Rental> GetAll()
    {
        return _rentals;
    }

    // Finds rentals that belong to the user AND haven't been returned yet
    public IEnumerable<Rental> GetActiveByUser(Guid userId)
    {
        return _rentals.Where(r => r.Renter.Id == userId && r.ActualReturnDate == null);
    }

    // Finds rentals that are past their due date
    public IEnumerable<Rental> GetOverdue(DateTime currentDate)
    {
        return _rentals.Where(r => r.IsOverdue(currentDate));
    }
}