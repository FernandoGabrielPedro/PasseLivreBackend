using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PasseLivre.Api.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => {
   options.ListenLocalhost(5000);
});

builder.Services.AddDbContext<UsuarioContext>(options => 
{
    options
    .UseNpgsql("Host=localhost;Database=Univali;Username=postgres;Password=123456");
}
);


// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(setupAction =>
       {
           setupAction.InvalidModelStateResponseFactory = context =>
           {
               // Cria a fábrica de um objeto de detalhes de problema de validação
               var problemDetailsFactory = context.HttpContext.RequestServices
                   .GetRequiredService<ProblemDetailsFactory>();


               // Cria um objeto de detalhes de problema de validação
               var validationProblemDetails = problemDetailsFactory
                   .CreateValidationProblemDetails(
                       context.HttpContext,
                       context.ModelState);


               // Adiciona informações adicionais não adicionadas por padrão
               validationProblemDetails.Detail =
                   "See the errors field for details.";
               validationProblemDetails.Instance =
                   context.HttpContext.Request.Path;


               // Relata respostas do estado de modelo inválido como problemas de validação
               validationProblemDetails.Type =
                   "https://courseunivali.com/modelvalidationproblem";
               validationProblemDetails.Status =
                   StatusCodes.Status422UnprocessableEntity;
               validationProblemDetails.Title =
                   "One or more validation errors occurred.";


               return new UnprocessableEntityObjectResult(
                   validationProblemDetails)
               {
                   ContentTypes = { "application/problem+json" }
               };
           };
       });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new()
//     {
//         // Declaramos o que deverá ser validado
//         // O tempo de expiração do token é validado automaticamente.
//         // Obriga a validação do emissor
//         ValidateIssuer = true,
//         // Obriga a validação da audiência
//         ValidateAudience = true,
//         // Obriga a validação da chave de assinatura`
//         ValidateIssuerSigningKey = true,

//         // Agora declaramos os valores das propriedades que serão validadas
//         // Apenas tokens  gerados por esta api serão considerados válidos.
//         ValidIssuer = builder.Configuration["Authentication:Issuer"],
//         // Apenas tokens desta audiência serão considerados válidos.
//         ValidAudience = builder.Configuration["Authentication:Audience"],
//         // Apenas tokens com essa assinatura serão considerados válidos.
//         IssuerSigningKey = new SymmetricSecurityKey(
//             Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!))
//     };
// });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
