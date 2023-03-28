using Enroot.Api;
using Enroot.Api.Common.Localization;
using Enroot.Application;
using Enroot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddPresentation()
    .AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(Localization.SupportedCultures[0])
    .AddSupportedCultures(Localization.SupportedCultures)
    .AddSupportedUICultures(Localization.SupportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseCors(x => x.AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:5173", "https://localhost:5173")
                            .AllowCredentials());

app.Run();
public partial class Program { }
