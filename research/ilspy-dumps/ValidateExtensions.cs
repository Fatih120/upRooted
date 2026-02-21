using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace RootApp.Core.Validate;

public static class ValidateExtensions
{
	public static readonly Extension<FieldOptions, FieldRules> Rules = new Extension<FieldOptions, FieldRules>(1159, FieldCodec.ForMessage(9274u, FieldRules.Parser));
}
