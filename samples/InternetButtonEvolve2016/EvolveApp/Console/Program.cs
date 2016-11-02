using System;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Nito.AsyncEx;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static System.Console;
using static System.Net.WebUtility;
using Particle;

namespace Console
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			AsyncContext.Run(() => MainAsync(args));
		}

		static async void MainAsync(string[] args)
		{

			//$ curl https://api.particle.io/oauth/token 
			//-u particle:particle -d grant_type=password 
			//-d username=joe@example.com -d password=SuperSecret

			await ParticleCloud.SharedInstance.CreateOAuthClientAsync("", "CreateWebhook");

			var eventBusName = "";//internetbuttonhub
			var eventBusPolicyKeyName = "";//D1
			var eventBusPrimaryKey = "";//SWxP6/1JPi4PjEX1lseZ4dHDC8CUmTpOeWIlfYL2PX8=

			string uri = "https://InternetButton-ns.servicebus.windows.net";

			string formData =
				"{" +
					"\"gameid\": \"{{g}}\"," +
					"\"activity\": \"{{a}}\"," +
					"\"value\": \"{{v}}\"," +
					"\"timecreated\": \"{{SPARK_PUBLISHED_AT}}\"," +
					"\"guid\": \"{{SPARK_CORE_ID}}\"" +
				"}";

			var requestContent = new FormUrlEncodedContent(new[] {
				new KeyValuePair<string, string> ("event", "SimonSays"),
				new KeyValuePair<string, string> ("url", $"{uri}/{eventBusName}/messages"),
				new KeyValuePair<string, string> ("json", formData),
				new KeyValuePair<string, string> ("azure_sas_token", "{\"key_name\": \"" + eventBusPolicyKeyName + "\", \"key\": \"" + eventBusPrimaryKey + "\"}"),
				new KeyValuePair<string, string> ("mydevices","true"),
			});

			try
			{
				using (var client = new HttpClient())
				{
					var response = await client.PostAsync(
						"https://api.particle.io/v1/webhooks",
						requestContent
					);

					var particleResponse = await response.Content.ReadAsStringAsync();
					WriteLine(particleResponse);
				}
			}
			catch (Exception e)
			{
				WriteLine(e.Message);
			}

			WriteLine("Press Enter to quit");
			Read();
		}
	}
}