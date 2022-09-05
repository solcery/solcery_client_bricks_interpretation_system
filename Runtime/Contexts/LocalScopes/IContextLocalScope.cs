using Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Vars;

namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes
{
    public interface IContextLocalScope
    {
        IContextLocalScopeVars Vars { get; }
    }
}