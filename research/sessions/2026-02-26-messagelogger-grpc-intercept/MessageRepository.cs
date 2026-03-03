using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RootApp.WebApi.Client;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;
using RootApp.WebApi.Shared.Grpc.Services;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.Client.CoreDomain.Repositories;

public class MessageRepository : IMessageRepository
{
	private readonly ILogger<MessageRepository> _logger = P_1.CreateLogger<MessageRepository>();

	private readonly IRootDefaultApiConnection _rootConnection;

	public MessageRepository(IRootDefaultApiConnection P_0, ILoggerFactory P_1)
	{
		_rootConnection = P_0;
		base..ctor();
	}

	public async ValueTask<MessageContainerResponse> GetMessagesAsync(MessageListRequest P_0)
	{
		MessageListResponse response = await _rootConnection.Connection.Message.ListAsync(P_0);
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			_logger.LogTrace("Message get: {Response}", response.ToString());
		}
		return response.Message;
	}

	public async ValueTask<MessagePacket> CreateMessageAsync(MessageCreateRequest P_0)
	{
		MessageGrpcService.MessageGrpcServiceClient serviceClient = _rootConnection.Connection.Message;
		MessageCreateResponse createResponse = await serviceClient.CreateAsync(P_0);
		MessageGetResponse getResponse = await serviceClient.GetAsync(new MessageGetRequest
		{
			CommunityId = P_0.CommunityId,
			ContainerId = P_0.ContainerId,
			Id = createResponse.Id
		});
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			_logger.LogTrace("Message created: {Response}", getResponse.ToString());
		}
		return getResponse.Message;
	}

	public async ValueTask<MessagePacket> EditMessageAsync(MessageEditRequest P_0)
	{
		MessageEditResponse response = await _rootConnection.Connection.Message.EditAsync(P_0);
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			_logger.LogTrace("Message edited: {Response}", response.ToString());
		}
		return response.Message;
	}

	public async ValueTask<MessageReactionCreateResponse> CreateReactionAsync(MessageReactionCreateRequest P_0)
	{
		return await _rootConnection.Connection.Message.ReactionCreateAsync(P_0);
	}

	public async ValueTask DeleteReactionAsync(MessageReactionDeleteRequest P_0)
	{
		await _rootConnection.Connection.Message.ReactionDeleteAsync(P_0);
	}

	public async ValueTask SetViewTimeAsync(MessageSetViewTimeRequest P_0)
	{
		await _rootConnection.Connection.Message.SetViewTimeAsync(P_0);
	}

	public async ValueTask SetTypingIndicatorAsync(MessageSetTypingIndicatorRequest P_0)
	{
		await _rootConnection.Connection.Message.SetTypingIndicatorAsync(P_0);
	}

	public async ValueTask<MessageContainerResponse> GetPinnedMessagesAsync(MessagePinListRequest P_0)
	{
		return (await _rootConnection.Connection.Message.PinListAsync(P_0)).Message;
	}

	public async ValueTask PinMessageAsync(MessagePinCreateRequest P_0)
	{
		await _rootConnection.Connection.Message.PinCreateAsync(P_0);
	}

	public async ValueTask UnpinMessageAsync(MessagePinDeleteRequest P_0)
	{
		await _rootConnection.Connection.Message.PinDeleteAsync(P_0);
	}

	public async ValueTask DeleteMessageAsync(MessageDeleteRequest P_0)
	{
		await _rootConnection.Connection.Message.DeleteAsync(P_0);
	}

	public async ValueTask<MessageContainerResponse> SearchAsync(MessageSearchRequest P_0, CancellationToken P_1)
	{
		return (await _rootConnection.Connection.Message.SearchAsync(P_0, null, null, P_1)).Message;
	}

	public async ValueTask<MessageSearchContainersResponse> SearchContainerAsync(MessageSearchContainersRequest P_0, CancellationToken P_1)
	{
		return await _rootConnection.Connection.Message.SearchContainerAsync(P_0, null, null, P_1);
	}
}
