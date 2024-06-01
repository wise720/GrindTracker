
using System.Numerics;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Game.Config;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Hooking;
using Dalamud.Memory;
using Dalamud.Plugin.Services;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.System.Memory;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using FFXIVClientStructs.FFXIV.Client.UI.Shell;
using FFXIVClientStructs.FFXIV.Component.GUI;
using GrindTracker.Extractors;
using System;
using System.Collections.Generic;
using System.Text;
using ValueType = FFXIVClientStructs.FFXIV.Component.GUI.ValueType;

namespace GrindTracker;

internal sealed class Chat : IDisposable 
{
    
    private Plugin Plugin { get; }

    private readonly List<IExtractor> _extractors;
    private Queue<Message> messageQueue;
    
    internal Chat(Plugin plugin) {
        this.Plugin = plugin;
        Plugin.GameInteropProvider.InitializeFromAttributes(this);
        Plugin.ChatGui.ChatMessageUnhandled += MessageHandler;
        _extractors = new List<IExtractor>
        {
            new GilExtractor(),
            new ItemExtractor(),
            new ExpExtractor(),
            new DutyTimerExtractor(),
        };
        
        messageQueue = new Queue<Message>();
        //this.ChangeChannelNameHook?.Enable();
        //this.ReplyInSelectedChatModeHook?.Enable();
        //this.SetChatLogTellTargetHook?.Enable();

        //this.Plugin.Framework.Update += this.InterceptKeybinds;
        //this.Plugin.ClientState.Login += this.Login;
        //this.Login();
    }

    
    private void MessageHandler(XivChatType type, uint senderId, SeString sender, SeString message)
    {
        //messageQueue.Enqueue(new Message(
        //    0,0,type,senderId,sender,message));
        Message msg = new Message(0, 0, type, senderId, sender, message);

        foreach (IExtractor extractor in _extractors)
        {
            extractor.extract(msg, Plugin.Tracker);
        }
        Plugin.Log.Debug("{0} {1}: {2}", type, sender, message);
    }
    public void Dispose() {
        //this.Plugin.ClientState.Login -= this.Login;
        //this.Plugin.Framework.Update -= this.InterceptKeybinds;

        //this.SetChatLogTellTargetHook?.Dispose();
        //this.ReplyInSelectedChatModeHook?.Dispose();
        //this.ChangeChannelNameHook?.Dispose();


        //this.Activated = null;
    }
    
}
