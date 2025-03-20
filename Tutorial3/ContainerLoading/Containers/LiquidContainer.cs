namespace Tutorial3.Containers;

public class LiquidContainer : HazardousContainer
{
    private readonly double _allowedMaxPayload;
    public LiquidContainer(double height, double depth, double tareWeight, double maxPayload, bool IsCargoHazardous) : base(height, depth, tareWeight, maxPayload, 'L')
    {
        IsCargoHazardous = IsCargoHazardous;
        var capacityLimit = IsCargoHazardous ? 0.5 : 0.9;
        _allowedMaxPayload = maxPayload * capacityLimit;
    }
    public bool IsCargoHazardous { get;}
    
    protected override bool CanLoadCargo(double mass) => CargoMass >= _allowedMaxPayload;
}