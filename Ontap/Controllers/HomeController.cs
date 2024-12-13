using Microsoft.AspNetCore.Mvc;
using Ontap.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Ontap.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OnlineShopContext _context;
        public HomeController(ILogger<HomeController> logger,OnlineShopContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
            var tieude =_context.NavItems.ToList();
            ViewBag.Tieude = tieude;
            var top4sp = (from p in _context.Products
                          orderby p.UnitPrice descending
                          select p).Distinct().Take(4).ToList();

			ViewBag.Top4sp = top4sp;
            return View();
        }
        [HttpPost]
        public IActionResult GetProDuct(int id)
        {
            int id1 = Convert.ToInt32(id);
            var products = _context.Products.Where(p => p.CategoryId == id).ToList();
            return Json(products);
        }
        [HttpGet]
        public IActionResult DangKi()
		{
			var tieude = _context.NavItems.ToList();
			ViewBag.Tieude = tieude;
			return View();
		}
        [HttpPost]
        public IActionResult DangKi(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var tieude = _context.NavItems.ToList();
            ViewBag.Tieude = tieude;
            return View(customer);
        }
        [HttpGet]
        public IActionResult ThemSanPham()
        {
            var tieude = _context.NavItems.ToList();
            ViewBag.Tieude = tieude;
            ViewData["Categories"] = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult ThemSanPham(Product product)
        {
            // Kiểm tra xem Model có hợp lệ không
            string idPattern = @"^[A-Z]{2}\d{4}$";
            if (!Regex.IsMatch(product.Id, idPattern))
            {
                // Nếu Id không hợp lệ, thêm lỗi vào ModelState
                ModelState.AddModelError("Id", "Id must be in the format XX0000 where XX are uppercase letters and 0000 are digits.");
                
            }

            // Thêm sản phẩm vào cơ sở dữ liệu
            _context.Products.Add(product);
                _context.SaveChangesAsync();

                // Chuyển hướng đến trang danh sách sản phẩm sau khi thêm thành công
                return RedirectToAction(nameof(Index));
            
            
        }

        public IActionResult TimKiem(string key)
        {
            // Giả sử bạn đã có phương thức tìm kiếm sản phẩm
            var products = _context.Products.Where(p => p.Name.Contains(key)).ToList();

            // Đặt giá trị vào ViewBag để truyền dữ liệu đến View
            ViewBag.Message = "Kết quả tìm kiếm cho từ khóa: " + key;

            // Trả về PartialView với model là danh sách sản phẩm
            return PartialView("SanPham", products);
        }

        public IActionResult Privacy()
        {
			return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
