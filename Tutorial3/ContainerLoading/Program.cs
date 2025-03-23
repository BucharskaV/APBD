using Tutorial3;
using Tutorial3.Containers;
using Tutorial3.Enums;

class Program
{
    public static void Main(string[] args)
    {
        var liquidContainer = new LiquidContainer(15, 5, 300, 2000, false);
        var gasContainer = new GasContainer(10, 5, 250, 1500, 10);
        var refrigeratedContainerChocolate = new RefrigeratedContainer(7, 3, 100, 1000, ProductType.Chocolate, 19);
        
        liquidContainer.LoadCargo(650);
        gasContainer.LoadCargo(750);
        refrigeratedContainerChocolate.LoadCargo(900);

        var ship1 = new Ship(25, 20, 5000);
        ship1.Load(liquidContainer);
        var listContainers = new List<Container>{gasContainer, refrigeratedContainerChocolate};
        ship1.Load(listContainers);
        
        ship1.ShowShipInfo();
        
        ship1.RemoveContainer(liquidContainer.SerialNumber);
        
        gasContainer.EmptyCargo();
        
        var refrigeratedContainerBananas = new RefrigeratedContainer(7, 3, 100, 1000, ProductType.Bananas, 15);
        ship1.ReplaceContainer(refrigeratedContainerChocolate.SerialNumber, refrigeratedContainerBananas);
        
        var ship2 = new Ship(20, 15, 3500);
        ship1.TransferContainer(gasContainer.SerialNumber, ship2);
        
        ship1.ShowShipInfo();
        ship2.ShowShipInfo();
    }
}