using Tutorial3.Interfaces;

namespace Tutorial3.Containers;

public class HazardousContainer : Container, IHazardNotifier
{
    public HazardousContainer(double height, double depth, double tareWeight, double maxPayload, char containerType) : base(height, depth, tareWeight, maxPayload, containerType)
    {
    }

    public override void LoadCargo(double mass)
    {
        if (mass <= 0) throw new ArgumentException("Mass must be greater than zero");

        if (!CanLoadCargo(mass))
        {
            NotifyHazard("The attempt of performing a dangerous operation");
            throw new OverflowException();
        }

        CargoMass += mass;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazardous situation occured for container {SerialNumber}: " +
                          $"Message: {message}");
    }

    public override void ShowCargoInfo()
    {
        base.ShowCargoInfo();
        Console.WriteLine($"The Container MIGHT be hazardous");
    }
}