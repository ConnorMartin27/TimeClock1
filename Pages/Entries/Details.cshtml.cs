using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeClock.Data;
using TimeClock.Models;

namespace TimeClock.Pages.Entries
{
    public class DetailsModel : PageModel
    {
        private readonly TimeClock.Data.TimeClockContext _context;

        public DetailsModel(TimeClock.Data.TimeClockContext context)
        {
            _context = context;
        }

        public Entry Entry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _context.Entry.FirstOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }
            else
            {
                Entry = entry;
            }
            return Page();
        }
    }
}
