var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5006");
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
        policy => {
            policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Enable cross-origin resource sharing (CORS)
app.UseCors(policy =>
policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod());

//// Enable static files (to serve the Vue.js app)
//app.UseStaticFiles();

//// Serve the Vue.js app in development mode
//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "chatapp";
//    if (app.Environment.IsDevelopment())
//    {
//        spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
//    }
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();