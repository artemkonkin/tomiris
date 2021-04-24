using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace tomiris.Models
{
    public class ShoppingListModel
    {
        [Key]
        public int id {get; set;}
        public string Name {get; set;}
        public List<ShoppingListItemModel> ShListItems {get; set;} = new List<ShoppingListItemModel>();
        public int UserId { get; set; }
        public User User {get; set;}
    }
}