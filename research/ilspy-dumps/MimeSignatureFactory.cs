using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using MimeDetective.Definitions;
using MimeDetective.Definitions.Licensing;
using MimeDetective.Storage;

namespace RootApp.Utility.MimeDetection;

public static class MimeSignatureFactory
{
	private static readonly ImmutableArray<Definition> _defaultDefinitions;

	public static IList<Definition> Default => _defaultDefinitions;

	static MimeSignatureFactory()
	{
		ImmutableArray<Definition> immutableArray = new ExhaustiveBuilder
		{
			UsageType = UsageType.CommercialPaid
		}.Build();
		_defaultDefinitions = ImmutableCollectionsMarshal.AsImmutableArray(MimeSignatureFilter.Filter(immutableArray).ToArray());
	}
}
