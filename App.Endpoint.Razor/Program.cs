using App.Domain.AppService;
using App.Domain.Core.Contract.Task;
using App.Domain.Core.Contract.User;
using App.Domain.Core.Entites;
using App.Domain.Core.Entites.Config;
using App.Domain.Core.PersianIdentity;
using App.Domain.Service;
using App.Infra.Acssess.EfCore.Repository;
using App.Infra.Db.Sql;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);





builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();



var taskUnfinished = builder.Configuration.GetSection("LimitTask:TaskUnfinished").Value;
builder.Services.AddSingleton(taskUnfinished);



var congiguration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSetting = congiguration.GetSection(nameof(SiteSetting)).Get<SiteSetting>();
builder.Services.AddSingleton(siteSetting);

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(siteSetting.ConnectionString.SqlConnection)
);


builder.Services.AddScoped<IUseAppService, UserAppService>();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskAppService, TaskAppService>();









// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 5;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddErrorDescriber<PersianIdentityErrorDescriber>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
