namespace Tutorial3.Containers;

public class GasContainer : HazardousContainer
{
    public GasContainer(double height, double depth, double tareWeight, double maxPayload, double pressure) : base(height, depth, tareWeight, maxPayload, 'G')
    {
        Pressure = pressure > 0 ? pressure : throw new ArgumentOutOfRangeException("Pressure must be greater than zero"); ;
    }
    
    public double Pressure { get; }

    public override void EmptyCargo() => CargoMass *= 0.65;

}