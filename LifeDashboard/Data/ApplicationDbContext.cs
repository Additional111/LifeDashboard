using Microsoft.EntityFrameworkCore;
using LifeDashboard.Models;

namespace LifeDashboard.Data;

public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<TaskItemModel> TaskItems { get; set; }
        public DbSet<HabitModel> Habits { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<MealLogModel> MealLogs { get; set; }
        public DbSet<UserSettingsModel> UserSettings { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
}