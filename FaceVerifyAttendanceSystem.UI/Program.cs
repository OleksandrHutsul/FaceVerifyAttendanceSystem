using FaceVerifyAttendanceSystem.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FaceVerifyAttendanceSystem.UI.DI;
using FaceVerifyAttendanceSystem.BL.AutoMapper;
using FaceVerifyAttendanceSystem.DAL.Entities;
using FaceVerifyAttendanceSystem.BL.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using FaceVerifyAttendanceSystem.BL.Services;
using Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("SqlConnection")));

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApiDbContext>();

//builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
//{
//    options.SignIn.RequireConfirmedAccount = true;
//})
//    .AddEntityFrameworkStores<ApiDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole<int>>() 
        .AddEntityFrameworkStores<ApiDbContext>();

builder.Services.AddAutoMapper(typeof(DbToDtoMappingProfile));
builder.Services.AddDependencyInjections();

builder.Services.AddScoped<SignInManager<User>>();

//builder.Services.AddTransient<IEmailSender, EmailSenderService>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.ConfigureApplicationCookie(o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});

builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection =
       builder.Configuration.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   });

builder.Services.AddRazorPages();

builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection("AuthMessageSenderOptions"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();