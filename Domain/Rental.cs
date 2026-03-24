namespace UniversityRental.Domain;

public class Rental
{
    public Guid Id { get; private set; }
    public User Renter { get; private set; }
    public Equipment RentedItem { get; private set; }
    public DateTime RentalDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ActualReturnDate { get; private set; } 
    public decimal PenaltyFee { get; private set; }

    public Rental(User renter, Equipment rentedItem, int daysToRent)
    {
        Id = Guid.NewGuid();
        Renter = renter;
        RentedItem = rentedItem;
        RentalDate = DateTime.Now;
        DueDate = RentalDate.AddDays(daysToRent);
        PenaltyFee = 0;
    }

    public void CompleteReturn(DateTime returnDate, decimal penalty)
    {
        ActualReturnDate = returnDate;
        PenaltyFee = penalty;
    }

    public bool IsOverdue(DateTime currentDate)
    {
        return ActualReturnDate == null && currentDate > DueDate;
    }
}