using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using PrimerAvancePOO2;

var builder = WebApplication.CreateBuilder(args);

//PASO1
var politicaUsuariosAutentificados = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

//PASO2
builder.Services.AddControllersWithViews(opc=>
    opc.Filters.Add(new AuthorizeFilter(politicaUsuariosAutentificados))
);


// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(opc => opc.UseSqlServer("name=AngelIbarraSQL"));



//PASO3
builder.Services.AddAuthentication();

//PASO4
builder.Services.AddIdentity<IdentityUser, IdentityRole> (
    opc => {opc.SignIn.RequireConfirmedAccount = false;}
).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


//PASO5
builder.Services.PostConfigure<CookieAuthenticationOptions>(
    IdentityConstants.ApplicationScheme, opc => 
    {
        //AQUI SE DEBE COLOCAR EL CONTROLLER , ES DECIR LA CARPETA DONDE ESTARA LA VISTA CREADA , Y LUEGO EL ACTION QUE ES LA VISTA 
       // opc.LoginPath = "/Usuarios/Registros";
       opc.LoginPath = "/Usuarios/Registros";
       opc.AccessDeniedPath = "/Usuarios/Registros";
    }
);


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

//ULTIMO PASO
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
