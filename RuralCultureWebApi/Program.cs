using IdentityModel.AspNetCore.OAuth2Introspection;
using RuralCultureDataAccess;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//��ȡ��վ�����ļ�
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string MOIConnStr = configuration["ConnectionStrings:MOIConnStr"];

//���������ַ���
builder.Services.AddDbContext(options => options.AddMtrlSqlServer(configuration["ConnectionStrings:MOIConnStr"]));

//����AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

#region ���ÿ���
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

#region ����SwaggerApi�ĵ�
builder.Services.AddSwaggerGen(options =>
{
    #region ����IdentityServer
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "���¿�����JWT���ɵ�TOKEN,��ʽΪBearer ��TOKEN��",
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
        Title = "����Ļ��ӿ��ĵ�v1",
        Description = "����Ļ��ӿ��ĵ�v1"
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
//�����м�������swagger-ui��ָ��Swagger JSON�ս��
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api-doc/moi/v1/swagger.json", "RuralCultureWebApi v1");
    //c.RoutePrefix = "swagger";  //Ĭ��
    c.RoutePrefix = "api-doc/moi";
});

app.UseAuthorization();

app.MapControllers();

app.Run();
