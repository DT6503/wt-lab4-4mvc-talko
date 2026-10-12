using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_talko.Models;

namespace wt_lab4_4mvc_talko.Controllers;

public class CouriersController : Controller
{
    public IActionResult Index()
    {
        return View(CourierData.GetAll());
    }

    public IActionResult Details(int id)
    {
        var courier = CourierData.GetById(id);
        if (courier == null)
            return NotFound();
        return View(courier);
    }
}