using System.ComponentModel.DataAnnotations;
using tomiris.utils;

namespace tomiris.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}