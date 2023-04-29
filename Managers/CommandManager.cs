using TomieHomie.Plugin;
using TomieHomie.Constants;
using Dalamud.Game.Command;
using TomieHomie.DataManager;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace TomieHomie.Managers
{
    public static class CommandManager
    {
        /// <summary>
        /// Sends the chat output to window.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        private static async void CommandHandler(string cmd, string arg)
        {
            string outputLog = string.Empty;
            int capacity;
            Plugin.TomieHomie.Chat.Print(TomieHomieConstants.CALC);
            try
            {
                capacity = int.Parse(arg);
                if (capacity < 0)
                {
                    throw new FormatException("Input for capacity out of bounds.");
                }
            }
            catch
            {
                Plugin.TomieHomie.Chat.Print(TomieHomieConstants.ARGFAIL);
                return;
            }
            try
            {
                PythonLoader pyLoader = new PythonLoader(capacity);
                await pyLoader.LoadPythonOutput();
                outputLog = pyLoader.Output;
            }
            catch
            {
                Plugin.TomieHomie.Chat.Print(TomieHomieConstants.PYFAIL);
                return;
            }
            if (outputLog == string.Empty)
            {
                Plugin.TomieHomie.Chat.Print(TomieHomieConstants.PYOUTPUTFAIL);
                return;
            }
            string[] outputSep = outputLog.Split('\n');
            foreach (string s in outputSep)
            {
                string cleaned = s.Replace("\n", "").Replace("\r", "");
                if (s != string.Empty)
                    Plugin.TomieHomie.Chat.Print(cleaned);
            }

        }
        /// <summary>
        /// load command
        /// </summary>
        public static void Load()
        {
            Plugin.TomieHomie.Commands.RemoveHandler(TomieHomieConstants.OUTPUTCOMMAND);
            Plugin.TomieHomie.Commands.AddHandler(TomieHomieConstants.OUTPUTCOMMAND, new CommandInfo(CommandHandler)
            {
                ShowInHelp = true,
                HelpMessage = "/mogtome will give you the output for most optimal way to spend your time."
            });
        }

        public static void Unload()
        {
            Plugin.TomieHomie.Commands.RemoveHandler(TomieHomieConstants.OUTPUTCOMMAND);
        }
    }
}
