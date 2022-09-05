namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Vars
{
    public interface IContextLocalScopeVars
    {
        void Update(string name, int value);
        bool TryGet(string name, out int value);
    }
}