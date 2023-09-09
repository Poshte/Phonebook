using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ElasticEmailClient.Api;
using static ElasticEmailClient.ApiTypes;


namespace Phonebook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            ApiKey = "F744AB68D4E5F86C78AAB6CD36FD3BF995F4F4475FF6C82634CF64D4728F67EC4418EFF180E50AE9B2E147134A97484A";

            var task = SendEmail("Hello World from Elastic Email!", "fromAddress@exmple.com",
                "John Tester", new string[] { "toAddress@exmple.com" },
                                "<h1>Hello! This mail was sent by Elastic Email service.<h1>",
                                "Hello! This mail was sent by Elastic Email service.");



            task.ContinueWith(t =>
            {
                if (t.Result == null)
                    Console.WriteLine("Something went wrong. Check the logs.");
                else
                {
                    Console.WriteLine("MsgID to store locally: " + t.Result.MessageID); // Available only if sent to a single recipient
                    Console.WriteLine("TransactionID to store locally: " + t.Result.TransactionID);
                }
            });

            task.Wait();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public async static Task<ElasticEmailClient.ApiTypes.EmailSend> SendEmail(string subject, string fromEmail, string fromName, string[] msgTo, string html, string text)
        {
            try
            {
                return await ElasticEmailClient.Api.Email.SendAsync(subject, fromEmail, fromName, msgTo: msgTo, bodyHtml: html, bodyText: text);
            }
            catch (Exception ex)
            {
                if (ex is ApplicationException)
                    Console.WriteLine("Server didn't accept the request: " + ex.Message);
                else
                    Console.WriteLine("Something unexpected happened: " + ex.Message);

                return null;
            }
        }
    }
}
