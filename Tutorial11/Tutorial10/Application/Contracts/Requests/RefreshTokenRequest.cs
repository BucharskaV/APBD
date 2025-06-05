using System.ComponentModel.DataAnnotations;

namespace Tutorial10.Application.Contracts.Requests;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; }
}