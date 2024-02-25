using CleanArchitecture.Application;
using CleanArchitecture.Application.RepositoryService;
using CleanArchitecture.Infructure.DatabaseContext;
using CleanArchitecture.WebApp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
//builder.Services.AddAutoMapper(typeof(IApplication).Assembly);
//builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddAutoMapper(typeof(CleanArchitecture.Application.IApplication).Assembly);
builder.Services.AddTransient<IStudentRepository,StudentRepository>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
