using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using TimeClock.Data;
using TimeClock.Models;

namespace TimeClock.Pages.Entries
{
    public class IndexModel : PageModel
    {
        private readonly TimeClock.Data.TimeClockContext _context;

        public IndexModel(TimeClock.Data.TimeClockContext context)
        {
            _context = context;
        }

        ///public IList<Entry> Entry { get;set; } = default!;
        public List<Entry> Entries { get; set; } = new List<Entry>();

        public TimeSpan TotalDurationBetweenType1And2 { get; set; }

        private TimeSpan CalculateTotalDuration(List<Entry> type1Entries, List<Entry> type2Entries)
        {
            TimeSpan totalDuration = TimeSpan.Zero;

            foreach (var type1Entry in type1Entries)
            {
                var closestType2Entry = type2Entries.FirstOrDefault(e => e.Timestamp > type1Entry.Timestamp);

                if (closestType2Entry != null)
                {
                    totalDuration += closestType2Entry.Timestamp - type1Entry.Timestamp;
                }
            }

            return totalDuration;
        }

        public async Task OnGetAsync()
        {
            // Retrieve entries from the database
            Entries = _context.Entry.OrderBy(e => e.Timestamp).ToList();

            // Find entries of type 1 and type 2
            var type1Entries = Entries.Where(e => e.type == 1).ToList();
            var type2Entries = Entries.Where(e => e.type == 2).ToList();

            // Calculate total duration between type 1 and type 2 entries
            TotalDurationBetweenType1And2 = CalculateTotalDuration(type1Entries, type2Entries);

        }
    }
}
