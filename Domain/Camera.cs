namespace UniversityRental.Domain;

public class Camera : Equipment
{
    public double Megapixels { get; private set; }
    public bool IncludesLens { get; private set; }

    public Camera(string name, double megapixels, bool includesLens) 
        : base(name)
    {
        Megapixels = megapixels;
        IncludesLens = includesLens;
    }
}