using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Asr.Data;
using Microsoft.AspNetCore.Authorization;
using Asr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ASR_Web.Data;
using ASR_Web.Models;

namespace ASR_Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly AsrContext _context;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger, AsrContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            [RegularExpression(@"^s\d{7}@student.rmit.edu.au|e\d{5}@rmit.edu.au$",
                ErrorMessage = "Email address is not in a valid format.")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var id = Input.Email.Substring(0, Input.Email.IndexOf('@'));
                if(id.StartsWith('e'))
                {
                    if(await _context.Staff.FindAsync(id) == null)
                        await _context.Staff.AddAsync(new Staff { StaffID = id, Email = Input.Email, Name = Input.Name });
                    user.StaffID = id;
                }
                else if(id.StartsWith('s'))
                {
                    if(await _context.Student.FindAsync(id) == null)
                        await _context.Student.AddAsync(new Student { StudentID = id, Email = Input.Email, Name = Input.Name });
                    user.StudentID = id;
                }
                else
                    throw new Exception();
                await _context.SaveChangesAsync();

                var result = await _userManager.CreateAsync(user, Input.Password);

                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, id.StartsWith('e') ? Constants.StaffRole :
                        id.StartsWith('s') ? Constants.StudentRole : throw new Exception());

                    _logger.LogInformation("User created a new account with password.");

                    await _signInManager.SignInAsync(user, false);

                    return LocalRedirect(returnUrl);
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
