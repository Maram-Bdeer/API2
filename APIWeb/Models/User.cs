using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb.Models
{
    public class User
    {
        [Key]  // تأكد من تعريف المفتاح الأساسي
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }

}
