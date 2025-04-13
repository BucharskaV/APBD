using Animals.Api.Contracts.Requests;
using Animals.Api.Data;
using Animals.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Animals.Api.Controllers;
[ApiController]
[Route("api/animals/{animalId}/visits")]
public class VisitsControllers : ControllerBase
{
    private readonly List<Animal> _animals = AnimalsRepository.Animals;
    private readonly List<Visit> _visits = VisitsRepository.Visits;
    
    //retrieve a list of visits associated with a given animal
    [HttpGet("{animalId:}/visits")]
    public IActionResult GetVisitsByAnimal(int animalId)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == animalId);
        if(animal is null) return NotFound();
        var visits = _visits.Where(v => v.AnimalId == animalId);
        return Ok(visits.ToList());
    }
    
    //add new visit
    [HttpPost]
    public IActionResult Create(CreateVisitRequest request)
    {
        var id = _visits.Max(v => v.Id) + 1;
        var visit = new Visit { Id = id, Date = request.Date, AnimalId = request.AnimalId, Price = request.Price };
        _visits.Add(visit);
        return CreatedAtAction(nameof(GetVisitsByAnimal), new { id }, visit);
    }
}