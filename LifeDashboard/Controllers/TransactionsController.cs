using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeDashboard.Data;
using LifeDashboard.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LifeDashboard.Controllers;

public class TransactionsController : Controller
{
    private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. GET: Afișează toate tranzacțiile și calculează Balanța, Veniturile și Cheltuielile
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.OrderByDescending(t => t.Date).ToListAsync();

            // Calculăm sumele matematice direct din baza de date
            decimal totalIncome = transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            decimal totalExpenses = transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            decimal balance = totalIncome - totalExpenses;

            // Trimitem aceste calcule către pagină folosind ViewBag
            ViewBag.TotalIncome = totalIncome;
            ViewBag.TotalExpenses = totalExpenses;
            ViewBag.Balance = balance;

            return View(transactions);
        }

        // 2. GET: Formularul de adăugare tranzacție
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. POST: Salvează tranzacția nouă în baza de date
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Transactions.Add(transactionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionModel);
        }

        // 4. POST: Șterge o tranzacție
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
}