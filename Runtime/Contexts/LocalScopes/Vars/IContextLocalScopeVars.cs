using System.Collections.Generic;

namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Vars
{
    public interface IContextLocalScopeVars
    {
        IReadOnlyList<string> AllVarName { get; }

        void Update(string name, int value);
        bool TryGet(string name, out int value);
    }
}