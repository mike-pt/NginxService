using System.Diagnostics;

namespace NginxService
{
	public class NginxController
	{
		private Process _nginxProcess;
		private string _nginxExePath;

		public void Start()
		{
			Stop();
			StartMasterProcess();
		}

		public void Stop()
		{
			if (IsRunning())
			{
				SendShutdownCommandToMasterProcess();
				StopMasterProcess();
			}
		}

		public bool IsRunning()
		{
			return _nginxProcess != null && !_nginxProcess.HasExited;
		}

		private void StopMasterProcess()
		{
			_nginxProcess.Close();
			_nginxProcess = null;
		}

		private void StartMasterProcess()
		{
			_nginxProcess = new Process();
			_nginxProcess.StartInfo.FileName = NginxExePath;
			_nginxProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			_nginxProcess.Start();
		}

		private void SendShutdownCommandToMasterProcess()
		{
			var signalProcess = new Process();
			signalProcess.StartInfo.FileName = NginxExePath;
			signalProcess.StartInfo.Arguments = "-s stop";
			signalProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			signalProcess.Start();
			signalProcess.WaitForExit(10000);
		}

		private string NginxExePath
		{
			get
			{
				if (string.IsNullOrEmpty(_nginxExePath))
				{
					_nginxExePath = new NginxExeLocator().GetNginxExePath();
				}
				return _nginxExePath;
			}
		}
	}
}
