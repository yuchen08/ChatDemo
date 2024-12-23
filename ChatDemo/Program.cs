using ChatDemo.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5006");
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dapper
builder.Services.AddScoped<IDatabaseService, DapperDatabaseService>();
builder.Services.AddScoped<DapperMemberService>();

builder.Services.AddTransient<QuestionAnswerService>();

// Configure CORS
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure HTTP request pipeline
app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();