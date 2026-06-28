using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeDashboard.Data;
using LifeDashboard.Models;
using System.Threading.Tasks;

namespace LifeDashboard.Controllers;

public class HabitsController : Controller
{
    private readonly ApplicationDbContext _context;

    public HabitsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var habits = await _context.Habits.ToListAsync();
        return View(habits);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HabitModel habitModel)
    {
        if (ModelState.IsValid)
        {
            habitModel.CreatedAt = DateTime.Now;
            habitModel.Streak = 0;

            _context.Habits.Add(habitModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(habitModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckIn(int id)
    {
        var habit = await _context.Habits.FindAsync(id);
        if (habit == null)
        {
            return NotFound();
        }

        var today = DateTime.Today;
        
        if(habit.LastCompleted.Date == today)
        {
            return RedirectToAction(nameof(Index));
        }

        if (habit.LastCompleted == null || habit.LastCompleted.Date == today.AddDays(-1))
        {
            habit.Streak++;
        }
        else
        {
            habit.Streak = 1;
        }

        habit.LastCompleted = DateTime.Now;

        _context.Update(habit);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index));

    }
    
    
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var habit = await _context.Habits.FindAsync(id);
        if (habit == null)
        {
            return NotFound();
        }

        return View(habit);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, HabitModel habitModel)
    {
        if (id != habitModel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(habitModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitExists(habitModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(habitModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var habit = await _context.Habits.FindAsync(id);
        if (habit != null)
        {
            _context.Habits.Remove(habit);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    
    private bool HabitExists(int id)
    {
        return _context.Habits.Any(e => e.Id == id);
    }
}

