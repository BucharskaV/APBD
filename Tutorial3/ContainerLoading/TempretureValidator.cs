using Tutorial3.Enums;

namespace Tutorial3;

public static class TemperatureValidator
{
    public static bool IsValid(ProductType productType, double currentTemp)
    {
        var temperature = GetRequiredTemperature(productType);
        return currentTemp >= temperature;
    }

    public static double GetRequiredTemperature(ProductType productType)
    {
        var temperature = productType switch
        {
            ProductType.Bananas => 13.3,
            ProductType.Chocolate => 18,
            ProductType.Fish => 2,
            ProductType.Meat => -15,
            ProductType.IceCream => 18,
            ProductType.FrozenPizza => -30,
            ProductType.Cheese => 7.2,
            ProductType.Sausages => 5,
            ProductType.Butter => 20.5,
            ProductType.Eggs => 19,
            _ => throw new ArgumentException("Invalid product type")
        };
        return temperature;
    }
}