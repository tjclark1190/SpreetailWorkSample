using System.Collections.Generic;

namespace SpreetailWorkSample.Services.Interfaces
{
    public interface IKeyValuePairService<TKey, TMember>
    {
        string KeyValuePairCommandOutput(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePairToProcess, string command);
    }
}