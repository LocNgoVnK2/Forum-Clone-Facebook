using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Generic;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MiniForum
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSession(options =>
            {
                options.Cookie.Name = ".MyApp.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddDbContext<EXDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EXDbContextConnection")));

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            }).CreateMapper());

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
          

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<ILike_DetailRepository,Like_DetailRepository>();
            services.AddScoped<ILike_DetailService, Like_DetailService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddScoped<IFriendFollowService, FriendFollowService>();
            services.AddScoped<IFriendFollowRepository, FriendFollowRepository>();
            // other service registrations

        }
   
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Index}/{id?}");
            });

        }
    }
}
