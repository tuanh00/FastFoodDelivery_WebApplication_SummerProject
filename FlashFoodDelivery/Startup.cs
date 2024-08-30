using Business_Layer.AutoMapper;
using Business_Layer.DataAccess;
using Business_Layer.Repositories;
using Business_Layer.Services;
using Business_Layer.Services.VNPay;
using Business_Layer.Utils;
using Data_Layer.Models;
using Data_Layer.ResourceModel.ViewModel.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

namespace API
{
    public class Startup
    {
        private IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", build => build.AllowAnyMethod()
                .AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(hostName => true).Build());
            });


            services.AddAutoMapper(typeof(ApplicationMapper));

            InjectServices(services);
            ConfigureJWT(services);

        }

        private void InjectServices(IServiceCollection services)
        {
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //string ConnectionStr = config.GetConnectionString("Db");
            //services.AddDbContext<EShopDBContext>(option => option.UseSqlServer(ConnectionStr));

            services.AddDbContext<FastFoodDeliveryDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DB"));
            });
            // Add services repository pattern
            services.AddTransient<IMenuFoodItemRepository, MenuItemFoodRepository>();
            services.AddTransient<IDataService, RoleDataRepository>();
            services.AddTransient<IUserRepository, UserRepository>();


            services.AddTransient<IUserSerivce, UserService>();
            services.AddTransient<IClaimsService, ClaimsService>();


            //Order
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            //OrderDetail
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

            //OrderStatus
            services.AddScoped<IOrderStatusService, OrderStatusService>();
            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();

           
            //MenuFoodItem
            services.AddScoped<IMenuFoodItem1Repository, MenuFoodItem1Repository>();
            services.AddScoped<IMenuFoodItem1Service, MenuFoodItem1Service>();

            //Category
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            //Shipper
            //services.AddScoped<IShipperRepository, ShipperRepository>();

            //Cart
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            //feedback
            services.AddScoped<IFeedBackRepository, FeedBackRepository>();
            services.AddScoped<IFeedBackService, FeedBackService>();

            //AdminDashboard
            services.AddScoped<IDashboardService, DashBoardService>();

            //ZaloPay
            services.Configure<ZaloPaySettings>(Configuration.GetSection("ZaloPay"));
            services.AddScoped<IPaymentZaloService, PaymentZaloSerivce>();
            // VNPay setting 
            services.AddSingleton<VNPayHelper>();
            services.Configure<VNPaySettings>(Configuration.GetSection("VNPaySettings"));
            services.AddScoped<IVNPayService, VNPayService>();

            

            services.AddControllers();
            //Map API
            services.AddCors(options =>
            {
                ////options.AddPolicy("AllowSpecificOrigin",
                //    builder => builder
                //        .WithOrigins("http://localhost:5177") // Change to your client app URL
                //        .AllowAnyHeader()
                //        .AllowAnyMethod());
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseCors(builder =>
            //{
            //    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            //});
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider.GetServices<IDataService>();

            foreach (var service in services)
            {
                service.AddData().GetAwaiter().GetResult();
            }

            app.Run();
        }

        private void ConfigureJWT(IServiceCollection services)
        {
            services.Configure<JWTSetting>(Configuration.GetSection("JwtSetting"));
            services.Configure<AdminAccount>(Configuration.GetSection("AdminAccount"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<FastFoodDeliveryDBContext>()
                    .AddDefaultTokenProviders();
            services.AddIdentityCore<User>();

            services.Configure<IdentityOptions>(options =>
            {
                // setting for password
                // ví dụ như password có số, có chữ hoa, or có chữ thường hay không?
                // độ dài của password.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                // Setting for logout
                // Thời gian vào hệ thống
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                // Định nghĩa số lần nhập sai Password, or userName
                options.Lockout.MaxFailedAccessAttempts = 3;

                options.Lockout.AllowedForNewUsers = true;

                // Setting for user
                // Không được đăng kí trùng Email
                options.User.RequireUniqueEmail = true;
            });

            // Kiểm Tra xem mã Token có mapping được không
            // Kiểm tra JWTSetting
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWTSetting:Issuer"],
                    ValidAudience = Configuration["JWTSetting:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSetting:Key"]))
                };

            });
        }
    }
}
