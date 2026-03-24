using UniversityRental.Domain;
using UniversityRental.Repositories;

namespace UniversityRental.Services;

public class RentalService
{
    private readonly UserRepository _userRepository;
    private readonly EquipmentRepository _equipmentRepository;
    private readonly RentalRepository _rentalRepository;

    // We pass the repositories in via the constructor (Dependency Injection!)
    public RentalService(UserRepository userRepo, EquipmentRepository equipRepo, RentalRepository rentalRepo)
    {
        _userRepository = userRepo;
        _equipmentRepository = equipRepo;
        _rentalRepository = rentalRepo;
    }

    public Rental RentEquipment(Guid userId, Guid equipmentId, int daysToRent)
    {
        var user = _userRepository.GetById(userId);
        var equipment = _equipmentRepository.GetById(equipmentId);

        if (user == null) throw new Exception("User not found.");
        if (equipment == null) throw new Exception("Equipment not found.");

                if (!equipment.IsAvailable)
        {
            throw new Exception($"Equipment '{equipment.Name}' is currently unavailable.");
        }

        
        var activeRentalsCount = _rentalRepository.GetActiveByUser(userId).Count();
        if (activeRentalsCount >= user.MaxSimultaneousRentals)
        {
            throw new Exception($"{user.FirstName} has reached the max limit of {user.MaxSimultaneousRentals} rentals.");
        }

       
        var rental = new Rental(user, equipment, daysToRent);
        equipment.MarkAsUnavailable();
        _rentalRepository.Add(rental);

        return rental;
    }

    public void ReturnEquipment(Guid rentalId, DateTime returnDate)
    {
        var rental = _rentalRepository.GetAll().FirstOrDefault(r => r.Id == rentalId);
        if (rental == null) throw new Exception("Rental not found.");
        if (rental.ActualReturnDate != null) throw new Exception("Already returned.");

        
        decimal penalty = 0;
        if (returnDate > rental.DueDate)
        {
            int daysLate = (returnDate - rental.DueDate).Days;
            penalty = daysLate * 10.0m; // 'm' stands for decimal (money) in C#
        }

        rental.CompleteReturn(returnDate, penalty);
        rental.RentedItem.MarkAsAvailable();
    }
}