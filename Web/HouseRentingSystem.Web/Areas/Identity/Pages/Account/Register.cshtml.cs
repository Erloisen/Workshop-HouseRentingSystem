namespace HouseRentingSystem.Web.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using static HouseRentingSystem.Common.GlobalConstants.UserConstants;

    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Fist Name")]
            [StringLength(UserFirstNameMaxLength, MinimumLength = UserFirstNameMinLength)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(UserLastNameMaxLength, MinimumLength = UserLastNameMinLength)]
            public string LastName { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");

            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                };

                var result = await this.userManager.CreateAsync(user, this.Input.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.RedirectToPage("/Identity/Account/Login");
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return this.Page();
        }
    }
}
