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

        


        public int lastType;
        public CreateModel(TimeClock.Data.TimeClockContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        
        public IActionResult OnGet()
        {
            
            
            ViewData["UserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            var entries = _context.Entry.ToList();
            lastType = entries.Any() ? entries.OrderByDescending(e => e.Timestamp).First().type : 0;
            return Page();
        }

        [BindProperty]
        public Entry Entry { get; set; } = default!;

        public Entry newlyAddedEntry { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.GetUserAsync(User);

            var entries = _context.Entry.ToList();

            int lastType = entries.Any() ? entries.OrderByDescending(e => e.Timestamp).First().type : 0;
            Console.WriteLine($"LastType Before: {lastType}");
            int newType = (lastType == 1) ? 2 : 1;
            Console.WriteLine($"LastType After: {lastType}, NewType: {newType}");

            var entry = new Entry
            {
                Timestamp = DateTime.Now,
                UserId = user.Id,
                type = newType
            };
            _context.Entry.Add(entry);
            _context.SaveChanges();

     
            if (!ModelState.IsValid)
            {
                newlyAddedEntry = _context.Entry.OrderByDescending(e => e.Timestamp).First();
                return Page();
            }

            _context.Entry.Add(Entry);
            await _context.SaveChangesAsync();

            
            return RedirectToPage("./Index");
        }
    }
}
