using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeDashboard.Data;
using LifeDashboard.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LifeDashboard.Controllers;

public class NotesController : Controller
{
    private readonly ApplicationDbContext _context;

    public NotesController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var notes = await _context.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync();
        return View(notes);
    }

    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NoteModel note)
    {
        if (ModelState.IsValid)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(note);
    }

    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return NotFound();
        }
        return View(note);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NoteModel updatedNote)
    {
        if (id != updatedNote.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Notes.Update(updatedNote);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notes.Any(n => n.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(updatedNote);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note != null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}