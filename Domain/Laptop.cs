namespace UniversityRental.Domain;

public class Laptop : Equipment
{
    public string OperatingSystem { get; private set; }
    public int RamGigabytes { get; private set; }

    public Laptop(string name, string operatingSystem, int ramGigabytes) 
        : base(name) 
    {
        OperatingSystem = operatingSystem;
        RamGigabytes = ramGigabytes;
    }
}