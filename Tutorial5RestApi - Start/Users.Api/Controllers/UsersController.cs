using Microsoft.AspNetCore.Mvc;
using Users.Api.Data;
using Users.Api.Models;

namespace Users.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly List<User> _users = UsersRepository.Users;
    private readonly List<Order> _orders = OrdersRepository.Orders;

    #region CRUD

    // Get all users
    [HttpGet]
    public IActionResult GetAll()
    {
        return null;
    }

    // Get specific user by id
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        return null;
    }

    // Create a new user
    [HttpPost]
    public IActionResult Create()
    {
        return null;
    }

    // Update existing user
    [HttpPut("{id}")]
    public IActionResult Update()
    {
        return null;
    }

    // Delete provided user
    [HttpDelete("{id}")]
    public IActionResult Delete()
    {
        return null;
    }

    #endregion

    #region CRUD - Subresource

    // Get all orders for the provided user
    [HttpGet("{userId:}/orders")]
    public IActionResult GetAllUserOrders(string userId)
    {
        return null;
    }

    // Get specific order for the provided user
    [HttpGet("{userId:int}/orders/{orderId:int}")]
    public IActionResult GetAllUserOrders(int userId, int orderId)
    {
        return null;
    }

    #endregion
}