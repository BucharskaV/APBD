namespace Tutorial3.Containers;

public abstract class Container
{
    private string ContainerPrefix = "KON";
    private static int _nextContainer = 1;

    protected Container(double height, double depth, double tareWeight, double maxPayload, char containerType)
    {
        Height = height > 0 ? height : throw new ArgumentException();
        Depth = depth > 0 ? depth : throw new ArgumentException();
        TareWeight = tareWeight > 0 ? tareWeight : throw new ArgumentException();
        MaxPayload = maxPayload > 0 ? maxPayload : throw new ArgumentException();
        SerialNumber = $"{ContainerPrefix}--{containerType}--{_nextContainer++}";
    }
    public double Height { get; }
    public double Depth { get; }
    public double TareWeight { get; }
    public double MaxPayload { get; }
    public string SerialNumber { get; }
    public double CargoMass { get; protected set; }

    public virtual void LoadCargo(double mass)
    {
        if(mass <= 0) throw new OverfillException("The amount of mass must be greater than 0.");

        if (!CanLoadCargo(mass)) throw new OverflowException();
        CargoMass += mass;
    }
    protected virtual bool CanLoadCargo(double mass) => CargoMass + mass <= MaxPayload;
}