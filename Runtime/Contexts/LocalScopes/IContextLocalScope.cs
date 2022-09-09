using Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Atrs;
using Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Vars;

namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes
{
    public interface IContextLocalScope
    {
        IContextLocalScopeVars Vars { get; }
        IContextLocalScopeAttrs Attrs { get; }
    }
}