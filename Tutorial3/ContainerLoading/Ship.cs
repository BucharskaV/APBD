using Tutorial3.Containers;

namespace Tutorial3;

public class Ship
{
    public Ship(double maxSpeed, int maxContainerNumber, double maxWeight)
    {
        MaxSpeed = maxSpeed > 0 ? maxSpeed : throw new ArgumentException("The maximum speed must be greater than zero");
        MaxContainerNumber = maxContainerNumber > 0 ? maxContainerNumber : throw new ArgumentException("The maximum number of containers must be greater than zero");
        MaxWeight = maxWeight > 0 ? maxWeight : throw new ArgumentException("The maximum weight must be greater than zero");
        Containers = new Dictionary<string, Container>();  
    }

    public Dictionary<string, Container> Containers { get; set; }
    public double MaxSpeed { get; set; }
    public int MaxContainerNumber {get; set;}
    public double MaxWeight { get; set; }

    public double CurrentWeight => GetCurrentWeight();

    private double GetCurrentWeight() => Containers.Values.Sum(c => c.CargoMass);

    public void Load(Container container)
    {
        if (IsThereFreeSpace(1) && IsAppropriateWeight(container.CargoMass))
        {
            Containers.Add(container.SerialNumber, container);
        }
        else throw new Exception("Ship limits are exceeded");
    }

    public void Load(List<Container> containersToLoad)
    {
        if (IsThereFreeSpace(containersToLoad.Count) && IsAppropriateWeight(containersToLoad.Sum(c => c.CargoMass)))
        {
            foreach (var container in containersToLoad)
                Containers.Add(container.SerialNumber, container);
        }
        else throw new Exception("Ship limits are exceeded");
    }

    public void RemoveContainer(string serialNumber) => Containers.Remove(serialNumber);

    public void ReplaceContainer(string serialNumber, Container container)
    {
        if(!Containers.ContainsKey(serialNumber))
            throw new Exception("The serial number does not exist");
        if (IsAppropriateWeight(container.CargoMass))
        {
            Containers[serialNumber] = container;
        }
        else throw new Exception("Ship limits are exceeded");
    }

    public void TransferContainer(string serialNumber, Ship containerDest)
    {
        if(!Containers.ContainsKey(serialNumber))
            throw new Exception("The serial number does not exist");
        if (containerDest.IsThereFreeSpace(1) && containerDest.IsAppropriateWeight(Containers[serialNumber].CargoMass))
        {
            containerDest.Load(Containers[serialNumber]);
            RemoveContainer(serialNumber);
        }
        else throw new Exception("Ship limits are exceeded");
    }

    public virtual void ShowShipInfo()
    {
        Console.WriteLine("Info about ship");
        Console.WriteLine($"Max Speed: {MaxSpeed} knots");
        Console.WriteLine($"Max Containers: {MaxContainerNumber}");
        Console.WriteLine($"Max Weight Capacity: {MaxWeight} tons");
        Console.WriteLine($"Current Weight: {CurrentWeight} tons");
        Console.WriteLine($"Number of Containers: {Containers.Count}");
        Console.WriteLine("Cargos");
        foreach (var container in Containers.Values)
            container.ShowCargoInfo();
    }
    
    public bool IsThereFreeSpace(double num) => Containers.Count + num < MaxContainerNumber;
    //MaxWeight is stored in tons so we need to convert it to kilos
    public bool IsAppropriateWeight(double weight) => CurrentWeight + weight <= 1000*MaxWeight;
}