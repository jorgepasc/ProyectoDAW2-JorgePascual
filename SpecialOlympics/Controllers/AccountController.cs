using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpecialOlympics.Models.ViewModels.Identity;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SpecialOlympics.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                        return RedirectToAction("ListUsers", "Administration");
                    //else
                    //{
                    //    await signInManager.SignInAsync(user, isPersistent: false);
                    //    return RedirectToAction("Index", "Home");
                    //}

                    SendConfirmEmail(confirmationLink, user.Email);

                    //TODO: Crear vista de mensaje para que no salga como un error
                    //ViewBag.ErrorTitle = "Registro completado con éxito";
                    //ViewBag.ErrorMessage = "Por favor confirma tu cuenta desde en el enlace que le hemos enviado al email";
                    ModelState.AddModelError(string.Empty, "Registro completado con éxito. Por favor confirme su cuenta desde en el enlace que le hemos enviado al email.");
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        private void SendConfirmEmail(string confirmationLink, string emailTo)
        {
            string emailFrom = "soavoluntarios@gmail.com";
            MailMessage mailMessage = new MailMessage(emailFrom, emailTo);
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Confirmación registro web Special Olympics Aragón";
            mailMessage.Body = "<p>Para confirmar su registro haga click en el siguiente enlace: <br/>" + confirmationLink + "</p><hr/>";
            mailMessage.Body += "<footer>Special Olympics Aragon</footer>";


            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = emailFrom,
                Password = "Sp3ciALS0a21?" // TODO: Guardar en UserSecrets
            };
            //smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"ID {userId} incorrecto";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Error al confirmar el email";
            return View("Error");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }


                ModelState.AddModelError(string.Empty, "Error al iniciar sesión");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"El email {email} ya está en uso");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
