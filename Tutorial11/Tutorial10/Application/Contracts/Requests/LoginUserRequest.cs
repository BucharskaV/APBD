using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Application.Contracts.Requests;

public class LoginUserRequest
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}