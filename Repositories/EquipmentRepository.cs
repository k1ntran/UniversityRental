using UniversityRental.Domain;

namespace UniversityRental.Repositories;

public class EquipmentRepository
{
    private readonly List<Equipment> _equipment = new();

    public void Add(Equipment item)
    {
        _equipment.Add(item);
    }

    public IEnumerable<Equipment> GetAll()
    {
        return _equipment;
    }

    public IEnumerable<Equipment> GetAvailable()
    {
        // Only return equipment where IsAvailable is true
        return _equipment.Where(e => e.IsAvailable);
    }

    public Equipment? GetById(Guid id)
    {
        return _equipment.FirstOrDefault(e => e.Id == id);
    }
}