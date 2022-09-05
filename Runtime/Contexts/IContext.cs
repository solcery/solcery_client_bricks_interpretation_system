using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts.Args;
using Solcery.BrickInterpretation.Runtime.Contexts.Attrs;
using Solcery.BrickInterpretation.Runtime.Contexts.GameStates;
using Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes;
using Solcery.BrickInterpretation.Runtime.Contexts.Objects;
using Solcery.BrickInterpretation.Runtime.Contexts.Utils;
using Solcery.BrickInterpretation.Runtime.Contexts.Vars;

namespace Solcery.BrickInterpretation.Runtime.Contexts
{
    public interface IContext
    {
        IContextGameStates GameStates { get; }
        IContextObject Object { get; }
        IContextObjectAttrs ObjectAttrs { get; }
        IContextGameAttrs GameAttrs { get; }
        IContextGameArgs GameArgs { get; }
        IContextGameVars GameVars { get; }
        IContextGameObjects GameObjects { get; }
        IContextLocalScopes LocalScopes { get; }
        ILog Log { get; }

        bool DeleteObject(object @object);
        bool TryCreateObject(JObject parameters, out object @object);
        bool TryResetObjectAttrs(object @object);
    }
}