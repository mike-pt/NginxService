using Topshelf;

namespace NginxService
{
	class Program
	{
		static void Main(string[] args)
		{
			HostFactory.Run(x =>
			{
				x.Service<NginxController>(s => 
				{
					s.ConstructUsing(name => new NginxController());
					s.WhenStarted(tc => tc.Start());
					s.WhenStopped(tc => tc.Stop());
				});
				x.RunAsNetworkService();
				x.StartAutomatically();

				x.SetDescription("Nginx web server");
				x.SetDisplayName("nginx");
				x.SetServiceName("nginx");
			});
		}
	}
}
