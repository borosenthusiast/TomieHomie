using Dalamud.Game.ClientState;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Plugin;
using TomieHomie.Attributes;
using System;
using Dalamud.Data;
using Dalamud.IoC;


namespace TomieHomie.Plugin
{
    public class TomieHomie : IDalamudPlugin
    {
        private readonly DalamudPluginInterface pluginInterface;
        private readonly ChatGui chat;
        private readonly ClientState clientState;

        private readonly PluginCommandManager<TomieHomie> commandManager;
        private readonly Configuration config;
        private readonly WindowSystem windowSystem;

        public string Name => "TomeHome";

        [PluginService] public static DalamudPluginInterface PluginInterface { get; set; } = null!;
        //[PluginService] public static PythonLoader Data { get; set; } = null!;
        [PluginService] public static ClientState ClientState { get; set; } = null!;
        [PluginService] public static CommandManager Commands { get; set; } = null!;
        [PluginService] public static ChatGui Chat { get; set; } = null!;

        public TomieHomie(
            DalamudPluginInterface pi,
            CommandManager commands,
            ChatGui chat,
            ClientState clientState)
        {
            pluginInterface = pi;
            this.chat = chat;
            this.clientState = clientState;

            // Get or create a configuration object
            config = (Configuration)pluginInterface.GetPluginConfig()
                          ?? pluginInterface.Create<Configuration>();

            // Initialize the UI
            windowSystem = new WindowSystem(typeof(TomieHomie).AssemblyQualifiedName);

            var window = pluginInterface.Create<PluginWindow>();
            if (window is not null)
            {
                windowSystem.AddWindow(window);
            }

            //this.pluginInterface.UiBuilder.Draw += this.windowSystem.Draw;

            // Load all of our commands
            commandManager = new PluginCommandManager<TomieHomie>(this, commands);
        }

        [Command("/example1")]
        [HelpMessage("Example help message.")]
        public void ExampleCommand1(string command, string args)
        {
            // You may want to assign these references to private variables for convenience.
            // Keep in mind that the local player does not exist until after logging in.
            var world = clientState.LocalPlayer?.CurrentWorld.GameData;
            chat.Print($"Hello, {world?.Name}!");
            PluginLog.Log("Message sent successfully.");
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            commandManager.Dispose();

            pluginInterface.SavePluginConfig(config);

            pluginInterface.UiBuilder.Draw -= windowSystem.Draw;
            windowSystem.RemoveAllWindows();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
