using RecipeService.Configurations;
using RecipeService.DB;
using RecipeService.ExceptionFilters;
using RecipeService.Interfaces;
using RecipeService.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options =>  options.Filters.Add<RecipeExceptionFilter>()
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
                     options =>
                     {
                         var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                         options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                     }
    );

builder.Services.AddInfrastructureApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //use developer eception page in developement page
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
