using Context_Layer.Models;
using Data_Services_Layer.DTOS;
using IServices_Repository_Layer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services_Repository_Layer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<edulmsContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
   b => b.MigrationsAssembly(typeof(edulmsContext).Assembly.FullName)));



//JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration
                                                                                     .GetSection("Jwt")
                                                                                     .Get<DtoTokenManager>().SecretKey))
    };
});

#region Inject Services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IAssessmentsRepository, AssessmentsRepository>();
builder.Services.AddTransient<IAssessmentsQuestionsRepository, AssessmentsQuestionsRepository>();
builder.Services.AddTransient<ITrueOrFalseAssessmentRepository, TrueOrFalseAssessmentRepository>();
builder.Services.AddTransient<IAssessment_Match_Repository, Assessment_Match_Repository>();
builder.Services.AddTransient<IAssessment_Options_Repository, Assessment_Options_Repository>();
builder.Services.AddTransient<IAssessment_Text_Repository, Assessment_Text_Repository>();
builder.Services.AddTransient<IAssessmentQuestionsRelationRepository, AssessmentQuestionsRelationRepository>();
builder.Services.AddTransient<IAssessmentEnrolRepository, AssessmentEnrolRepository>();
builder.Services.AddTransient<IAssessmentAnswerRepository, AssessmentAnswerRepository>();

#endregion

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
