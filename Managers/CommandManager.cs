using TomieHomie.Plugin;
using TomieHomie.Constants;
using Dalamud.Game.Command;
using TomieHomie.DataManager;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;

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
            try
            {

            }
            catch
            {
                Plugin.TomieHomie.Chat.Print(TomieHomieConstants.pyFail);
                return;
            }
        }
        public static void load()
        {
            Plugin.TomieHomie.Commands.RemoveHandler(TomieHomieConstants.outputCommand);
            Plugin.TomieHomie.Commands.AddHandler(TomieHomieConstants.outputCommand, new CommandInfo(CommandHandler)
            {
                ShowInHelp = true,
                HelpMessage = "/mogtome will give you the output for most optimal way to spend your time."
            });
        }
    }
}
