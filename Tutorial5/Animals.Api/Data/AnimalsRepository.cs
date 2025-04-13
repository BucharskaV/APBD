using Animals.Api.Model;

namespace Animals.Api.Data;

public static class AnimalsRepository
{
    public static List<Animal> Animals = 
    [
        new() {Id = 1, Name = "Bob", Category = "Dog", Weight = 15, FurColor = "Brown"},
        new() {Id = 2, Name = "Daisy", Category = "Dog", Weight = 10, FurColor = "Black"},
        new() {Id = 3, Name = "Chloe", Category = "Dog", Weight = 8, FurColor = "Brown"},
        new() {Id = 4, Name = "Oscar", Category = "Dog", Weight = 22, FurColor = "Black"},
        new() {Id = 5, Name = "Sunny", Category = "Dog", Weight = 15, FurColor = "Orange"},
        new() {Id = 6, Name = "Flora", Category = "Dog", Weight = 10, FurColor = "White"},
        new() {Id = 7, Name = "Cherry", Category = "Cat", Weight = 5, FurColor = "Orange"},
        new() {Id = 8, Name = "Simba", Category = "Cat", Weight = 6, FurColor = "Grey"},
        new() {Id = 9, Name = "Sky", Category = "Cat", Weight = 5, FurColor = "Grey"},
        new() {Id = 10, Name = "Bloom", Category = "Cat", Weight = 3, FurColor = "Orange"},
        new() {Id = 11, Name = "Stella", Category = "Cat", Weight = 4, FurColor = "Black"},
        new() {Id = 12, Name = "Lola", Category = "Hamster", Weight = 0.3, FurColor = "Brown"},
        new() {Id = 13, Name = "Mike", Category = "Hamster", Weight = 0.2, FurColor = "Brown"},
        new() {Id = 14, Name = "Lucy", Category = "Rabbit", Weight = 2, FurColor = "Grey"},
        new() {Id = 15, Name = "Vasya", Category = "Rabbit", Weight = 1.5, FurColor = "White"}
    ];
}