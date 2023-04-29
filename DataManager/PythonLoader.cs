using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TomieHomie.DataManager
{

    public class PythonLoader
    {
        private string path;
        private int capacity;
        private string output;
        public string Output { get { return output; } }
        public PythonLoader(int capacity)
        {
            this.path = Plugin.TomieHomie.PluginInterface.AssemblyLocation.DirectoryName + Constants.TomieHomieConstants.PATH;
            this.capacity = capacity;
        }
        

        public async Task LoadPythonOutput()
        {
            // Set up the process start info
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = path;
            startInfo.Arguments = capacity.ToString();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            // Start the process
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            // Read the output
            string output = await process.StandardOutput.ReadToEndAsync();

            // Wait for the process to exit
            await Task.Run(process.WaitForExit);
            this.output = output;
            // Example string {'Hullbreaker Isle': {'run_times': 1, 'total_time': 14}, 'The Tam-Tara Deepcroft (Hard)': {'run_times': 17, 'total_time': 221}}



        }
    }
}
