using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeDashboard.Data;
using LifeDashboard.Models;
using System.Threading.Tasks;

namespace LifeDashboard.Controllers;

public class TasksController : Controller
{
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
        _context = context;
    }
    //READ
    public async Task<IActionResult> Index()
    {
        var tasks = await _context.TaskItems.ToListAsync();
        return View(tasks);
    }

    //HTTP for CREATE
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskItemModel task)
    {
        if (ModelState.IsValid)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }
    
    //HTTP for UPDATE
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var taskItem = await _context.TaskItems.FindAsync(id);
        if (taskItem == null)
        {
            return NotFound();
        }
        return View(taskItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaskItemModel task)
    {
        if (id != task.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(task);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var taskItem = await _context.TaskItems.FindAsync(id);
        if (taskItem != null)
        {
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}