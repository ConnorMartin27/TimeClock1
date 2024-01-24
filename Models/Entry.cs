using Microsoft.AspNetCore.Identity;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeClock.Models
{
    public class Entry
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; } // Unix timestamp

        public int type { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }



    }


}