using System;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Component.GUI;
using GrindTracker.Windows;

namespace GrindTracker;

public sealed class Plugin : IDalamudPlugin
{
    private const string CommandName = "/pmycommand";
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;
    [PluginService] internal static DalamudPluginInterface Interface { get; private set; } = null!;
    [PluginService] internal static IChatGui ChatGui { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static ICondition Condition { get; private set; } = null!;
    [PluginService] internal static IDataManager DataManager { get; private set; } = null!;
    [PluginService] internal static IFramework Framework { get; set; } = null!;
    [PluginService] internal static IGameGui GameGui { get; private set; } = null!;
    [PluginService] internal static IKeyState KeyState { get; private set; } = null!;
    [PluginService] internal static IObjectTable ObjectTable { get; private set; } = null!;
    [PluginService] internal static IPartyList PartyList { get; private set; } = null!;

    [PluginService] internal static IDutyState DutyState { get; private set; } = null!;
    //[PluginService] internal static ITargetManager TargetManager { get; private set; } = null!;
    [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] internal static IGameInteropProvider GameInteropProvider { get; private set; } = null!;
    [PluginService] internal static IGameConfig GameConfig { get; private set; } = null!;
    [PluginService] internal static INotificationManager Notification { get; private set; } = null!;
    [PluginService] internal static IAddonLifecycle AddonLifecycle { get; private set; } = null!;
    

    internal Configuration Configuration = null!;
    
    public readonly WindowSystem WindowSystem = new("GrindTracker");
    private ConfigWindow ConfigWindow { get; init; }
    private MainWindow MainWindow { get; init; }
    
    public Tracker Tracker { get; set; }
    public static  DataLoader DataLoader  = new ();
    private Chat Chat { get; set; }
    
#pragma warning disable CS8618
    public Plugin()
    {
        
        #if DEBUG
        ChatGui.Print("DEBUG mode Active");
        #endif
        try
        {
            //GameStarted = Process.GetCurrentProcess().StartTime.ToUniversalTime();

            Configuration = Interface.GetPluginConfig() as Configuration ?? new Configuration();
            
            // ITextureProvider takes care of the image caching and dispose
            Log.Fatal("loaded sample");
            ConfigWindow = new ConfigWindow(this);
            MainWindow = new MainWindow(this);
            Chat = new Chat(this);
            WindowSystem.AddWindow(ConfigWindow);
            WindowSystem.AddWindow(MainWindow);
            Tracker = new Tracker();
            CommandManager.AddHandler("/gt", new CommandInfo((command, arguments) => MainWindow.Toggle()) { HelpMessage = "opens gui" });
            CommandManager.AddHandler("/gts", new CommandInfo(startTracker) { HelpMessage = "starts tracker" });
            CommandManager.AddHandler("/gte", new CommandInfo(((command, arguments) => Tracker.Stop())) { HelpMessage = "ends tracker" });
            CommandManager.AddHandler("/gtr", new CommandInfo(((command, arguments) => Tracker = new Tracker())) { HelpMessage = "resets tracker" });
            CommandManager.AddHandler("/gtp", new CommandInfo((command, arguments) => Tracker.Print()) { HelpMessage = "prints tracker" });
            DutyState.DutyStarted += DutyStarted;
            DutyState.DutyCompleted += DutyCompleted;
            Plugin.DataLoader.loadData(this);

            Interface.UiBuilder.Draw += DrawUI;

            // This adds a button to the plugin installer entry of this plugin which allows
            // to toggle the display status of the configuration ui
            Interface.UiBuilder.OpenConfigUi += ToggleConfigUI;

            // Adds another button that is doing the same but for the main ui of the plugin
            Interface.UiBuilder.OpenMainUi += ToggleMainUI;
        }
        catch
        {
            Dispose();
            throw;
        }
    }

#pragma warning restore CS8618
    
    public void Dispose()
    {
        WindowSystem?.RemoveAllWindows();
        Tracker.Stop();
        ConfigWindow?.Dispose();
        MainWindow?.Dispose();
        Chat?.Dispose();
        CommandManager?.RemoveHandler(CommandName);
    }

    private void OnCommand(string command, string args)
    {
        // in response to the slash command, just toggle the display status of our main ui
        ToggleMainUI();
    }

    private void startTracker(string s1, string s2)
    {
        Tracker = new Tracker();
    }

    private void  DutyStarted(object? event_obj, ushort eventid )
    {
        //var val = AtkArrayDataHolder.Addresses.GetStringArrayData.Value+58;
        //Log.Error("{0}, {1}",event_obj ?? "", eventid);
        if (Configuration.timeOnlyDuty)
        {
            Tracker.Start();
        }
    }

    private void DutyCompleted(object? event_obj, ushort eventid)
    {
        if (Configuration.timeOnlyDuty)
        {
            Tracker.Stop();
        }
    }
    private void DrawUI() => WindowSystem.Draw();

    public void ToggleConfigUI() => ConfigWindow.Toggle();
    public void ToggleMainUI() => MainWindow.Toggle();
}

