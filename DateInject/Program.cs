using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MVCCamiloMentoria.Data;
using Microsoft.EntityFrameworkCore;

namespace DateInject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {

                    services.AddDbContext<EscolaContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("SchoolMVCManagerConnectionString")));

                    // Registra seus serviços
                    services.AddTransient<Importador>();
                    services.AddTransient<PlanilhaHelper>();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var importador = scope.ServiceProvider.GetRequiredService<Importador>();
                importador.Executar();
            }
        }
    }
}
