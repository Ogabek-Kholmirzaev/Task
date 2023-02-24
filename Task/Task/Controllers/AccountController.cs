using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskk.Data;
using Taskk.Dtos.AppUser;
using Taskk.Entities;

namespace Taskk.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext appDbContext;
    private readonly UserManager<AppUser> userManager;
    private readonly SignInManager<AppUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AccountController(AppDbContext appDbContext, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.appDbContext = appDbContext;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return View(registerDto);

        var user = await this.appDbContext.Users
            .FirstOrDefaultAsync(user => user.UserName == registerDto.UserName || user.Email == registerDto.EmailAddress);

        if (user != null)
        {
            TempData["Error"] = "This Email or UserName is already in use!";

            return View(registerDto);
        }

        var newUser = registerDto.Adapt<AppUser>();
        var newUserResponse = await this.userManager.CreateAsync(newUser, registerDto.Password);

        if (newUserResponse.Succeeded)
        {
            if (await this.roleManager.RoleExistsAsync(registerDto.Role) == false)
            {
                var role = new IdentityRole(registerDto.Role);

                await this.roleManager.CreateAsync(role);
            }

            await this.userManager.AddToRoleAsync(newUser, registerDto.Role);
            await this.signInManager.SignInAsync(newUser, isPersistent: true);

            return RedirectToAction("Index", "Product");
        }

        TempData["Error"] = "Cannot register. Please try later!";

        return View(registerDto);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return View(loginDto);

        var user = await this.appDbContext.Users
            .FirstOrDefaultAsync(user => user.UserName == loginDto.UserName);

        if (user != null)
        {
            var passwordCheck = await this.userManager.CheckPasswordAsync(user, loginDto.Password);

            if (passwordCheck)
            {
                var result = await this.signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Product");
            }

            TempData["Error"] = "Entered wrong password or username!";

            return View(loginDto);
        }

        TempData["Error"] = "Entered wrong password or username!";

        return View(loginDto);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await this.signInManager.SignOutAsync();

        return RedirectToAction("Index", "Product");
    }
}