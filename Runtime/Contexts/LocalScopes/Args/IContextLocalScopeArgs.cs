using Newtonsoft.Json.Linq;

namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Args
{
    public interface IContextLocalScopeArgs
    {
        bool Contains(string name);
        void Update(string name, JObject value);
        bool TryGetValue(string name, out JObject value);
    }
}