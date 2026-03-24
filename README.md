\# University Equipment Rental System



A C# console application designed to manage university equipment rentals with specific business rules for students and employees.



\# How to Run

1\. Ensure you have the .NET SDK (version 10.0 or later) installed.

2\. Open a terminal in the project root folder.

3\. Run: `dotnet run`

4\. The application will execute a pre-defined demonstration scenario showing successful rentals, blocked operations (due to limits or availability), and penalty calculations.



\# Design Decisions \& Justifications



This project demonstrates a transition from basic OOP to a structured architecture focused on \*\*Separation of Concerns\*\*.



\# 1. High Cohesion \& Single Responsibility (SRP)

Instead of putting all logic in `Program.cs`, the system is divided into three distinct layers:

\* Domain Layer: Holds the data and basic state (e.g., `Equipment.cs` manages its own `IsAvailable` status).

\* Repository Layer: Classes like `UserRepository` and `EquipmentRepository` have one job: managing the collection of objects. This isolates data storage from the rest of the app.

\* Service Layer (`RentalService.cs`): This is the only place where business rules live (e.g., "Can this user rent this?"). By keeping rules here, the code is easier to maintain and modify.



\# 2. Low Coupling

By using a Repository Pattern, the `RentalService` doesn't care \*how\* users are stored (whether in a List, a JSON file, or a Database). It only cares that it can call `GetById()`. This makes the system modular and easy to upgrade.



\# 3. Conscious Use of Inheritance

Inheritance was chosen over composition here because of the strong "is-a" relationship in the domain:

\* Equipment Specialization: `Laptop`, `Camera`, and `Projector` inherit from the abstract `Equipment` class to share common traits (ID, Name) while providing unique technical specifications.

\* User Limits: I used an abstract property `MaxSimultaneousRentals` in the base `User` class. This forces `Student` and `Employee` to define their own limits (2 vs 5). This allows the `RentalService` to check limits polymorphically without needing complex `if-else` checks for user types.



\# 4. Business Rules \& Penalties

\* Limits: Enforced in the `RentalService` before any rental object is created.

\* Penalties: Defined as a simple rule: $10 per day late. This logic is encapsulated in the `ReturnEquipment` method, making it easy to change the "rate" in one single place.

