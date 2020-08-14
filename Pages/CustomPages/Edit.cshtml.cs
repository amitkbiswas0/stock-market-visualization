using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StockMarket.Pages.CustomPages
{
  public class EditModel : PageModel
  {
    private stockdbContext _db;

    public EditModel(stockdbContext db)
    {
      _db = db;
    }

    [BindProperty]
    public Stocks Stock { get; set; }

    public async Task OnGetAsync(Guid Id)
    {
      Stock = await _db.Stocks.FindAsync(Id);
    }

    public async Task<IActionResult> OnPost()
    {
      if (ModelState.IsValid)
      {
        var OldStock = await _db.Stocks.FindAsync(Stock.Id);
        OldStock.TradeCode = Stock.TradeCode;
        OldStock.TradeDate = Stock.TradeDate;
        OldStock.High = Stock.High;
        OldStock.Low = Stock.Low;
        OldStock.Open = Stock.Open;
        OldStock.Close = Stock.Close;
        OldStock.Volume = Stock.Volume;

        await _db.SaveChangesAsync();
        return RedirectToPage("Table");
      }
      return RedirectToPage();
    }
  }
}
