using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoxikaBlogApp.Data;
using MoxikaBlogApp.Models.ViewModels;
using System.Net;

namespace MoxikaBlogApp.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] _allowedExtension ={".jpg",".jpeg",".png"};

        public PostController(AppDbContext context, IWebHostEnvironment webHostEnvironment) 
        {
            _context=context;
            _webHostEnvironment=webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            var postQuery = _context.Posts.Include(p =>p.Category).AsQueryable();

            if (categoryId.HasValue) 
            {
                postQuery = postQuery.Where(p => p.CategoryId == categoryId);
            }
            var posts = postQuery.ToList();
            ViewBag.Categories =_context.Categories.ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var postViewModel = new PostViewModel();
            postViewModel.Categories = _context.Categories.Select(c=>
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

        private async Task<string> UploadFiletoFolder(IFormFile file)
        {
            var inputFileExtension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString()+ inputFileExtension;
            var wwwRootPath= _webHostEnvironment.WebRootPath;
            var ImagesFolderPath = Path.Combine(wwwRootPath, "images");

            if (!Directory.Exists(ImagesFolderPath))
            {
                Directory.CreateDirectory(ImagesFolderPath);
            }

            var filePath = Path.Combine(ImagesFolderPath, fileName);
            try 
            {
                await using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                //return fileName;
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError("", "File upload failed");
                return "Error Uploading Image: "+ex.Message;
            }
            return "/images/" + fileName;
        }
    }
}
