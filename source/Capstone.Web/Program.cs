using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Capstone.Web
{
	/// <summary>
	/// Contains the entry-point(s) for the application.
	/// </summary>
	public static class Program
	{
		/// <summary>
		/// The application console title.
		/// </summary>
		public const string Title = "Capstone";

		/// <summary>
		/// The entry-point for the application.
		/// </summary>
		/// <param name="args">The command-line arguments for the application.</param>
		public static void Main(params string[] args)
		{
			MainAsync()
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		/// The asychronous entry-point for the application.
		/// </summary>
		/// <param name="args">The command-line arguments for the application.</param>
		/// <returns></returns>
		public static async Task MainAsync(params string[] args)
		{
			var cancellationTokenSource =
				new CancellationTokenSource();

			void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
			{
				cancellationTokenSource.Cancel();
				e.Cancel = true;
			}

			Console.Title = Title;
			Console.CancelKeyPress += OnCancelKeyPress;

			// Run the web service asynchronously.
			await WebHost.CreateDefaultBuilder(args)
				//.UseUrls("http://*:80", "https://*:81")
				.UseUrls("http://capstone.ngrok.io:5000", "https://capstone.ngrok.io:5001")
				.UseStartup<ProgramStartup>()
				.Build()
				.RunAsync(cancellationTokenSource.Token);

			Console.CancelKeyPress -= OnCancelKeyPress;
		}
	}
}
