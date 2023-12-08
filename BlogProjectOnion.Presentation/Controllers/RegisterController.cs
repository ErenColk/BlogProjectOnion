using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Application.ValidationRules.AppUserValidationRules;
using BlogProjectOnion.Domain.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace BlogProjectOnion.Presentation.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;

        public RegisterController(UserManager<AppUser> userManager, IMapper mapper, IAppUserService appUserService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterAppUserDTO registerAppUser)
        {
            AppUserRegisterValidator validationRules = new AppUserRegisterValidator();
            var model  = validationRules.Validate(registerAppUser);
            if (model.IsValid)
            {
                Random random = new Random();
                int code = random.Next(100000, 1000000);

                AppUser user = new AppUser();
                user.UserName = registerAppUser.UserName;
                user.Email = registerAppUser.Email;
                user.CreatedDate = registerAppUser.CreatedDate;
                user.ConfirmCode = code;
                var result = await _appUserService.Register(registerAppUser, user);

                if (result.Succeeded)
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAdressFrom = new MailboxAddress("BLOG VİSTA", "demoblogvista@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", user.Email);

                    mimeMessage.From.Add(mailboxAdressFrom); // gönderecek 
                    mimeMessage.To.Add(mailboxAddressTo); // kime gidecek

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz : " + code;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "BLOG VİSTA Onay Kodu";

                    SmtpClient client = new SmtpClient();
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("demoblogvista@gmail.com", "xmxaivnzhqonjgec");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    return RedirectToAction("Index", "ConfirmMail",new {id = user.Id});
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(registerAppUser);                  
                }
            }
            else
            {
                ModelState.Clear();
                foreach (var error in model.Errors)
                {                                 
                        ModelState.AddModelError("", error.ErrorMessage);                    
                }
                return View(registerAppUser);
            }        
        }
    }
}


//string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", Guid.NewGuid().ToString() + Path.GetExtension(registerAppUser.UploadPath.FileName));

//SaveImage.ImagePath(uploadPath, registerAppUser);

//registerAppUser.ImagePath = $"/images/{registerAppUser.UploadPath.FileName}";

//AppUser appUser = _mapper.Map<AppUser>(registerAppUser);
//IdentityResult result = await _userManager.CreateAsync(appUser, registerAppUser.Password);
//if (result.Succeeded)
//{

//    await _userManager.AddToRoleAsync(appUser, "User");

//    return RedirectToAction("Index", "Home");
//}