using PapersApi.Models;
using PapersApi.Services;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

builder.Services.Configure<PapersDatabaseSettings>(
    builder.Configuration.GetSection("PapersDatabase"));

// Add services to the container.

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<ProjectsService>();

builder.Services.AddControllers()
.AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);;
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
