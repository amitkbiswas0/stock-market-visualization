using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StockMarket.Pages.CustomPages
{
  public class CreateModel : PageModel
  {
    private stockdbContext _db;

    public CreateModel(stockdbContext db)
    {
      _db = db;
    }

    [BindProperty]
    public Stocks Stock { get; set; }

    public async Task<IActionResult> OnPost()
    {
      if (ModelState.IsValid)
      {
        await _db.Stocks.AddAsync(Stock);
        await _db.SaveChangesAsync();
        return RedirectToPage("Table");
      }
      else
      {
        return Page();
      }
    }
  }
}