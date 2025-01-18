using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineMealOrdering.Models;

public class CartController : Controller
{
    private List<CartItem> GetCart()
    {
        var cart = HttpContext.Session.GetString("Cart");
        if (string.IsNullOrEmpty(cart))
        {
            return new List<CartItem>();
        }

        return JsonConvert.DeserializeObject<List<CartItem>>(cart);
    }

    private void SaveCart(List<CartItem> cart)
    {
        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    [Authorize]
    public IActionResult Checkout()
    {
        var cart = GetCart();

        if (cart.Count == 0)
        {
            TempData["Message"] = "Twój koszyk jest pusty.";
            return RedirectToAction("Index");
        }

        return View(cart);
    }
    [Authorize]
    public IActionResult DeliveryDetails()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    [HttpPost]
    [Authorize]
    public IActionResult DeliveryDetails(DeliveryDetails deliveryDetails)
    {
        if (!ModelState.IsValid)
        {
            return View(deliveryDetails);
        }

        TempData["SuccessMessage"] = "Zamówienie zostało złożone pomyślnie! Dziękujemy za zakupy.";

        return RedirectToAction("Index", "Home");
    }
}
