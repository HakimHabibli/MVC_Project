﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_App.DAL;
using MVC_App.Models;

namespace MVC_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index()
        {
            var posts = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.PostPopularTags)
                    .ThenInclude(ptp => ptp.PopularTag)
                 .AsNoTracking()
                .ToListAsync();

            return View(posts);
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.PostPopularTags).ThenInclude(ptp => ptp.PopularTag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }




        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title");


            ViewData["PopularTags"] = new MultiSelectList(_context.PopularTags, "Id", "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,PublishDate,CategoryId,Id")] Post post, int[] selectedPopularTags)
        {


            _context.Add(post);
            await _context.SaveChangesAsync();
            if (ModelState.IsValid)
            {

                if (selectedPopularTags.Length > 0)
                {
                    foreach (var tagId in selectedPopularTags)
                    {
                        var popularTagPost = new PopularTagPost
                        {
                            PostId = post.Id,
                            PopularTagId = tagId
                        };
                        _context.PopularTagPosts.Add(popularTagPost);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));


        }








        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include(p => p.Category)
                .Include(p => p.PostPopularTags)

                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", post.CategoryId);
            ViewData["PopularTags"] = new MultiSelectList(_context.PopularTags, "Id", "Title");

            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Content,ImageUrl,PublishDate,CategoryId,Id")] Post post, int[] selectedPopularTags)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);


                    var existingTags = _context.PopularTagPosts.Where(ptp => ptp.PostId == id);

                    _context.PopularTagPosts.RemoveRange(existingTags);


                    if (selectedPopularTags != null && selectedPopularTags.Length > 0)
                    {
                        foreach (var tagId in selectedPopularTags)
                        {
                            var popularTagPost = new PopularTagPost
                            {
                                PostId = post.Id,
                                PopularTagId = tagId
                            };
                            _context.PopularTagPosts.Update(popularTagPost);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", post.CategoryId);
            ViewData["PopularTags"] = new MultiSelectList(_context.PopularTags, "Id", "Title", selectedPopularTags);

            return View(post);
        }






        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.PostPopularTags)
                .ThenInclude(ptp => ptp.PopularTag)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts
                .Include(p => p.PostPopularTags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post != null)
            {

                _context.PopularTagPosts.RemoveRange(post.PostPopularTags);


                _context.Posts.Remove(post);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Upload(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int? id, IFormFile photo, CancellationToken cancellationToken)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }
            if (photo == null) { BadRequest("Image is not found"); }
            if (photo.Length <= 0)
            {
                return BadRequest("Image is empty");
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };//Şəkillərin uzantilarini alirsan 
            var extention = Path.GetExtension(photo.FileName).ToLowerInvariant();//Yuklenen filein uzantilarini alirsan və onu loüer edirsən 
            if (string.IsNullOrEmpty(extention) || !allowedExtensions.Contains(extention))//Uzantilarin dogru olub olmadigini ve olub olmadigini yoxluyursan
            {
                return BadRequest("Invalid image extension");
            }
            string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(photo.FileName)}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload", fileName);


            try
            {
                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //throw;
            }

            var imageUlr = $"{Request.Scheme}://{Request.Host}/upload/{fileName}";
            // post request image stock api

            var post = await _context.Posts.FindAsync(id);
            post.ImageUrl = imageUlr;

            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
