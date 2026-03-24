using UniversityRental.Domain;

namespace UniversityRental.Repositories;

public class UserRepository
{
    // A private list keeps our data safe from outside interference
    private readonly List<User> _users = new();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User? GetById(Guid id)
    {
        // LINQ makes it easy to find a specific user, or return null if not found
        return _users.FirstOrDefault(u => u.Id == id);
    }
}