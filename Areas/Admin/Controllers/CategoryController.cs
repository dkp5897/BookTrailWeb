using Microsoft.AspNetCore.Mvc;
using BookTrail.DataAccess.Data;
using BookTrail.Models;
using BookTrail.DataAccess.Repository.IRepository;

namespace BookTrailWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //applying custom validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Category Name and Display Order can not be same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? category1 = _context.Categories.FirstOrDefault(u=>u.CategoryId == id);
            //Category? category2 = _context.Categories.Where(u=>u.CategoryId == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? category1 = _context.Categories.FirstOrDefault(u=>u.CategoryId == id);
            //Category? category2 = _context.Categories.Where(u=>u.CategoryId == id).FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _unitOfWork.Category.Get(u => u.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
