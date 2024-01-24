using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using TimeClock.Data;
using TimeClock.Models;

namespace TimeClock.Pages.Entries
{
    public class CreateModel : PageModel
    {
        private readonly TimeClock.Data.TimeClockContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        

        public CreateModel(TimeClock.Data.TimeClockContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {

        ViewData["UserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Entry Entry { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.GetUserAsync(User);

            var entries = _context.Entry.ToList();

            int lastType = entries.Any() ? entries.OrderByDescending(e => e.Timestamp).First().type : 0;
            int newType = (lastType == 1) ? 2 : 1;

            var entry = new Entry
            {
                Timestamp = DateTime.Now,
                UserId = "0f3e2655-c4b0-48b5-9616-f7df05b992b0",
                type = newType
            };
            _context.Entry.Add(entry);
            _context.SaveChanges();

     
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Entry.Add(Entry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
