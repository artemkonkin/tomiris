using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tomiris.Models
{
    public class ShoppingList
    {
        [Key]
        public int id {get; set;}
        public string Name {get; set;}
        public List<ShoppingListItem> ShListItems {get; set;} = new List<ShoppingListItem>();
        public int UserId { get; set; }
        public User User {get; set;}
    }
}