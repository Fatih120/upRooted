using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Emojis;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Client.Domain.Helpers.Store.State.Emoji;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Messages;

public class ReactionViewModel : ViewModelBase<ReactionViewModel>
{
	private readonly BitmapCache _bitmapCache;

	private readonly ILocalDataStore _localDataStore;

	private readonly IGlobalUserCacheService _globalUserCacheService;

	private readonly string? _emojiUri;

	private readonly Emoji? _emoji;

	[CompilerGenerated]
	private string <CombinedUsername>k__BackingField = string.Empty;

	[CompilerGenerated]
	private string <ExtraCount>k__BackingField = string.Empty;

	[CompilerGenerated]
	private Reaction <Reaction>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? reactionSelectedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? loadReactionDataCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CombinedUsername
	{
		get
		{
			return <CombinedUsername>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<string>.Default.Equals(<CombinedUsername>k__BackingField, text))
			{
				<CombinedUsername>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CombinedUsername);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ExtraCount
	{
		get
		{
			return <ExtraCount>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<string>.Default.Equals(<ExtraCount>k__BackingField, text))
			{
				<ExtraCount>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ExtraCount);
			}
		}
	}

	public Reaction Reaction
	{
		[CompilerGenerated]
		get
		{
			return <Reaction>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Reaction>k__BackingField = reaction;
		}
	}

	public Message Message { get; }

	public Task<BitmapWrapper?> EmojiAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(_emojiUri, _emoji?.Shortname);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ReactionSelectedCommand => reactionSelectedCommand ?? (reactionSelectedCommand = new AsyncRelayCommand(ReactionSelectedAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand LoadReactionDataCommand => loadReactionDataCommand ?? (loadReactionDataCommand = new AsyncRelayCommand(LoadReactionDataAsync));

	public ReactionViewModel(Reaction P_0, Message P_1, EmojiHelper P_2, BitmapCache P_3, ILocalDataStore P_4, IGlobalUserCacheService P_5)
		: base((IValidator<ReactionViewModel>?)null)
	{
		Reaction = P_0;
		Message = P_1;
		_bitmapCache = P_3;
		_localDataStore = P_4;
		_globalUserCacheService = P_5;
		_emoji = P_2.GetEmojiByShortcode(P_0.ShortCode);
		Emoji emoji = _emoji;
		if (emoji != null && emoji.CodePoints != null)
		{
			_emojiUri = "root://emoji/" + _emoji.Shortname + ".png";
		}
	}

	[RelayCommand]
	public async Task ReactionSelectedAsync()
	{
		try
		{
			if (!Message.MessageContainer.LocalChannelPermission.ChannelCreateMessageReaction)
			{
				return;
			}
			if (Reaction.ContainsSelf)
			{
				await Reaction.MessageContainer.Messages.DeleteReactionAsync(Reaction.MessageId, Reaction.ShortCode);
				return;
			}
			await Reaction.MessageContainer.Messages.CreateReactionAsync(Reaction.MessageId, Reaction.ShortCode);
			if (!_localDataStore.TryGetGlobal(DataStoreKeys.FrequentlyUsedEmojis, out var frequentlyUsedEmojisState, FrequentlyUsedEmojisStateSerializerContext.Default.FrequentlyUsedEmojisState))
			{
				frequentlyUsedEmojisState = new FrequentlyUsedEmojisState();
			}
			frequentlyUsedEmojisState.AddEmoji(Reaction.ShortCode);
			_localDataStore.SetGlobal(DataStoreKeys.FrequentlyUsedEmojis, frequentlyUsedEmojisState, FrequentlyUsedEmojisStateSerializerContext.Default.FrequentlyUsedEmojisState);
		}
		catch
		{
		}
	}

	[RelayCommand]
	public async Task LoadReactionDataAsync()
	{
		int totalCount = Reaction.Users.Count;
		int renderAmountCount = ((totalCount > 50) ? 50 : totalCount);
		int remainingCount = totalCount - renderAmountCount;
		List<UserGuid> userIdsToRender = Reaction.Users.ToList().GetRange(0, renderAmountCount);
		CombinedUsername = string.Join(", ", (await _globalUserCacheService.GetUsersByIdsAsync(userIdsToRender)).Select((GlobalUser u) => u.UserName));
		if (remainingCount > 0)
		{
			ExtraCount = $"and {remainingCount} others";
		}
		else
		{
			ExtraCount = string.Empty;
		}
	}
}
