using Animals.Api.Contracts.Requests;
using Animals.Api.Data;
using Animals.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Animals.Api.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    private readonly List<Animal> _animals = AnimalsRepository.Animals;
    
    //Retrieve a list of animals
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_animals);
    }
    
    //retrieve a specific animal by id
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if(animal is null) return NotFound();
        return Ok(animal);
    }
    
    //add an animal
    [HttpPost]
    public IActionResult Create(CreateAnimalRequest request)
    {
        var id = _animals.Max(a => a.Id) + 1;
        var animal = new Animal {Id = id, Name = request.Name, Category = request.Category, Weight = request.Weight, FurColor = request.FurColor};
        _animals.Add(animal);
        return CreatedAtAction(nameof(Get), new {id = id}, animal);
    }
    
    //edit an animal
    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateAnimalRequest request)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if(animal is null) return NotFound();
        animal.Name = request.Name;
        animal.Category = request.Category;
        animal.Weight = request.Weight;
        animal.FurColor = request.FurColor;
        return Ok(animal);
    }
    
    //delete an animal
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if(animal is null) return NotFound();
        _animals.Remove(animal);
        return Ok(animal);
    }
}