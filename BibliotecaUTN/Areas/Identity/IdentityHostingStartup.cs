using System;
using BibliotecaUTN.Areas.Identity.Data;
using BibliotecaUTN.Models;
using BibliotecaUTN.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BibliotecaUTN.Areas.Identity.IdentityHostingStartup))]
namespace BibliotecaUTN.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BibliotecaIdentityContext>(options =>
                    options.UseMySql(
                        context.Configuration.GetConnectionString("BibliotecaDBConnectionString")));

            services
            .AddIdentity<BibliotecaUTNUser, IdentityRole>
                //.AddDefaultIdentity<BibliotecaUTNUser>
                (config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                //.AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<BibliotecaIdentityContext>();



                services.AddAuthentication().AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = context.Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = context.Configuration["Authentication:Facebook:AppSecret"];
                });

                services.AddTransient<IEmailSender, EmailSender>();
                services.Configure<SendGridService>(context.Configuration);
            });
        }
    }
}