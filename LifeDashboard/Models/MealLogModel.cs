using System;
using System.ComponentModel.DataAnnotations;

namespace LifeDashboard.Models;

public class MealLogModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Numele alimentului sau al mesei este obligatoriu")]
    [StringLength(100)]
    public string FoodName{ get; set; }
    
    [Required(ErrorMessage = "Numarul de calorii este obligatoriu")]
    [Range(0, 10000, ErrorMessage = "Caloriile trebuie sa fie un numar pozitiv")]
    public int Calories { get; set; }
    
    [Range(0, 1000, ErrorMessage = "Proteinele trebuie sa fie un numar pozitiv")]
    public int Proteins { get; set; }
    
    [Range(0, 1000, ErrorMessage = "Carbohidratii trebuie sa fie un numar pozitiv")]
    public int Carbs { get; set; }

    [Range(0, 1000, ErrorMessage = "Grasimile trebuie sa fie un numar pozitiv")]
    public int Fats { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;
}