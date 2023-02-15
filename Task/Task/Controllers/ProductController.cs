using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskk.Entities;
using Taskk.Services;
using Taskk.Statics;

namespace Taskk.Controllers;

[Authorize(Roles = UserRoles.Admin)]
public class ProductController : Controller
{
    private readonly IProductService productService;
    private readonly IProductAuditService productAuditService;

    public ProductController(IProductService productService, IProductAuditService productAuditService)
    {
        this.productService = productService;
        this.productAuditService = productAuditService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var products = await this.productService.GetAllAsync();

        return View(products);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Product newProduct)
    {
        if (!ModelState.IsValid)
            return View(newProduct);

        await this.productService.AddAsync(newProduct);

        var newProductAudit = new ProductAudit()
        {
            ChangedDate = DateTime.Now,
            Name = "Added New Product",
            ProductId = newProduct.Id,
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        };

        await this.productAuditService.AddAsync(newProductAudit);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await this.productService.GetByIdAsync(id);

        if (product == null)
            return View("NotFound");

        return View(product);
    }

    [HttpPost, ActionName(nameof(Edit))]
    public async Task<IActionResult> EditConfirmed(int id, Product updateProduct)
    {
        if (!ModelState.IsValid)
            return View(updateProduct);

        var product = await this.productService.GetByIdAsync(id);

        if (product == null)
            return View("NotFound");

        var newProductAuditName = "";

        if (product.Title != updateProduct.Title)
            newProductAuditName += "Title ";
        if (product.Quantity != updateProduct.Quantity)
            newProductAuditName += "Quantity ";
        if (product.Price != updateProduct.Price)
            newProductAuditName += "Price ";

        if (newProductAuditName != "")
        {
            var newProductAudit = new ProductAudit()
            {
                ChangedDate = DateTime.Now,
                Name = newProductAuditName,
                ProductId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            };

            await this.productAuditService.AddAsync(newProductAudit);
        }

        await this.productService.UpdateAsync(id, updateProduct);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await this.productService.GetByIdAsync(id);

        if (product == null)
            return View("NotFound");

        return View(product);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await this.productService.GetByIdAsync(id);

        if (product == null)
            return View("NotFound");

        return View(product);
    }

    [HttpPost, ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await this.productService.GetByIdAsync(id);

        if (product == null)
            return View("NotFound");

        var newProductAudit = new ProductAudit()
        {
            ChangedDate = DateTime.Now,
            Name = "Deleted Product",
            ProductId = id,
            UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        };

        await this.productAuditService.AddAsync(newProductAudit);
        await this.productService.DeleteAsync(product);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Audit()
    {
        var productAudits = await this.productAuditService.GetAllAsync();

        return View(productAudits);
    }

    public async Task<IActionResult> FilterHistory()
    {
        var filter = new Filter();

        if (DateTime.TryParse(Request.Form["startdate"], out _))
            filter.StartDate = DateTime.Parse(Request.Form["startdate"]);

        if (DateTime.TryParse(Request.Form["enddate"], out _)) 
            filter.EndDate = DateTime.Parse(Request.Form["enddate"]);

        var result = await this.productAuditService.FilterAsync(filter);

        return View(result);
    }
}