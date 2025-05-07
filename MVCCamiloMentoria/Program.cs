using Microsoft.EntityFrameworkCore;
using MVCCamiloMentoria.Data;
using MVCCamiloMentoria.Integracao;
using MVCCamiloMentoria.Integracao.Interfaces;
using MVCCamiloMentoria.Integracao.Refit;
using Refit;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<EscolaContext>(options =>     {
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolMVCManagerConnectionString"));
});

builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();
builder.Services.AddRefitClient<IViaCepIntegracaoRefit>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri("https://viacep.com.br");
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
