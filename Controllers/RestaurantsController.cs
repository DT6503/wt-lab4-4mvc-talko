using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_talko.Models;

namespace wt_lab4_4mvc_talko.Controllers;

public class RestaurantsController : Controller
{
    public IActionResult Index()
    {
        return View(RestaurantData.GetAll());
    }

    public IActionResult Details(int id)
    {
        var restaurant = RestaurantData.GetById(id);
        if (restaurant == null)
            return NotFound();
        return View(restaurant);
    }
}