using Microsoft.AspNetCore.Mvc;

namespace Q2.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Order/OrderList")]
        public IActionResult OrderList() 
        {
            return View();
        }
    }
}
