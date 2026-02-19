// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.!AvaloniaResources
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

public static void Populate_003A_002FResources_002FSounds_002Eaxaml(IServiceProvider P_0, ResourceDictionary P_1)
{
	CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ResourceDictionary>(P_0, new object[1] { NamespaceInfo_003A_002FResources_002FSounds_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Resources/Sounds.axaml")
	{
		RootObject = P_1,
		IntermediateRoot = P_1
	};
	if (P_1 is ResourceDictionary resourceDictionary)
	{
		resourceDictionary.EnsureCapacity(resourceDictionary.Count + 15);
	}
	((IDictionary<object, object>)P_1).Add((object)"Deafen", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_Deafen.wav");
	((IDictionary<object, object>)P_1).Add((object)"Undeafen", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_Undeafen.wav");
	((IDictionary<object, object>)P_1).Add((object)"IncomingRing", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_IncomingRing.wav");
	((IDictionary<object, object>)P_1).Add((object)"JoinVoice", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_JoinVoiceChat.wav");
	((IDictionary<object, object>)P_1).Add((object)"LeaveVoice", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_LeaveVoiceChat.wav");
	((IDictionary<object, object>)P_1).Add((object)"SelfLeaveVoice", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_SelfLeaveVoiceChat.wav");
	((IDictionary<object, object>)P_1).Add((object)"MediaShareStarted", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_MediaShareStarted.wav");
	((IDictionary<object, object>)P_1).Add((object)"MediaShareStopped", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_MediaShareStopped.wav");
	((IDictionary<object, object>)P_1).Add((object)"SelfMediaShareStopped", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_SelfMediaShareStopped.wav");
	((IDictionary<object, object>)P_1).Add((object)"MessageReceived", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_MessageReceived.wav");
	((IDictionary<object, object>)P_1).Add((object)"Mute", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_Mute.wav");
	((IDictionary<object, object>)P_1).Add((object)"Unmute", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_Unmute.wav");
	((IDictionary<object, object>)P_1).Add((object)"NotificationReceived", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_NotificationReceived.wav");
	((IDictionary<object, object>)P_1).Add((object)"OutgoingRing", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_OutgoingRing.wav");
	((IDictionary<object, object>)P_1).Add((object)"Poke", (object)"avares://RootApp.Client.Avalonia/Resources/Assets/SoundEffects/Sounds/Root_AUX_Poke.wav");
	if (P_1 is StyledElement styledElement)
	{
		NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
	}
	context.AvaloniaNameScope.Complete();
}
