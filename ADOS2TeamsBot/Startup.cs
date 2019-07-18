// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EmptyBot v4.3.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Bot.Builder.Teams.Middlewares;
using System;
using ADOS2Teams.ADOSSearch;

namespace ADOS2Teams
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<ADOSSearchHandler>(s =>
                {
                    s.BaseAddress = new Uri($"{Configuration["ADOSBaseUri"]}/{Configuration["ADOSCollection"]}/");
                    s.DefaultRequestHeaders.Add("Accept", "application/json");
                    s.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", "", Configuration["ADOSSecret"]))));
                }
            );

            services.AddBot<ADOS2TeamsBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(this.Configuration);
                options.Middleware.Add(new TeamsMiddleware(new ConfigurationCredentialProvider(this.Configuration)));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseBotFramework();
        }
    }
}
