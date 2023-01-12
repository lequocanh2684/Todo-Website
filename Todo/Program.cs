using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Auth0.AspNetCore.Authentication;
using Todo.Data;
using Todo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseMySQL(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<IDesignTimeServices, MysqlEntityFrameworkDesignTimeServices>();
builder.Services.AddScoped<IToDoService, ToDoService>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ToDoContext>();
builder.Services.AddControllersWithViews();

//builder.Services.AddAuth0WebAppAuthentication(options =>
//{
//    options.Domain = builder.Configuration["Auth0:Domain"];
//    options.ClientId = builder.Configuration["Auth0:ClientId"];
//    options.CallbackPath = new PathString("/callback");
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddCookie()
//    .AddOpenIdConnect("Auth0", options =>
//    {
//        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";

//        options.ClientId = builder.Configuration["Auth0:ClientId"];
//        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];

//        options.ResponseType = OpenIdConnectResponseType.Code;

//        options.Scope.Clear();
//        options.Scope.Add("openid");
//        options.Scope.Add("profile");
//        options.CallbackPath = new PathString("/callback");
//        options.ClaimsIssuer = "Auth0";
//        options.SaveTokens = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            NameClaimType = "name"
//        };

//        options.Events = new OpenIdConnectEvents
//        {
//            OnRedirectToIdentityProviderForSignOut = (context) =>
//            {
//                var logoutUri = $"https://{builder.Configuration["Auth0:Domain"]}/v2/logout?client_id={builder.Configuration["Auth0:ClientId"]}";

//                var postLogoutUri = context.Properties.RedirectUri;
//                if (!string.IsNullOrEmpty(postLogoutUri))
//                {
//                    if (postLogoutUri.StartsWith("/"))
//                    {
//                        var request = context.Request;
//                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
//                    }
//                    logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
//                }

//                context.Response.Redirect(logoutUri);
//                context.HandleResponse();

//                return Task.CompletedTask;
//            }
//        };
//    });

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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
app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.None
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoes}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

