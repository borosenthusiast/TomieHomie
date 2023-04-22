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

        public string loadPythonOutput()
        {
            var engine = Python.CreateEngine();          
            dynamic scope = engine.CreateScope();
            scope.SetVariable("capacity", capacity);
            engine.ExecuteFile(path, scope);

            var dungeons = scope.GetVariable("dungeons");
            var results = scope.GetVariable("results");

            return "Dungeons to run today: " + dungeons.ToString() + " " + results.ToString() + " amount of times in a play session of " + capacity + " minutes!"; 

            
        }
    }
}
