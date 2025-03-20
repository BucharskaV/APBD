namespace Tutorial3.Containers;

public class HazardousContainer : Container
{
    public HazardousContainer(double height, double depth, double tareWeight, double maxPayload, char containerType) : base(height, depth, tareWeight, maxPayload, 'H')
    {
    }

    public override void LoadCargo(double mass)
    {
        base.LoadCargo(mass);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazardous situation occured for container {SerialNumber}: " +
                          $"Message: {message}");
    }

    public virtual void EmptyCargo()
    {
        throw new NotImplementedException();
    }
}