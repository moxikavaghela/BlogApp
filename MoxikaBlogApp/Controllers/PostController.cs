using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoxikaBlogApp.Data;
using MoxikaBlogApp.Models;
using MoxikaBlogApp.Models.ViewModels;
using System.Net;

namespace MoxikaBlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] _allowedExtension = { ".jpg", ".jpeg", ".png" };

        public PostController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            var postQuery = _context.Posts.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                postQuery = postQuery.Where(p => p.CategoryId == categoryId);
            }
            var posts = postQuery.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View(posts);
        }

        [HttpGet]

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        //public async Task<IActionResult> Detail(int id)
        //{            
        //    if id == 0)
        //    {
        //        return NotFound();
        //    }

        //    var post = _context.Posts.Include(p => p.Category).Include(p=>p.Comments).FirstOrDefault(p => p.Id == id);

        //    if (post == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(post);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            var postViewModel = new PostViewModel();
            postViewModel.Categories = _context.Categories.Select(c =>
            new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }
            ).ToList();

            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var inputFileExtension = Path.GetExtension(postViewModel.FeatureImage.FileName).ToLower();
                bool isAllowed = _allowedExtension.Contains(inputFileExtension);
                if (!isAllowed)
                {
                    ModelState.AddModelError("", "Only .jpg, .jpeg, .png files are allowed");
                    return View(postViewModel);
                }

                postViewModel.Post.FeatureImagePath = await UploadFiletoFolder(postViewModel.FeatureImage);
                await _context.Posts.AddAsync(postViewModel.Post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            postViewModel.Categories = _context.Categories.Select(c =>
            new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }
            ).ToList();

            return View(postViewModel);

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (postFromDb == null)
            {
                return NotFound();
            }
            EditViewModel editViewModel = new EditViewModel
            {
                Post = postFromDb,
                Categories = _context.Categories.Select(c =>
                 new SelectListItem
                 {
                     Value = c.Id.ToString(),
                     Text = c.Name
                 }).ToList()
            };
            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editViewModel);
            }

            var postFromDb = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == editViewModel.Post.Id);
            if (postFromDb == null)
            {
                return NotFound();
            }

            if (editViewModel.FeatureImage != null)
            {
                var inputFileExtension = Path.GetExtension(editViewModel.FeatureImage.FileName).ToLower();
                bool isAllowed = _allowedExtension.Contains(inputFileExtension);
                if (!isAllowed)
                {
                    ModelState.AddModelError("", "Only .jpg, .jpeg, .png files are allowed");
                    return View(editViewModel);
                }
                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Path.GetFileName(postFromDb.FeatureImagePath));
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
                editViewModel.Post.FeatureImagePath = await UploadFiletoFolder(editViewModel.FeatureImage);
            }
            else 
            {
                editViewModel.Post.FeatureImagePath = postFromDb.FeatureImagePath;
            }
            _context.Posts.Update(editViewModel.Post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (postFromDb == null)
            {
                return NotFound();
            }
            return View(postFromDb);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postFromDb = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (postFromDb == null)
            {
                return NotFound();
            }

            // Delete the file if it exists
            if (!string.IsNullOrEmpty(postFromDb.FeatureImagePath))
            {
                var existingFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "images",
                    Path.GetFileName(postFromDb.FeatureImagePath));
                if (System.IO.File.Exists(existingFilePath))
                {
                    System.IO.File.Delete(existingFilePath);
                }
            }

            _context.Posts.Remove(postFromDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public JsonResult AddComment([FromBody] Comments comment)
        {
            comment.CommentDate = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Json(new
            {
                UserName = comment.UserName, // Capital "N"
                CommentDate = comment.CommentDate.ToString("MMM dd yyyy"),
                Content = comment.Content
            });


            //return Json(new
            //{
            //    Username = comment.UserName,
            //    CommentDate = comment.CommentDate.ToString("MMM dd yyyy"),
            //    Content = comment.Content
            //});
        }

        private async Task<string> UploadFiletoFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + inputFileExtension;
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            var ImagesFolderPath = Path.Combine(wwwRootPath, "images");

            if (!Directory.Exists(ImagesFolderPath))
            {
                Directory.CreateDirectory(ImagesFolderPath);
            }

            var filePath = Path.Combine(ImagesFolderPath, fileName);
            try
            {
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                //return fileName;
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("", "File upload failed");
                return "Error Uploading Image: " + ex.Message;
            }
            return "/images/" + fileName;
        }
    }
}
