using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.DataAccess;

namespace Q1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly PE_PRN231_Sum23B5Context _context;
        public OrderController(PE_PRN231_Sum23B5Context context)
        {
            _context = context;
        }

        [HttpGet("GetOrderByDate/{from}/{to}")]
        public IActionResult GetOrderByDate(DateTime from, DateTime to)
        {
            if(to < from)
            {
                return BadRequest();
            }
            var result = _context.Orders.Include(x => x.Customer).Include(x => x.Employee).ThenInclude(x => x.Department).Where(x => x.OrderDate >= from && x.OrderDate <= to).ToList();
            if (result == null)
            {
                return NotFound();
            }
            var result1 = result.Select(x => new OrderDTO()
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer?.CompanyName,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee?.FirstName + x.Employee?.LastName,
                EmployeeDepartmentName = x.Employee?.Department?.DepartmentName,
                OrderDate = x.OrderDate,
                ShipName = x.ShipName,
                ShipAddress = x.ShipAddress,
                ShipCity = x.ShipCity,
                ShipRegion = x.ShipRegion,
                ShipPostalCode = x.ShipPostalCode,
                ShipCountry = x.ShipCountry,
                RequiredDate = x.RequiredDate,
                ShippedDate = x.ShippedDate,
                Freight = x.Freight
            });
            return Ok(result1);
        }

        [HttpGet("GetAllOrder")]
        public IActionResult GetAllOrder()
        {
            var result = _context.Orders.Include(x => x.Customer).Include(x => x.Employee).ThenInclude(x => x.Department).ToList();
            if (result == null)
            {
                return NotFound();
            }
            var result1 = result.Select(x => new OrderDTO()
            {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer?.CompanyName,
                EmployeeId = x.EmployeeId,
                EmployeeName = x.Employee?.FirstName + x.Employee?.LastName,
                EmployeeDepartmentName = x.Employee?.Department?.DepartmentName,
                OrderDate = x.OrderDate,
                ShipName = x.ShipName,
                ShipAddress = x.ShipAddress,
                ShipCity = x.ShipCity,
                ShipRegion = x.ShipRegion,
                ShipPostalCode = x.ShipPostalCode,
                ShipCountry = x.ShipCountry,
                RequiredDate = x.RequiredDate,
                ShippedDate = x.ShippedDate,
                Freight = x.Freight
            }) ;
            return Ok(result1);
        }
    }
}
