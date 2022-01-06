using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.IRepository;
using Server.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using XebecPortal.Client.GamifiedEnvBeta.Utils;

using XebecPortal.Server.JobPortalTestEnv.Helpers.Repositories;
using Server.Configurations;

namespace XebecPortal.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"));

            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IJobTestRepo, JobTestRepo>();
            services.AddTransient<ICandidateTestRepo, CandidateTestRepo>();
            services.AddTransient<IUsersCustomRepo, UsersCustomRepo>();

            services.AddTransient<IApplicationPhaseHelperRepository, ApplicationPhaseHelperRepository>();


            services.AddAutoMapper(typeof(MapperInitializer_));

            /*newly added*/
            services.AddSingleton<State>();
            

            services.AddTransient<IUserDb, UserDb>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            
            });

            services.AddAuthentication(options =>
            {

                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddCookie(options =>
            {
                options.LoginPath = "/api/ThirdPartyUser/GoogleSignIn";
            })
            .AddLinkedIn(Linkedinoptions =>
            {
                IConfigurationSection linkedinAuthNSection =
                Configuration.GetSection("Authentication:Linkedin");

                Linkedinoptions.ClientId = linkedinAuthNSection["ClientId"];
                Linkedinoptions.ClientSecret = linkedinAuthNSection["ClientSecret"];
                Linkedinoptions.Scope.Add("r_liteprofile");
                Linkedinoptions.Scope.Add("r_emailaddress");
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateAudience = true,
                    ValidAudience = "domain.com",
                    ValidateIssuer = true,
                    ValidIssuer = "domain.com",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS IS THE SECRET KEY"))

                };

            })
            // Add GitHub authentication
            .AddGitHub("Github", options =>
            {
                options.ClientId = Configuration["Authentication:GitHub:ClientId"]; // client id from registering github app
                options.ClientSecret = Configuration["Authentication:GitHub:ClientSecret"]; ; // client secret from registering github app
                options.Scope.Add("user:email"); // add additional scope to obtain email address
                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = OnCreatingGitHubTicket()
                }; // Event to capture when the authentication ticket is being created
            })
            // Add Google Authentication
            .AddGoogle("Google", options =>
                {
                    options.ClientId = Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                    options.ClaimActions.Clear();
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                    options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = OnCreatingGoogleTicket()
                    }; // Event to capture when the authentication ticket is being created
                })
                //twiiter
                .AddTwitter(twitterOptions =>
                {
                    twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                    twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
                    twitterOptions.RetrieveUserDetails = true;
                });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(o =>
          {
              o.AddPolicy("AllowAll", builder =>
              {
                  builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
              });
          });

        }

        /*Begin get data from oauth*/
        private static Func<OAuthCreatingTicketContext, Task> OnCreatingGitHubTicket()
        {
            return async context =>
            {
                var fullName = context.Identity.FindFirst("urn:github:name").Value;
                var email = context.Identity.FindFirst(ClaimTypes.Email).Value;

                //Todo: Add logic here to save info into database

                // this Task.FromResult is purely to make the code compile as it requires a Task result
                await Task.FromResult(true);
            };
        }

        private static Func<OAuthCreatingTicketContext, Task> OnCreatingGoogleTicket()
        {
            return async context =>
            {
                var firstName = context.Identity.FindFirst(ClaimTypes.GivenName).Value;
                var lastName = context.Identity.FindFirst(ClaimTypes.Surname)?.Value;
                var email = context.Identity.FindFirst(ClaimTypes.Email).Value;
              
                //Todo: Add logic here to save info into database

                /*Testing*/                
                

                /*End Testing*/
                // this Task.FromResult is purely to make the code compile as it requires a Task result
                await Task.FromResult(true);
            };
        }
        /*End get data from oauth*/

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseCors("AllowAll");

            /*new addition*/
            app.UseCookiePolicy();
            /*end new addition*/

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
