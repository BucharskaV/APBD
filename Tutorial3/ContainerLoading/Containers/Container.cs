using System.ComponentModel.Design;

namespace Tutorial3.Containers;

public abstract class Container
{
    private const string ContainerPrefix = "KON";
    private static int _nextContainer = 1;

    protected Container(double height, double depth, double tareWeight, double maxPayload, char containerType)
    {
        Height = height > 0 ? height : throw new ArgumentException("Height must be greater than zero");
        Depth = depth > 0 ? depth : throw new ArgumentException("Depth must be greater than zero");
        TareWeight = tareWeight > 0 ? tareWeight : throw new ArgumentException("TareWeight must be greater than zero");
        MaxPayload = maxPayload > 0 ? maxPayload : throw new ArgumentException("MaxPayload must be greater than zero");
        SerialNumber = $"{ContainerPrefix}-{containerType}-{_nextContainer++}";
    }
    public double Height { get; }
    public double Depth { get; }
    public double TareWeight { get; }
    public double MaxPayload { get; }
    public string SerialNumber { get; }
    public double CargoMass { get; protected set; }

    public virtual void LoadCargo(double mass)
    {
        if(mass <= 0) throw new OverfillException("Cargo weight must be greater than 0.");
        if (!CanLoadCargo(mass)) throw new OverfillException();
        CargoMass += mass;
    }
    
    public virtual void EmptyCargo() => CargoMass = 0;
    
    protected virtual bool CanLoadCargo(double mass) => CargoMass + mass <= MaxPayload;

    public virtual void ShowCargoInfo()
    {
        Console.WriteLine($"Info about container '{SerialNumber}':");
        Console.WriteLine($"Cargo mass: {CargoMass}");
        Console.WriteLine($"The weight of the container itself: {TareWeight} kg");
        Console.WriteLine($"Depth: {Depth} cm");
        Console.WriteLine($"The maximum payload: {MaxPayload} kg");
    }
}