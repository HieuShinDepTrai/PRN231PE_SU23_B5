using Microsoft.AspNetCore.Mvc;
using Q1.DataAccess;

namespace Q1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly PE_PRN231_Sum23B5Context _context;
        public EmployeeController(PE_PRN231_Sum23B5Context context)
        {
            _context = context;
        }

        [HttpPost("Delete/{EmployeeId}")]
        public IActionResult DeleteEmployee(int EmployeeId)
        {
            try
            {
                Employee deleteEmployee = _context.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeId);
                if(deleteEmployee == null)
                {
                    return NotFound();
                }
                var orders = _context.Orders.Where(o => o.EmployeeId == EmployeeId).ToList();
                var orderIds = orders.Select(o => o.OrderId).ToList();
                var orderDetailsToDelete = _context.OrderDetails.Where(od => orderIds.Contains(od.OrderId)).ToList();
                var orderDeletedNum = orders.Count();
                var result = new CountDTO
                {
                    employeeDeletedCount = 1,
                    orderDeletedCount = orderDeletedNum,
                    orderDetailDeletedCount = orderDetailsToDelete.Count()
                };
                _context.OrderDetails.RemoveRange(orderDetailsToDelete);
                _context.Orders.RemoveRange(orders);
                _context.Employees.Remove(deleteEmployee);
                _context.SaveChanges();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict("Can not delete");
            }
        }
    }
}
