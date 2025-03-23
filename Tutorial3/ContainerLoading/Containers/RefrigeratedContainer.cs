using Tutorial3.Enums;

namespace Tutorial3.Containers;

public class RefrigeratedContainer : Container
{
    public RefrigeratedContainer(double height, double depth, double tareWeight, double maxPayload, ProductType productType, double maintainedTemperature) : base(height, depth, tareWeight, maxPayload, 'R')
    {
        if (TemperatureValidator.IsValid(productType, maintainedTemperature))
        {
            ProductType = productType;
            MaintainedTemperature = maintainedTemperature;
        }
        else
        {
            throw new ArgumentException("Invalid temperature for the product type.");
        }
    }
    public ProductType ProductType { get;}
    public double MaintainedTemperature { get;}

    public override void ShowCargoInfo()
    {
        base.ShowCargoInfo();
        Console.WriteLine($"The product type: {ProductType}");
        Console.WriteLine($"The maintained temperature: {MaintainedTemperature} C");
    }
}