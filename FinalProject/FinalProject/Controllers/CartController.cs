using FinalProject.Entities;
using FinalProject.Helpers;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        private readonly NhatNgheDbContext _context;
        private readonly ILogger<CartController> _logger;

        public CartController(NhatNgheDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [Route("/gio-hang")]
        public IActionResult Index()
        {
            return View(Carts);
        }

        [Route("/thanh-toan")]
        [Authorize]
        public IActionResult Checkout()
        {
            if(Carts.Count == 0)
            {
                return Redirect("/");
            }
            var maKh = Guid.Parse(User.Claims.SingleOrDefault(p => p.Type == MyConstants.ClaimCustomerId).Value);

            var defaultAddress = _context.UserAddresses
                .Include(ua => ua.User)
                .SingleOrDefault(ua => ua.UserId == maKh && ua.IsDefault);

            var addresses = _context.UserAddresses
               .Where(ua => ua.UserId == maKh);

            var addressList = new SelectList(addresses, "Id", "Address", defaultAddress.Id);

            ViewBag.SelectListAddress = addressList;
            var data = new CheckoutVM
            {
                Carts = Carts,
                UserAddressDefault = defaultAddress
            };

            return View(data);
        }

        [HttpPost("/thanh-toan")]
        [Authorize]
        public IActionResult Checkout(Guid UserAddressId)
        {
            var maKh = Guid.Parse(User.Claims.SingleOrDefault(p => p.Type == MyConstants.ClaimCustomerId).Value);

            _context.Database.BeginTransaction();

            try
            {
                var mycart = Carts;
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    OrderDate = DateTime.UtcNow,
                    OrderStatus = OrderStatus.NEW_ORDER,
                    UserId = maKh,
                    UserAddressId = UserAddressId,
                    Total = mycart.Sum(c => c.Quantity * c.Price)
                };
                _context.Add(order);

                foreach(var item in mycart)
                {
                    var cartItem = new OrderDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = item.Id,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };

                    _context.Add(cartItem);
                }

                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                _context.Database.RollbackTransaction();
            }
            return View();
        }

        const string GIO_HANG = "GioHang";

        public List<CartItem> Carts
        {
            get
            {
                var gioHang = HttpContext.Session.Get<List<CartItem>>(GIO_HANG);
                if (gioHang == null)
                {
                    gioHang = new List<CartItem>();
                }
                return gioHang;
            }
        }

        [HttpPost]
        public IActionResult AddToCart(Guid id, int qty = 1)
        {
            try
            {
                var gioHang = Carts;
                var item = gioHang.SingleOrDefault(p => p.Id == id);
                if (item != null) //có
                {
                    item.Quantity += qty;
                }
                else
                {
                    var hangHoa = _context.Products.SingleOrDefault(hh => hh.Id == id);
                    item = new CartItem
                    {
                        Id = id,
                        Price = hangHoa.Price,
                        Image = hangHoa.Image,
                        Quantity = qty,
                        ProductName = hangHoa.ProductName
                    };
                    gioHang.Add(item);
                }
                HttpContext.Session.Set<List<CartItem>>(GIO_HANG, gioHang);

                return Json(new { 
                    Success = true,
                    Total = Carts.Sum(c => c.Quantity * c.Price),
                    Count = Carts.Count
                });
            }
            catch
            {
                return Json(new
                {
                    Success = false
                });
            }
        }
    }
}
