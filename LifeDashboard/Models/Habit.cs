using System;
using System.ComponentModel.DataAnnotations;


namespace LifeDashboard.Models;

public class Habit
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Titlul obiceiului este obligatoriu")]
    [StringLength(100, ErrorMessage = "Titlul nu poate depasi 100 de caractere")]
    public string Title { get; set; } = string.Empty;
    [StringLength(500)]
    public string? Description { get; set; }
    public int Streak { get; set; } = 0;
    public DateTime LastCompleted { get;set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}