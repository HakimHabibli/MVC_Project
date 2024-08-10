using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_App.DAL;

namespace MVC_App.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public CategoryViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _appDbContext.Categories.ToListAsync();
            return View(categories);
        }
    }
}
