using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NetCore2.MVC.BasicAuthentication.Models;

namespace NetCore2.MVC.BasicAuthentication
{
    public class Program
    {

        public static List<AdminUser> AdminUserList { get; set; }

        public static void Main(string[] args)
        {

            string baseDir = Directory.GetCurrentDirectory();

            string f = baseDir + Path.DirectorySeparatorChar + "config" + Path.DirectorySeparatorChar + "admin_users.json";

            using (StreamReader file = File.OpenText(f))
            {

                JsonSerializer serializer = new JsonSerializer();
                AdminUserList = (List<AdminUser>)serializer.Deserialize(file, typeof(List<AdminUser>));
            }

            IConfigurationRoot config;

                config = new ConfigurationBuilder()
                 .SetBasePath(baseDir)
                .AddJsonFile("hosting.json", true)
                .Build();

            string url = config.GetValue<string>("server.urls");

            var host = new WebHostBuilder()
               .UseKestrel()
               .UseLibuv(options =>
               {
                   options.ThreadCount = 10;
               })
                 .UseUrls(url)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

    }
}
