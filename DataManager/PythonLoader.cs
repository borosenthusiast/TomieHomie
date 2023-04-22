using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace TomieHomie.DataManager
{

    internal class PythonLoader
    {
        private string path;
        private int capacity;
        public PythonLoader(string path, int capacity)
        {
            this.path = path;
            this.capacity = capacity;
        }
        
        private string formatOutput(string results)
        {
            string formattedResults = results;



            return formattedResults;
        }

        public string loadPythonOutput()
        {
            var engine = Python.CreateEngine();          
            dynamic scope = engine.CreateScope();
            scope.SetVariable("capacity", capacity);
            engine.ExecuteFile(path, scope);

            // Example string {'Hullbreaker Isle': {'run_times': 1, 'total_time': 14}, 'The Tam-Tara Deepcroft (Hard)': {'run_times': 17, 'total_time': 221}}
            var results = scope.GetVariable("results");

            return results.ToString() + " amount of times in a play session of " + capacity + " minutes!"; 

            
        }
    }
}
