using System.ComponentModel.DataAnnotations;

namespace tomiris.Models
{
    public class ShoppingListItem
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}