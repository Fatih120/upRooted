using System.Collections.Generic;
using System.Threading.Tasks;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.Client.CoreDomain.Services;

public interface IGlobalUserCacheService
{
	void AddSessionUser(SessionUser P_0);

	void CreateShellUsers(IReadOnlyCollection<UserGuid> P_0);

	ValueTask<GlobalUser?> GetUserByIdAsync(UserGuid P_0, bool P_1 = false);

	GlobalUser? GetUserById(UserGuid P_0);

	ValueTask<IEnumerable<GlobalUser>> GetUsersByIdsAsync(IEnumerable<UserGuid> P_0, bool P_1 = false);

	IEnumerable<GlobalUser> GetUsersByIds(IEnumerable<UserGuid> P_0);

	void HandlePacket(IPacket P_0);

	Task InitializeAsync();

	void SetUserBlockService(IUserBlockService P_0);
}
