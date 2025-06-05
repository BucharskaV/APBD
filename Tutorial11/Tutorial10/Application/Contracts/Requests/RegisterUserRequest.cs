using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Application.Contracts.Requests;

public class RegisterUserRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}