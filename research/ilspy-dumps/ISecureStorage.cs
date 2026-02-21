using System.Threading.Tasks;

namespace RootApp.Client.Domain.SecureStorage;

public interface ISecureStorage
{
	ValueTask SetAsync(string P_0, string? P_1);

	ValueTask<string?> GetAsync(string P_0);

	void Delete(string P_0);
}
