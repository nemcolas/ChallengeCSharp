using Microsoft.EntityFrameworkCore;
using OdontoPrevCSharp.Data;
using OdontoPrevCSharp.Repositories;
using OdontoPrevCSharp.Repositories.Implementations;
using OdontoPrevCSharp.Services; // Add this using

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container.
builder.Services.AddControllersWithViews();

// Configure HttpClient for ViaCEP service
builder.Services.AddHttpClient<IViaCepService, ViaCepService>(client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br/");
});

// Configurar o contexto do banco de dados com Oracle
builder.Services.AddDbContext<OdontoPrevContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OdontoPrevContext")));

// Registrar os repositórios para injeção de dependência
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IDentistaRepository, DentistaRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<ISinistroRepository, SinistroRepository>();
builder.Services.AddScoped<ITratamentoRepository, TratamentoRepository>();

// Register Model Trainer Service (as Singleton or Scoped depending on usage, Scoped for now)
builder.Services.AddScoped<ModelTrainerService>();

// Register Prediction Service (Singleton is appropriate here as the model is loaded once)
builder.Services.AddSingleton<IPredictionService, PredictionService>();

var app = builder.Build();

// Train and save the ML model on startup
using (var scope = app.Services.CreateScope())
{
    var trainer = scope.ServiceProvider.GetRequiredService<ModelTrainerService>();
    trainer.TrainAndSaveModel();
}


// Configurar o pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


public partial class Program { }

