using chapter4_demo2.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.EntityFrameworkCore;

namespace chapter4_demo2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddOData(options =>
                    options.Select()
                           .Filter()
                           .Count()
                           .OrderBy()
                           .Expand()
                           .SetMaxTop(100)
                           .AddRouteComponents("odata", GetEdmModel()) // Đăng ký OData và route
            .RouteOptions.EnableKeyInParenthesis = true);
            // Đăng ký DbContext
            builder.Services.AddDbContext<BookStoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
            });

            // Cấu hình Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseRouting(); // Bổ sung để hỗ trợ định tuyến
           
            app.MapControllers();

            app.Run();
        }

        // Phương thức tạo mô hình EDM
        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("Books");
            builder.EntitySet<Press>("Presses");
            return builder.GetEdmModel();
        }
    }
}