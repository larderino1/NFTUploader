using NFTUploaderWeb.Services.EthereumService;
using NFTUploaderWeb.Services.ImageConverter;
using NFTUploaderWeb.Services.IpfsService;

namespace NFTUploaderWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddCors();

            builder.Services.AddHttpClient();

            builder.Services
                .AddScoped<IEthereumService, EthereumService>()
                .AddScoped<IImageConverter, ImageConverter>()
                .AddScoped<IIpfsService, IpfsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
