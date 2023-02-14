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

    public ProductController(IProductService productService)
    {
        this.productService = productService;
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

        await this.productService.DeleteAsync(product);

        return RedirectToAction(nameof(Index));
    }
}