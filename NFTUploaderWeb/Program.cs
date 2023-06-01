using NFTUploaderWeb.Constants;
using NFTUploaderWeb.Services.EthereumService;
using NFTUploaderWeb.Services.ExcelParserService;
using NFTUploaderWeb.Services.ImageConverter;
using NFTUploaderWeb.Services.IpfsService;
using System.Net.Http;
using System.Text;

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

            builder.Services.AddHttpClient("InfuraIPFS", options =>
            {
                var key = builder.Configuration[ConfigurationConstants.InfuraIPFSKey];
                var secret = builder.Configuration[ConfigurationConstants.InfuraIPFSSecret];

                var byteArray = Encoding.UTF8.GetBytes($"{key}:{secret}");
                var encodedCredentials = Convert.ToBase64String(byteArray);

                options.DefaultRequestHeaders.Add("Authorization", $"Basic {encodedCredentials}");
            });

            builder.Services.AddHttpClient("InfuraNode", options =>
            {
                var key = builder.Configuration[ConfigurationConstants.InfuraApiKey];
                var secret = builder.Configuration[ConfigurationConstants.InfuraApiSecret];

                var byteArray = Encoding.UTF8.GetBytes($"{key}:{secret}");
                var encodedCredentials = Convert.ToBase64String(byteArray);

                options.DefaultRequestHeaders.Add("Authorization", $"Basic {encodedCredentials}");
            });

            builder.Services.AddHttpClient("InfuraNFT_API", options =>
            {
                var key = builder.Configuration[ConfigurationConstants.InfuraApiKey];
                var secret = builder.Configuration[ConfigurationConstants.InfuraApiSecret];

                var byteArray = Encoding.UTF8.GetBytes($"{key}:{secret}");
                var encodedCredentials = Convert.ToBase64String(byteArray);

                options.DefaultRequestHeaders.Add("Authorization", $"Basic {encodedCredentials}");
            });

            builder.Services
                .AddScoped<IEthereumService, EthereumService>()
                .AddScoped<IImageConverter, ImageConverter>()
                .AddScoped<IIpfsService, IpfsService>()
                .AddScoped<IExcelParserService, ExcelParserService>();

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
