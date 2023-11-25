using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using BlogProjectOnion.Application.AutoMapper;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Application.Services.Concrete;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Repositories;

namespace BlogProjectOnion.Application.IoC
{
    //NuGet Paketi : Autofac.Extensions.DependencyInjection
    //Module Class'ını System.Reflection'dan değil, using Autofac; satırını ekleyerek o namespace'ten alacağız.
    public  class DependencyResolver : Module
    {
        protected  override void Load(ContainerBuilder builder)
        {
            //Diğer eklenecek service ve managerlar  ve repositoryler varsa eklenecek.
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LikeRepository>().As<ILikeRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AppUserManager>().As<IAppUserService>().InstancePerLifetimeScope();

            #region AutoMapper
            builder.Register(context => new MapperConfiguration ( cfg =>
            {
                cfg.AddProfile<Mapping>();// Automapper klasörünün altına eklediğimiz Mapping class'ını bağlıyoruz.

            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            #endregion

            base.Load(builder);
        }

    }
}
