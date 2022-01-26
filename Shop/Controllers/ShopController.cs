using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.DB;
using Shop.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext context;

        public ShopController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult GetAllProducts()
        {
            var products = context.Products.Include(i=>i.Category).ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var categoryList = context.Categories.ToList();
            var selectListCategories = new SelectList(categoryList, "Id", "Name");
            ViewBag.CategoryList = selectListCategories;
            return View(new Product());
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return RedirectToAction("GetAllProducts");
        }

        [HttpGet]
        public IActionResult AddToCart(int productId)
        {
            List<Product> cartList = null;
            var product = context.Products.FirstOrDefault(i => i.Id == productId);
            var carts = HttpContext.Session.GetString("cart");
            try
            {
                cartList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(carts);
            }
            catch (System.Exception)
            {
                cartList = new List<Product>();
            }
            
            cartList.Add(product);
            var cartJson = Newtonsoft.Json.JsonConvert.SerializeObject(cartList);
            HttpContext.Session.SetString("cart", cartJson);
            return RedirectToAction("GetAllProducts");
        }
    }
}
