using System;
using System.IO;

namespace NginxService
{
	internal class NginxExeLocator
	{
		/// <summary>
		/// Gets the full path to the nginx.exe. Assumes this service executable is
		/// in the same directory as nginx.exe
		/// </summary>
		/// <returns>The full path to nginx.exe</returns>
		public string GetNginxExePath()
		{
			var nginxExePath = Path.Combine(GetCurrentExecutingDirectory(), "nginx.exe");
			if (!File.Exists(nginxExePath))
			{
				throw new FileNotFoundException("Could not find nginx.exe in the current directory");
			}
			return nginxExePath;
		}

		/// <summary>
		/// Gets the current assemblies current location (pre-shadow copy).
		/// </summary>
		/// <remarks>
		/// Under ASP.NET this will return the bin directory.
		/// </remarks>
		/// <returns>The full local directory path</returns>
		public string GetCurrentExecutingDirectory()
		{
			return CreateLocalWindowsPath(GetType().Assembly.CodeBase);
		}

		/// <summary>
		/// Turns a file Uri into a local windows path.
		/// file://c:/somedir/somefile.txt -> c:\somedir\somefile.txt
		/// </summary>
		/// <param name="fileUriPath">Full Uri file path</param>
		/// <returns>The Windows friendly path</returns>
		private static string CreateLocalWindowsPath(string fileUriPath)
		{
			string filePath = new Uri(fileUriPath).LocalPath;
			return Path.GetDirectoryName(filePath);
		}
	}
}
