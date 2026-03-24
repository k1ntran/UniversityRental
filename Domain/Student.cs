namespace UniversityRental.Domain;

public class Student : User
{
    public string StudentIdNumber { get; private set; }
    
    // Fulfilling the abstract requirement from the User base class
    public override int MaxSimultaneousRentals => 2; 

    public Student(string firstName, string lastName, string studentIdNumber) 
        : base(firstName, lastName)
    {
        StudentIdNumber = studentIdNumber;
    }
}