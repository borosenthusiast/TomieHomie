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
using DataManger;

namespace TomieHomie
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
        [PluginService] public static PythonLoader Data { get; set; } = null!;
        [PluginService] public static ClientState ClientState { get; set; } = null!;
        [PluginService] public static Dalamud.Game.Command.CommandManager Commands { get; set; } = null!;
        [PluginService] public static ChatGui Chat { get; set; } = null!;

        public TomieHomie(
            DalamudPluginInterface pi,
            CommandManager commands,
            ChatGui chat,
            ClientState clientState)
        {
            this.pluginInterface = pi;
            this.chat = chat;
            this.clientState = clientState;

            // Get or create a configuration object
            this.config = (Configuration)this.pluginInterface.GetPluginConfig()
                          ?? this.pluginInterface.Create<Configuration>();

            // Initialize the UI
            this.windowSystem = new WindowSystem(typeof(TomieHomie).AssemblyQualifiedName);

            var window = this.pluginInterface.Create<PluginWindow>();
            if (window is not null)
            {
                this.windowSystem.AddWindow(window);
            }

            //this.pluginInterface.UiBuilder.Draw += this.windowSystem.Draw;

            // Load all of our commands
            this.commandManager = new PluginCommandManager<TomieHomie>(this, commands);
        }

        [Command("/example1")]
        [HelpMessage("Example help message.")]
        public void ExampleCommand1(string command, string args)
        {
            // You may want to assign these references to private variables for convenience.
            // Keep in mind that the local player does not exist until after logging in.
            var world = this.clientState.LocalPlayer?.CurrentWorld.GameData;
            this.chat.Print($"Hello, {world?.Name}!");
            PluginLog.Log("Message sent successfully.");
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            this.commandManager.Dispose();

            this.pluginInterface.SavePluginConfig(this.config);

            this.pluginInterface.UiBuilder.Draw -= this.windowSystem.Draw;
            this.windowSystem.RemoveAllWindows();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
