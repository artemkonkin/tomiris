using System.ComponentModel.DataAnnotations;

namespace tomiris.ViewModels
{
    public class BlogPostModel
    {
        [Required(ErrorMessage="Пустое имя статьи.")]
        public string Name {get; set;}

        [Required(ErrorMessage="Пустая статья.")]
        public string Text {get; set;}
    }
}