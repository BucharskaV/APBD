using Users.Api.Models;

namespace Users.Api.Data;

public static class OrdersRepository
{
    public static List<Order> Orders =
    [
        new()
        {
            Id = 1, UserId = 1, Products = new Dictionary<int, string> { { 101, "Laptop" }, { 102, "Mouse" } }
        },
        new()
        {
            Id = 2, UserId = 2, Products = new Dictionary<int, string> { { 103, "Tablet" }, { 104, "Keyboard" } }
        },
        new()
        {
            Id = 3, UserId = 3, Products = new Dictionary<int, string> { { 105, "Smartphone" }, { 106, "Charger" } }
        },
        new()
        {
            Id = 4, UserId = 4, Products = new Dictionary<int, string> { { 107, "Monitor" }, { 108, "HDMI Cable" } }
        },
        new()
        {
            Id = 5, UserId = 5, Products = new Dictionary<int, string> { { 109, "Printer" }, { 110, "Ink Cartridge" } }
        },
        new()
        {
            Id = 6, UserId = 6, Products = new Dictionary<int, string> { { 111, "Camera" }, { 112, "Memory Card" } }
        },
        new()
        {
            Id = 7, UserId = 7, Products = new Dictionary<int, string> { { 113, "Router" }, { 114, "Ethernet Cable" } }
        },
        new()
        {
            Id = 8, UserId = 8,
            Products = new Dictionary<int, string> { { 115, "Smartwatch" }, { 116, "Wireless Earbuds" } }
        },
        new()
        {
            Id = 9, UserId = 9, Products = new Dictionary<int, string> { { 117, "Gaming Chair" }, { 118, "Desk Lamp" } }
        },
        new()
        {
            Id = 10, UserId = 10, Products = new Dictionary<int, string> { { 119, "External SSD" }, { 120, "USB Hub" } }
        },
        new()
        {
            Id = 11, UserId = 11,
            Products = new Dictionary<int, string> { { 121, "Projector" }, { 122, "Projection Screen" } }
        },
        new()
        {
            Id = 12, UserId = 12,
            Products = new Dictionary<int, string> { { 123, "VR Headset" }, { 124, "Gaming Controller" } }
        },
        new()
        {
            Id = 13, UserId = 13,
            Products = new Dictionary<int, string> { { 125, "Bluetooth Speaker" }, { 126, "Power Bank" } }
        },
        new()
        {
            Id = 14, UserId = 14,
            Products = new Dictionary<int, string> { { 127, "Graphics Tablet" }, { 128, "Stylus Pen" } }
        },
        new()
        {
            Id = 15, UserId = 15,
            Products = new Dictionary<int, string> { { 129, "Fitness Tracker" }, { 130, "Yoga Mat" } }
        },
        new()
        {
            Id = 16, UserId = 16,
            Products = new Dictionary<int, string> { { 131, "Drone" }, { 132, "Extra Batteries" } }
        },
        new()
        {
            Id = 17, UserId = 17,
            Products = new Dictionary<int, string> { { 133, "Electric Scooter" }, { 134, "Helmet" } }
        },
        new()
        {
            Id = 18, UserId = 18,
            Products = new Dictionary<int, string> { { 135, "Kitchen Scale" }, { 136, "Coffee Grinder" } }
        },
        new()
        {
            Id = 19, UserId = 19,
            Products = new Dictionary<int, string> { { 137, "Air Purifier" }, { 138, "Replacement Filters" } }
        },
        new()
        {
            Id = 20, UserId = 20,
            Products = new Dictionary<int, string> { { 139, "Smart Lock" }, { 140, "Security Camera" } }
        }
    ];

    public static List<Order> GetAllOrders() => Orders;
}