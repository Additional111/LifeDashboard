using System;
using System.ComponentModel.DataAnnotations;

namespace LifeDashboard.Models;

public class TransactionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrierea este obligatorie.")]
        [StringLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Suma este obligatorie.")]
        [Range(0.01, 1000000, ErrorMessage = "Suma trebuie să fie mai mare decât 0.")]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }