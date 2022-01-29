using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.DB;
using Shop.Entities;
using Shop.Filters;
using Shop.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IGenericRepository<Category> categoryRepository;

        public ShopController(ApplicationDbContext context, IGenericRepository<Category> categoryRepository)
        {
            this.context = context;
            this.categoryRepository = categoryRepository;
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

        [HttpGet]
        public IActionResult AddCategoryForTest()
        {
            return View(new Category());
        }

        [HttpPost]
        [CustomValidationFilter]
        public IActionResult AddCategoryForTest(Category categoryModel)
        {
            context.Categories.Add(new Category { CreatedDate = DateTime.Now, CreatorId = 5, Name = "Electronics" });
            foreach (var item in context.ChangeTracker.Entries())
            {
                Console.WriteLine(item);
            }
            context.SaveChanges();
            return RedirectToAction(nameof(AddCategoryForTest));
        }
    }
}
