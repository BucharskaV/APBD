using Users.Api.Models;

namespace Users.Api.Data;

public static class UsersRepository
{
    public static readonly List<User> Users =
    [
        new() { Id = 1, FullName = "Alice Johnson", Email = "alice.johnson@example.com" },
        new() { Id = 2, FullName = "Bob Smith", Email = "bob.smith@example.com" },
        new() { Id = 3, FullName = "Charlie Davis", Email = "charlie.davis@example.com" },
        new() { Id = 4, FullName = "Diana Evans", Email = "diana.evans@example.com" },
        new() { Id = 5, FullName = "Ethan Moore", Email = "ethan.moore@example.com" },
        new() { Id = 6, FullName = "Fiona Brown", Email = "fiona.brown@example.com" },
        new() { Id = 7, FullName = "George Clark", Email = "george.clark@example.com" },
        new() { Id = 8, FullName = "Hannah White", Email = "hannah.white@example.com" },
        new() { Id = 9, FullName = "Ian Wright", Email = "ian.wright@example.com" },
        new() { Id = 10, FullName = "Jane Miller", Email = "jane.miller@example.com" },
        new() { Id = 11, FullName = "Ken Wilson", Email = "ken.wilson@example.com" },
        new() { Id = 12, FullName = "Laura Scott", Email = "laura.scott@example.com" },
        new() { Id = 13, FullName = "Mike Adams", Email = "mike.adams@example.com" },
        new() { Id = 14, FullName = "Nina Turner", Email = "nina.turner@example.com" },
        new() { Id = 15, FullName = "Oscar Harris", Email = "oscar.harris@example.com" },
        new() { Id = 16, FullName = "Paul Carter", Email = "paul.carter@example.com" },
        new() { Id = 17, FullName = "Quinn Lewis", Email = "quinn.lewis@example.com" },
        new() { Id = 18, FullName = "Rachel Martinez", Email = "rachel.martinez@example.com" },
        new() { Id = 19, FullName = "Steve Walker", Email = "steve.walker@example.com" },
        new() { Id = 20, FullName = "Tina Young", Email = "tina.young@example.com" }
    ];
}