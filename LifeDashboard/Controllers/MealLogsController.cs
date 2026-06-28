using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeDashboard.Data;
using LifeDashboard.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LifeDashboard.Controllers
{
    public class MealLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MealLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: Afișează mesele de azi și targeturile dinamice
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;

            // Luăm mesele de azi
            var todayMeals = await _context.MealLogs
                .Where(m => m.Date.Date == today)
                .OrderByDescending(m => m.Date)
                .ToListAsync();

            // Calculează totalurile de azi
            ViewBag.TotalCalories = todayMeals.Sum(m => m.Calories);
            ViewBag.TotalProteins = todayMeals.Sum(m => m.Proteins);
            ViewBag.TotalCarbs = todayMeals.Sum(m => m.Carbs);
            ViewBag.TotalFats = todayMeals.Sum(m => m.Fats);

            // Citim setările din DB. Dacă nu există nicio setare încă (e prima rulare), creăm una default.
            var settings = await _context.UserSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new UserSettingsModel { TargetCalories = 2500, TargetProteins = 150 };
                _context.UserSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            // Trimitem targeturile reale în pagină
            ViewBag.TargetCalories = settings.TargetCalories;
            ViewBag.TargetProteins = settings.TargetProteins;

            return View(todayMeals);
        }

        // 2. POST: Actualizează targeturile din interfață
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTargets(int targetCalories, int targetProteins)
        {
            var settings = await _context.UserSettings.FirstOrDefaultAsync();
            if (settings != null)
            {
                settings.TargetCalories = targetCalories;
                settings.TargetProteins = targetProteins;
                _context.UserSettings.Update(settings);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MealLogModel mealLog)
        {
            if (ModelState.IsValid)
            {
                _context.MealLogs.Add(mealLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mealLog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var mealLog = await _context.MealLogs.FindAsync(id);
            if (mealLog != null)
            {
                _context.MealLogs.Remove(mealLog);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}