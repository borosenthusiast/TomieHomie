using TomieHomie.Plugin;
using TomieHomie.Constants;
using Dalamud.Game.Command;
using TomieHomie.DataManager;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;
using System;

namespace TomieHomie.Managers
{
    public static class CommandManager
    {

        /// <summary>
        /// Sends the chat output to window.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        private static void CommandHandler(string cmd, string arg)
        {
            string outputLog = string.Empty;
            int capacity;
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
                TomieHomie.DataManager.PythonLoader pyLoader = new TomieHomie.DataManager.PythonLoader(TomieHomieConstants.PATH, capacity);
                outputLog = pyLoader.loadPythonOutput();
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
            Plugin.TomieHomie.Chat.Print(outputLog);
        }
        /// <summary>
        /// load command
        /// </summary>
        public static void load()
        {
            Plugin.TomieHomie.Commands.RemoveHandler(TomieHomieConstants.OUTPUTCOMMAND);
            Plugin.TomieHomie.Commands.AddHandler(TomieHomieConstants.OUTPUTCOMMAND, new CommandInfo(CommandHandler)
            {
                ShowInHelp = true,
                HelpMessage = "/mogtome will give you the output for most optimal way to spend your time."
            });
        }

        public static void unload()
        {
            Plugin.TomieHomie.Commands.RemoveHandler(TomieHomieConstants.OUTPUTCOMMAND);
        }
    }
}
