using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.Services.Abstract
{
    public interface IAppUserService : IBaseServices<AppUser>
    {
        Task<IdentityResult> Register(RegisterAppUserDTO model);
        Task<SignInResult> Login(LoginAppUserDTO model);
        Task Logout();
        Task<UpdateAppUserDTO> GetByUserName(string userName);
        Task UpdateAppUser(UpdateAppUserDTO model);
    }
}
