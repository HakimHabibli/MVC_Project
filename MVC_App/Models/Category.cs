namespace MVC_App.Models;

public class Category : BaseEntity
{
    public string Title { get; set; } = null!;

    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }

    public virtual ICollection<Category>? SubCategories { get; set; }//reletion ucundu 
    public virtual ICollection<Post>? Posts { get; set; }
}