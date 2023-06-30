using IdentityModel.AspNetCore.OAuth2Introspection;
using RuralCultureDataAccess;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//读取网站配置文件
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string MOIConnStr = configuration["ConnectionStrings:MOIConnStr"];

//配置连接字符串
builder.Services.AddDbContext(options => options.AddMtrlSqlServer(configuration["ConnectionStrings:MOIConnStr"]));

//配置AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

#region 配置跨域
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("Cors", policy =>
    {
        policy.WithOrigins("*", "*");
        policy.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");
        policy.AllowAnyHeader()
              .AllowAnyOrigin()
              .AllowAnyMethod();
    });
});
#endregion

#region 配置SwaggerApi文档
builder.Services.AddSwaggerGen(options =>
{
    #region 配置IdentityServer
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "在下框输入JWT生成的TOKEN,格式为Bearer 【TOKEN】",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        { new OpenApiSecurityScheme{
            Reference=new OpenApiReference{
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        }, new string[]{ } }
    });
    #endregion

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "乡村文化接口文档v1",
        Description = "乡村文化接口文档v1"
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors("Cors");
app.UseSwagger(c =>
{
    //Change the path of the end point , should also update UI middle ware for this change                
    c.RouteTemplate = "api-doc/moi/{documentName}/swagger.json";
});
//启用中间件服务对swagger-ui，指定Swagger JSON终结点
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api-doc/moi/v1/swagger.json", "RuralCultureWebApi v1");
    //c.RoutePrefix = "swagger";  //默认
    c.RoutePrefix = "api-doc/moi";
});

app.UseAuthorization();

app.MapControllers();

app.Run();
