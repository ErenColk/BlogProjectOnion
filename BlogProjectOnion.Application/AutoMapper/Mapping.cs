using AutoMapper;
using BlogProjectOnion.Application.Models.DTOs.AuthorDTOs;
using BlogProjectOnion.Application.Models.DTOs.CommentDTOs;
using BlogProjectOnion.Application.Models.DTOs.GenreDTOs;
using BlogProjectOnion.Application.Models.DTOs.PostDTOs;
using BlogProjectOnion.Application.Models.VMs;
using BlogProjectOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjectOnion.Application.AutoMapper
{
    //DTO veya VM ile Entity arasındaki bağlantıları yapan kısım.Yani, DTO veya VM aracılığıyla alınan ya da gönderilen verileri ilgili Entity tipinde tanıyacak.
    public class Mapping : Profile
    {
        public Mapping()
        {
            //Buradaki işlemi, bütün entityler için yazılan tüm DTO ve VM'ler için gerçekleştireceğiz.
            // Daha sonra, Service Concrete Class'larımızda, IMapper'ı inject edeceğiz.
            // Service Concrete Class'larımızda, _mapper field'ından Map<T>(entity) şeklinde ilgili örneği ilgili tipimize alacağız. 
            CreateMap<Author,CreateAuthorDTO>().ReverseMap();
            CreateMap<Author,AuthorDetailVM>().ReverseMap();
            CreateMap<Author,UpdateAuthorDTO>().ReverseMap();
            CreateMap<Author,ResultAuthorDTO>().ReverseMap();
            CreateMap<Author,AuthorVM>().ReverseMap();

            CreateMap<Comment,CreateCommentDTO>().ReverseMap();
            CreateMap<Comment,UpdateCommentDTO>().ReverseMap();
            CreateMap<Comment,ResultCommentDTO>().ReverseMap();
            CreateMap<Comment,CommentVM>().ReverseMap();


            CreateMap<AppUser,RegisterAppUserDTO>().ReverseMap();
            CreateMap<AppUser,LoginAppUserDTO>().ReverseMap();
            CreateMap<AppUser,UpdateAppUserDTO>().ReverseMap();



            CreateMap<Post,CreatePostDTO>().ReverseMap();
            CreateMap<Post,UpdatePostDTO>().ReverseMap();
            CreateMap<Post,PostGetVM>().ReverseMap();
            CreateMap<Post,PostDetailVM>().ReverseMap();
            CreateMap<Post,PostVMT>().ReverseMap();
            CreateMap<Post,PostVM>().ReverseMap();

            CreateMap<Genre,CreateGenreDTO>().ReverseMap();
            CreateMap<Genre,UpdateGenreDTO>().ReverseMap();
            CreateMap<Genre,ResultGenreDTO>().ReverseMap();
            CreateMap<Genre,DetailGenreDTO>().ReverseMap();
            CreateMap<Genre,DeletedResultGenreDTO>().ReverseMap();
            CreateMap<Genre,GenreVM>().ReverseMap();

        }

    }
}
