using UniversityRental.Domain;
using UniversityRental.Repositories;
using UniversityRental.Services;

Console.WriteLine("=== UNIVERSITY EQUIPMENT RENTAL SYSTEM ===");

var userRepo = new UserRepository();
var equipmentRepo = new EquipmentRepository();
var rentalRepo = new RentalRepository();
var rentalService = new RentalService(userRepo, equipmentRepo, rentalRepo);

var laptop = new Laptop("Dell XPS 15", "Windows 11", 16);
var projector = new Projector("Epson Pro", "1080p", 3000);
var camera = new Camera("Canon EOS R5", 45.0, true);

equipmentRepo.Add(laptop);
equipmentRepo.Add(projector);
equipmentRepo.Add(camera);
Console.WriteLine("\n[+] Added 3 equipment items (Laptop, Projector, Camera).");

var student = new Student("Alice", "Smith", "S12345"); // Max 2 rentals
var employee = new Employee("Dr. Bob", "Jones", "Computer Science"); // Max 5 rentals

userRepo.Add(student);
userRepo.Add(employee);
Console.WriteLine("[+] Added 2 users (Student, Employee).");

Console.WriteLine("\n--- Processing Rentals ---");
try
{
    rentalService.RentEquipment(student.Id, laptop.Id, 5);
    Console.WriteLine($"[SUCCESS] {student.FirstName} rented {laptop.Name}.");
}
catch (Exception ex) { Console.WriteLine($"[ERROR] {ex.Message}"); }

try
{
    Console.WriteLine($"\nAttempting to rent the already-rented {laptop.Name} to {employee.FirstName}...");
    rentalService.RentEquipment(employee.Id, laptop.Id, 2);
}
catch (Exception ex) 
{ 
    Console.WriteLine($"[BLOCKED - EXPECTED] {ex.Message}"); 
}

try
{
    Console.WriteLine($"\nAttempting to exceed {student.FirstName}'s rental limit (Max {student.MaxSimultaneousRentals})...");
    rentalService.RentEquipment(student.Id, projector.Id, 3); // 2nd rental (Allowed)
    Console.WriteLine($"[SUCCESS] {student.FirstName} rented {projector.Name}.");
    
    rentalService.RentEquipment(student.Id, camera.Id, 2); // 3rd rental (Should Fail!)
}
catch (Exception ex) 
{ 
    Console.WriteLine($"[BLOCKED - EXPECTED] {ex.Message}"); 
}


Console.WriteLine("\n--- Processing Returns ---");
var activeRentals = rentalRepo.GetActiveByUser(student.Id).ToList();


var onTimeRental = activeRentals[0];
rentalService.ReturnEquipment(onTimeRental.Id, DateTime.Now.AddDays(2)); // Returned early
Console.WriteLine($"[RETURN] {onTimeRental.RentedItem.Name} returned on time. Penalty: ${onTimeRental.PenaltyFee}");


var lateRental = activeRentals[1];
rentalService.ReturnEquipment(lateRental.Id, DateTime.Now.AddDays(5)); // Returned 2 days late!
Console.WriteLine($"[RETURN] {lateRental.RentedItem.Name} returned LATE. Penalty: ${lateRental.PenaltyFee}");



Console.WriteLine("\n=== FINAL SYSTEM REPORT ===");
Console.WriteLine("EQUIPMENT STATUS:");
foreach (var eq in equipmentRepo.GetAll())
{
    string status = eq.IsAvailable ? "AVAILABLE" : "RENTED";
    Console.WriteLine($" - {eq.Name} ({eq.GetType().Name}): {status}");
}

Console.WriteLine("\nALL RENTAL TRANSACTIONS:");
foreach (var rental in rentalRepo.GetAll())
{
    string returnStatus = rental.ActualReturnDate.HasValue ? $"Returned (Penalty: ${rental.PenaltyFee})" : "Active";
    Console.WriteLine($" - {rental.Renter.FirstName} rented {rental.RentedItem.Name} | Status: {returnStatus}");
}