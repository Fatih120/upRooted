using System;
using System.Collections.Generic;
using RootApp.Assets;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Services;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayMessageViewModelFactory(MessageFactory, MessageViewModelFactory, AssetLinkWrapperFactory, BitmapCache)
{
	public OverlayMessageViewModel Create(GlobalMessageEvent P_0, string P_1, string? P_2)
	{
		OverlayMessageContainer overlayMessageContainer = new OverlayMessageContainer(P_0.ContainerId, P_0.CommunityId, P_0.ContainerName);
		Dictionary<Uri, AssetLinkWrapper> dictionary = null;
		if (P_0.Packet.ReferenceMaps?.Assets != null)
		{
			dictionary = new Dictionary<Uri, AssetLinkWrapper>();
			foreach (KeyValuePair<string, AssetInformation> asset in P_0.Packet.ReferenceMaps.Assets)
			{
				dictionary[new Uri(asset.Key)] = P_2.Create(asset.Key, asset.Value);
			}
		}
		Message message = P_0.Create(P_0.Packet, dictionary, true, false, overlayMessageContainer);
		MessageViewModel messageViewModel = P_1.Create(message);
		return new OverlayMessageViewModel(P_0, P_1, P_2, message, messageViewModel, P_3);
	}
}
