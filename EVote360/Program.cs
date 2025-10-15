using Evote360.Core.Application;
using Evote360.Core.Application.Interfaces;
using Evote360.Infrastructure.Persistence;
using EVote360.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(60);
    opt.Cookie.HttpOnly = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceLayerIoc(builder.Configuration);
builder.Services.AddApplicationLayerIoc();
builder.Services.AddHttpContextAccessor(); // Es lo mismo que builder.Services.AddSingleton<IHttpContextor...>
builder.Services.AddScoped<IUserSession, UserSession>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

/*
 *  Citizen es el Home por defecto.
 *  Recuerda que el Elector es un Citizen ejerciendo el voto. De ahí el nombre
 */
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=CitizenHome}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();