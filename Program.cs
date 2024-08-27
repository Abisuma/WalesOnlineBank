using Microsoft.EntityFrameworkCore;
using Wales_Online_Bank.Data;
using Microsoft.AspNetCore.Identity;
using Wales_Online_Bank.Repository.IRepository;
using Wales_Online_Bank.Repository;
using Wales_Online_Bank.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Wales_Online_Bank.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions  =>
    {
        sqlOptions.EnableRetryOnFailure();
    }
    ));


builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<CustomerUser>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddTransient<IEmailSender>(s => new EmailSender("smtp.gmail.com", 587, "detunjiaish@gmail.com"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
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
app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=CustomerSection}/{controller=Home}/{action=Index}/{id?}/{slug?}");
app.Run();
