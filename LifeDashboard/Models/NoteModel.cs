using System;
using System.ComponentModel.DataAnnotations;

namespace LifeDashboard.Models;

public class NoteModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Titlul notei este obligatoriu.")]
    [StringLength(100)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Conținutul notei nu poate fi gol.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "Categoria este obligatorie.")]
    [StringLength(50)]
    public string Category { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.Now;    
}