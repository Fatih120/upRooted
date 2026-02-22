using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Core.Identifiers;

namespace RootApp.Client.CoreDomain.Models.Messages;

public class Reaction : ObservableObject
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private string <ShortCode>k__BackingField;

	[CompilerGenerated]
	private int <Count>k__BackingField;

	[CompilerGenerated]
	private bool <ContainsSelf>k__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int Count
	{
		get
		{
			return <Count>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(<Count>k__BackingField, num))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Count);
				<Count>k__BackingField = num;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Count);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ContainsSelf
	{
		get
		{
			return <ContainsSelf>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ContainsSelf>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ContainsSelf);
				<ContainsSelf>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ContainsSelf);
			}
		}
	}

	public MessageGuid MessageId { get; }

	public string ShortCode
	{
		[CompilerGenerated]
		get
		{
			return <ShortCode>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<ShortCode>k__BackingField = text;
		}
	}

	public IMessageContainer MessageContainer { get; }

	public bool IsPlaceHolder { get; }

	public HashSet<UserGuid> Users { get; } = new HashSet<UserGuid>();

	public Reaction(MessageGuid P_0, string P_1, IMessageContainer P_2, IRootSessionAccessor P_3)
	{
		MessageId = P_0;
		ShortCode = P_1;
		MessageContainer = P_2;
		_rootSessionAccessor = P_3;
		IsPlaceHolder = P_0 == MessageGuid.Empty;
	}

	public void Increment(UserGuid P_0)
	{
		if ((!ContainsSelf || !(P_0 == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id)) && Users.Add(P_0))
		{
			if (_rootSessionAccessor.Session.UserInfoService.SessionUser.Id == P_0)
			{
				ContainsSelf = true;
			}
			Count++;
		}
	}

	public void Decrement(UserGuid P_0)
	{
		if ((ContainsSelf || !(P_0 == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id)) && Users.Remove(P_0))
		{
			if (_rootSessionAccessor.Session.UserInfoService.SessionUser.Id == P_0)
			{
				ContainsSelf = false;
			}
			Count--;
		}
	}
}
