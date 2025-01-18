using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMealOrdering.Data;
using OnlineMealOrdering.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;


public class MenuController : Controller
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var menuItems = _context.MenuItems.ToList();
        return View(menuItems);
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddToCart(int id)
    {
        var menuItem = _context.MenuItems.FirstOrDefault(m => m.Id == id);
        if (menuItem == null) return NotFound();

        var cart = GetCart();

        var cartItem = cart.FirstOrDefault(c => c.MenuItemId == id);
        if (cartItem != null)
        {
            cartItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem
            {
                MenuItemId = menuItem.Id,
                Name = menuItem.Name,
                Price = menuItem.Price,
                Quantity = 1
            });
        }

        SaveCart(cart);

        return RedirectToAction("Index");
    }

    private List<CartItem> GetCart()
    {
        var cartJson = HttpContext.Session.GetString("Cart");
        return cartJson == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(cartJson);
    }

    private void SaveCart(List<CartItem> cart)
    {
        var cartJson = JsonSerializer.Serialize(cart);
        HttpContext.Session.SetString("Cart", cartJson);
    }
    public IActionResult Cart()
    {
        var cart = GetCart();
        return View(cart);
    }

}
