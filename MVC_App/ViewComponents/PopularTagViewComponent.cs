using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_App.DAL;

namespace MVC_App.ViewComponents
{
    public class PopularTagViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public PopularTagViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var popularTag = await _appDbContext.PopularTags.Include(pt => pt.PopularTagPosts).ToListAsync();
            return View(popularTag);
        }

    }
}
