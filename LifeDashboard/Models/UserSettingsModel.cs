using System.ComponentModel.DataAnnotations;

namespace LifeDashboard.Models;

public class UserSettingsModel
{
    public int Id { get; set; }

    [Range(1000, 10000, ErrorMessage = "Caloriile trebuie să fie între 1000 și 10000.")]
    public int TargetCalories { get; set; } = 2000;

    [Range(0, 500, ErrorMessage = "Proteinele trebuie să fie între 0 și 500g.")]
    public int TargetProteins { get; set; } = 140;    
}