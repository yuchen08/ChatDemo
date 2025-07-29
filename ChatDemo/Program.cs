using ChatDemo.Contracts;
using ChatDemo.Application;
using ChatDemo.Implementation;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5006");

// 切換是否使用Mock資料
bool useMock = false; // ← 只要改這裡即可切換

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (useMock)
{
    // 註冊 Mock 服務
    builder.Services.AddScoped<IMemberService, MockMemberService>();
    builder.Services.AddScoped<IQuestionAnswerService, MockQuestionAnswerService>();
    // Mock 不需要資料庫服務
    builder.Services.AddScoped<IDatabaseService, DapperDatabaseService>(); // 若有用到還是要註冊
}
else
{
    // 註冊真實服務
builder.Services.AddScoped<IDatabaseService, DapperDatabaseService>();
    builder.Services.AddScoped<IMemberService, DapperMemberService>();
    builder.Services.AddScoped<IQuestionAnswerService, OllamaService>();
}

// 註冊 Application 層的應用服務
builder.Services.AddScoped<MemberApplicationService>();
builder.Services.AddScoped<QuestionAnswerApplicationService>();

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