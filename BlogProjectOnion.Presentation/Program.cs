using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogProjectOnion.Application.AutoMapper;
using BlogProjectOnion.Application.IoC;
using BlogProjectOnion.Application.Services.Abstract;
using BlogProjectOnion.Application.Services.Concrete;
using BlogProjectOnion.Domain.Entities;
using BlogProjectOnion.Domain.Repositories;
using BlogProjectOnion.Infrastructure.Context;
using BlogProjectOnion.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogProjectOnion.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddAutoMapper(typeof(Mapping));

            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            //builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            //{
            //    builder.RegisterModule(new DependencyResolver()); // Kendi oluþturdugumuz Resolver Class'ýný baðlýyoruz.

            //});

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conStr")));

            //Identity 
            builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
      
            builder.Services.AddTransient<IAuthorRepository,AuthorRepository>();
            builder.Services.AddTransient<ICommentRepository,CommentRepository>();
            builder.Services.AddTransient<IGenreRepository,GenreRepository>();
            builder.Services.AddTransient<ILikeRepository,LikeRepository>();
            builder.Services.AddTransient<IPostRepository,PostRepository>();

            builder.Services.AddTransient<IAuthorService, AuthorManager>();
            builder.Services.AddTransient<ICommentService, CommentManager>();
            builder.Services.AddTransient<IGenreService, GenreManager>();
            builder.Services.AddTransient<ILikeService, LikeManager>();
            builder.Services.AddTransient<IPostService, PostManager>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.AccessDeniedPath = "/Home/Index";
                options.LoginPath = "/Login/Index";

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}