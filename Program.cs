using System.Reflection;
using Oicana.Example;
using Oicana.Example.Services;
using Oicana.Template;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSingleton<IOicanaService, OicanaService>();
builder.Services.AddScoped<ITemplatingService, TemplatingService>()
    .AddScoped<IStoredBlobService, StoredBlobService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddHostedService<WarmUpTemplates>();

builder
    .RegisterTemplate("certificate", TemplateVersion.From(0, 1, 0))
    .RegisterTemplate("dependency", TemplateVersion.From(0, 1, 0))
    .RegisterTemplate("fonts", TemplateVersion.From(0, 1, 0))
    .RegisterTemplate("invoice", TemplateVersion.From(0, 1, 0))
    .RegisterTemplate("invoice_zugferd", TemplateVersion.From(0, 1, 0))
    .RegisterTemplate("minimal", TemplateVersion.From(0, 1, 0))
    .RegisterTemplate("test", TemplateVersion.From(0, 1, 0))
    .RegisterTemplate("test_multi_input", TemplateVersion.From(0, 1, 0));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Content("Visit the swagger documentation at <a href=\"/swagger\">/swagger</a>", "text/html")).ExcludeFromDescription();

app.Run();