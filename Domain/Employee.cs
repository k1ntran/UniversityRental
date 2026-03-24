namespace UniversityRental.Domain;

public class Employee : User
{
    public string Department { get; private set; }
    
    // Fulfilling the abstract requirement from the User base class
    public override int MaxSimultaneousRentals => 5;

    public Employee(string firstName, string lastName, string department) 
        : base(firstName, lastName)
    {
        Department = department;
    }
}