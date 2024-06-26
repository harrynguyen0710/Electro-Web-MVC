﻿using Microsoft.EntityFrameworkCore;
using DACS.Data;
using Microsoft.AspNetCore.Identity;
using DACS.Models;
using DACS.Service;
using DACS.IRepository;
using DACS.Repository;
using Microsoft.Extensions.Options;
using DACS.Helpers;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElectroWeb"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
);



builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddScoped<IProductRepository<SanPham>, ProductRepository<SanPham>>();   
builder.Services.AddScoped<IProductRepository<Iphone>, ProductRepository<Iphone>>();
builder.Services.AddScoped<IProductRepository<IMac>, ProductRepository<IMac>>();
builder.Services.AddScoped<IProductRepository<Ipad>, ProductRepository<Ipad>>();
builder.Services.AddScoped<IProductRepository<Laptop>, ProductRepository<Laptop>>();

builder.Services.AddScoped(typeof(IToolsRepository<>), typeof(ToolsRepository<>));

builder.Services.AddScoped<IBlog,BlogRepository>(); 
builder.Services.AddScoped<IHinhAnh, HinhAnhRepository>();
builder.Services.AddScoped<IDonHang, DonHangRepository>();
builder.Services.AddScoped<IOrderDetails, OrderDetailsRepository>();
builder.Services.AddScoped<IWishList, WishListRepository>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IAddress, AddressRepository>();
builder.Services.AddScoped<IWarranty, WarrantyRepository>();

builder.Services.AddIdentity<AppUserModel, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromMinutes(15); //thoi gian ton tai
    options.Cookie.IsEssential = true;
});


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddScoped<UserManager<AppUserModel>>();
builder.Services.AddScoped<SignInManager<AppUserModel>>();
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        // Đọc thông tin Authentication:Google từ appsettings.json



        googleOptions.ClientId = builder.Configuration["Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Google:ClientSecret"];
      
        googleOptions.SaveTokens = true;
        googleOptions.CallbackPath = builder.Configuration["Google:CallbackPath"];

    })
    .AddFacebook(FacebookOptions =>
      {
          // Đọc thông tin Authentication:Google từ appsettings.json

          FacebookOptions.ClientId = builder.Configuration["Facebook:ClientId"];
          FacebookOptions.ClientSecret = builder.Configuration["Facebook:ClientSecret"];

          FacebookOptions.SaveTokens = true;
          FacebookOptions.CallbackPath = builder.Configuration["Facebook:CallbackPath"];

     });


builder.Services.AddSingleton(x => new PaypalClient(
        builder.Configuration["PaypalOptions:AppId"],
        builder.Configuration["PaypalOptions:AppSecret"],
        builder.Configuration["PaypalOptions:Mode"]
));

builder.Services.AddSingleton<IVnPayService, VnPayService>();


var app = builder.Build(); 

app.UseSession();

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

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Manager", "Staff", "User" };

    foreach (var role in roles)
    {
        if (!await roleManger.RoleExistsAsync(role))
            await roleManger.CreateAsync(new IdentityRole(role));
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUserModel>>();
    string email = "admin@gmail.com";
    string emailtStaff = "staff@gmail.com";
    string password = "Test@1234";
    string address = "SG";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new AppUserModel();
        user.UserName = email;
        user.Email = email;
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Manager");
    }
    if (await userManager.FindByEmailAsync(emailtStaff) == null)
    {
        var user = new AppUserModel();
        user.UserName = emailtStaff;
        user.Email = emailtStaff;
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Staff");
    }
}
app.Run();
