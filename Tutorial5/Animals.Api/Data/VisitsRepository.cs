using Animals.Api.Model;

namespace Animals.Api.Data;

public static class VisitsRepository
{
    public static List<Visit> Visits =
    [
        new () {Id = 1, Date = DateTime.Parse("2025-05-01"), AnimalId = 1, Price = 50},
        new () { Id = 2, Date = DateTime.Parse("2025-05-01"), AnimalId = 2, Description = "Vaccination", Price = 130 },
        new () { Id = 3, Date = DateTime.Parse("2025-05-04"), AnimalId = 3, Description = "Nail trimming", Price = 15 },
        new () { Id = 4, Date = DateTime.Parse("2025-05-05"), AnimalId = 15, Description = "Dental visit", Price = 80 },
        new () { Id = 5, Date = DateTime.Parse("2025-05-05"), AnimalId = 4, Description = "Vaccination", Price = 120 },
        new () { Id = 6, Date = DateTime.Parse("2025-05-10"), AnimalId = 5, Price = 65 },
        new () { Id = 7, Date = DateTime.Parse("2025-05-10"), AnimalId = 6, Description = "Vaccination", Price = 140 },
        new () { Id = 8, Date = DateTime.Parse("2025-05-10"), AnimalId = 7, Description = "Nail trimming", Price = 75 },
        new () { Id = 9, Date = DateTime.Parse("2025-05-13"), AnimalId = 8, Description = "Vaccination", Price = 135 },
        new () { Id = 10, Date = DateTime.Parse("2025-05-16"), AnimalId = 9, Description = "Nail trimming", Price = 25 },
        new () { Id = 11, Date = DateTime.Parse("2025-05-17"), AnimalId = 10, Price = 45 },
        new () { Id = 12, Date = DateTime.Parse("2025-05-18"), AnimalId = 11, Description = "Nail trimming", Price = 50 },
        new () { Id = 13, Date = DateTime.Parse("2025-04-21"), AnimalId = 12, Price = 15 },
        new () { Id = 14, Date = DateTime.Parse("2025-04-21"), AnimalId = 1, Price = 20 },
        new () { Id = 15, Date = DateTime.Parse("2025-04-22"), AnimalId = 14, Description = "Dental visit", Price = 25 }
    ];
}