using CMS.Application.Extensions;
using CMS.Domain.Entities.Concrete;
using CMS.Infrastructure.Context;
using CMS.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Presentation.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;
        public CartController(AppDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartVM model = new CartVM
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };
            return View(model);
        }

        public async Task<IActionResult> Add(int id)
        {
            // Sepete eklenecek ürünün Id'sini tuttuk.
            Product product = await _db.Products.FindAsync(id);
            // Burada session'da tutulan yani hali hazırda sepette olan ve json tipinde tutulan ürünlerimizi uygulama tarafına çağırdık. Uygulama Json tipini anlamaz, bu sebeple ürünlerimizi CartItem tipine Deserialize ettik.
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            // Eklenmek istenen ürün varsa cartitem olacak yoksa null
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();
            // Şayet sepette bu ürün yoksa sepete ekleyecek
            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else// Zaten varsa 1 artıracak
            {
                cartItem.Quantity += 1;
            }
            // Uygulamanın anlayacağı cart objemizi Session' ın anlayacağı Json diline dönüştürdük ve gönderdik.
            HttpContext.Session.SetJson("Cart", cart);
            return ViewComponent("SmallCart");
        }

        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cart.Where(x => x.ProductId == id).FirstOrDefault();
            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(x => x.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        // Çöp kutusuna basınca ürün komple silinmesi için
        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            cart.RemoveAll(x => x.ProductId == id);

            // Sepette hiç ürü kalmazsa
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                // Tek bir ürün dahi kalsa
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        // Komple sepeti temizle
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Home");
        }
    }
}
