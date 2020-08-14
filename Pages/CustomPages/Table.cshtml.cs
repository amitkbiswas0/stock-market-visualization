using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockMarket.Models;

namespace StockMarket.Pages.CustomPages
{
  public class StockIndexModel : PageModel
  {
    private readonly stockdbContext _db;
    public StockIndexModel(stockdbContext db)
    {
      _db = db;
    }
  }
}