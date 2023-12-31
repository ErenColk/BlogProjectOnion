using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BlogProjectOnion.Application.Services.Concrete
{
    public class AppUserManager : BaseManager<AppUser>, IAppUserService
    {
        private readonly IBaseRepository<AppUser> _baseRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorManager;

        public AppUserManager(IBaseRepository<AppUser> baseRepository, IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IAuthorService authorManager) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _authorManager = authorManager;
        }

        public async Task<UpdateAppUserDTO> GetByUserName(string userName)
        {
            UpdateAppUserDTO result = await _appUserRepository.GetFilteredFirstOrDefault(select: x => new UpdateAppUserDTO
            {
                Id = x.Id,
                UserName = x.UserName,
                Password = x.PasswordHash,
                Email = x.Email,
                ImagePath = x.ImagePath

            },
            where: x => x.UserName == userName);

            return result;
        }

        //Kullanıcının sisteme login olmasını sağlayacak. User bilgilerine DTO'dan ulaştık. VE bu bilgileri Sessionda'da tutabiliriz.
        public async Task<SignInResult> Login(LoginAppUserDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterAppUserDTO model, AppUser user)
        {
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (model.Role == 1)
                {
                    Author author = new Author();
                    if (await _authorManager.TCreate(author))
                    {
                        user.Author = author;
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;

                        user.Author.FirstName = model.FirstName;
                        user.Author.LastName = model.LastName;

                        await _userManager.UpdateAsync(user);
                    }

                    await _userManager.AddToRoleAsync(user, "Author");

                }
                else if (model.Role == 2)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }

            }

            return result;

        }
        // Kullanıcı bilgilerini güncellemek için kullanacağımız metot.
        //Kullanıcının güncellemek istediği bilgileri View'dan UpdateProfileDTO aracılığıyla alacağız. Resim,Email,Password alanlarını kontrol ederek AppUser nesnesinde güncelleme yapacağız
        public async Task UpdateAppUser(UpdateAppUserDTO model)
        {
            // Update işlemlerinde önce Id ile ilgili nesneyi RAM'e çekeriz. Dışarıdan gelen güncel bilgilerle değişikleri yaparız. En son SaveChanges ile veritabanına güncellemeleri göndeririz.

            //Mapper
            var user = _mapper.Map<AppUser>(model);

            AppUser user2 = await _appUserRepository.GetDefault(x => x.Id == user.Id);

            // UploadPAth resim işlemleri
            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                image.Mutate(x => x.Resize(300, 300)); // Görsel ile ilgili işlemler mutate() içerisinde yapılabilir. Boyutlandırma kırpma vs.

                Guid guid = Guid.NewGuid(); // Resmin ismini unique oluşturacağız.
                image.Save($"wwwroot/images/{guid}.jpg"); // wwwroot klasörünün içinde images içinde guid bir isimle dosya kaydedilsin.
                user2.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                if (model.ImagePath != null) // Hali hazırda kendisinin bir resmi varsa
                    user2.ImagePath = model.ImagePath;
                else // Profili güncelleyen kullanıcının zaten bir resmi yoksa varsayılan resim adresi eklenir
                    user2.ImagePath = $"/images/defaultuser.jpg";
            }

            await _appUserRepository.Update(user2);

            //Parola değiştirme işlemi
            if (model.Password != null)
            {

                user2.PasswordHash = _userManager.PasswordHasher.HashPassword(user2, model.Password);
                await _userManager.UpdateAsync(user2);
            }

            // Email adres işlemleri (Eğer yoksa ekleteceğiz)
            if (model.Email != null)
            {
                AppUser isUserEmailExists = await _userManager.FindByEmailAsync(model.Email);

                if (isUserEmailExists == null)
                {
                    await _userManager.SetEmailAsync(user2, model.Email);
                }
            }
        }
    }
}
