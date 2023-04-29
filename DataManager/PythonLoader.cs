﻿using System.Collections.Generic;
using IronPython.Hosting;

namespace TomieHomie.DataManager
{

    public class PythonLoader
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
            List<string> argv = new List<string>
            {
                capacity.ToString()
            };
            engine.GetSysModule().SetVariable("argv", argv);
            engine.ExecuteFile(path, scope);

            // Example string {'Hullbreaker Isle': {'run_times': 1, 'total_time': 14}, 'The Tam-Tara Deepcroft (Hard)': {'run_times': 17, 'total_time': 221}}
            var results = scope.GetVariable("results");

            return results.ToString() + " amount of times in a play session of " + capacity + " minutes!"; 

            
        }
    }
}
