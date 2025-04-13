using System.ComponentModel.DataAnnotations;

namespace Animals.Api.Contracts.Requests;

public class CreateAnimalRequest
{
    [Required]
    [StringLength(256, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 256 characters")]
    public string Name { get; set; }
    [Required]
    [StringLength(256, MinimumLength = 2, ErrorMessage = "Category must be between 2 and 256 characters")]
    public string Category { get; set; }
    [Required]
    public double Weight { get; set; }
    [Required]
    [StringLength(256, MinimumLength = 2, ErrorMessage = "Fur color must be between 2 and 256 characters")]
    public string FurColor { get; set; }
}