namespace Tutorial3.Containers;

public class GasContainer : HazardousContainer
{
    public GasContainer(double height, double depth, double tareWeight, double maxPayload, double pressure) : base(height, depth, tareWeight, maxPayload, 'G')
    {
        Pressure = pressure > 0 ? pressure : throw new ArgumentException("Pressure must be greater than zero");
    }
    
    public double Pressure { get; }

    public override void EmptyCargo() => CargoMass *= 0.05;

    public override void ShowCargoInfo()
    {
        base.ShowCargoInfo();
        Console.WriteLine($"The container is hazardous");
        Console.WriteLine($"The pressure is: {Pressure}");
    }
}