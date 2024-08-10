using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_App.DAL;

namespace MVC_App.ViewComponents
{
    [ViewComponent(Name = "Post")]
    public class PostsViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public PostsViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.PostPopularTags)
                  .ToListAsync();
            return View(posts);
        }
    }
}
