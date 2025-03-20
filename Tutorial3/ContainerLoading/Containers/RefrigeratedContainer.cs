using Tutorial3.Enums;

namespace Tutorial3.Containers;

public class RefrigeratedContainer : Container
{
    public RefrigeratedContainer(double height, double depth, double tareWeight, double maxPayload, ProductType productType, double maintainedTemperature) : base(height, depth, tareWeight, maxPayload, 'C')
    {
        if (TempretureValidator.isValid(productType, maxPayload))
        {
            ProductType = productType;
            MaintainedTemperature = maintainedTemperature;
        }
        else
        {
            throw new ArgumentException("Invalid tempreture for the product type.");
        }
    }
    public ProductType ProductType { get;}
    public double MaintainedTemperature { get;}
    
}