using System;
using System.ComponentModel.DataAnnotations;

namespace LifeDashboard.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Titlul este obligatoriu!")]
    [StringLength(100)]
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
}