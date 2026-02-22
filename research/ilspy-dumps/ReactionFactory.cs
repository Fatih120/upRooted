using RootApp.Core.Identifiers;

namespace RootApp.Client.CoreDomain.Models.Messages;

public class ReactionFactory(IRootSessionAccessor rootSessionAccessor)
{
	public Reaction Create(MessageGuid P_0, string P_1, IMessageContainer P_2)
	{
		return new Reaction(P_0, P_1, P_2, rootSessionAccessor);
	}
}
