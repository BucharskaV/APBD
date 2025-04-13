using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Animals.Api.Contracts.Requests;

public class CreateVisitRequest
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int AnimalId { get; set; }
    [Required]
    public double Price { get; set; }
}