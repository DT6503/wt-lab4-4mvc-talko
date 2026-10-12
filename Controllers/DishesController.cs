using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_talko.Models;

namespace wt_lab4_4mvc_talko.Controllers;

public class DishesController : Controller
{
   
    public IActionResult Details(int id)
    {
        var dish = DishData.GetById(id);
        if (dish == null)
            return NotFound();   // 404
        return View(dish);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Dish dish)
    {
        if (!ModelState.IsValid)
            return View(dish);

        var id = DishData.NextId();
        var newDish = new Dish(id, dish.Name, dish.Price, dish.Category)
        {
            Description = dish.Description,
            ImageUrl = string.IsNullOrWhiteSpace(dish.ImageUrl)
                ? "https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=400&h=300&fit=crop"
                : dish.ImageUrl,
            IsAvailable = dish.IsAvailable
        };
        DishData.Add(newDish);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var dish = DishData.GetById(id);
        if (dish == null) return NotFound();
        return View(dish);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        DishData.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Index(string? category)
    {
        var dishes = DishData.GetDishes();
        if (!string.IsNullOrWhiteSpace(category))
            dishes = dishes.Where(d => d.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

        ViewBag.Categories = DishData.GetDishes().Select(d => d.Category).Distinct().ToList();
        ViewBag.CurrentCategory = category;
        return View(dishes);
    }

}