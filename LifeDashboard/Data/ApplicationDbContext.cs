using Microsoft.EntityFrameworkCore;
using LifeDashboard.Models;

namespace LifeDashboard.Data;

public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Habit> Habits { get; set; }
}