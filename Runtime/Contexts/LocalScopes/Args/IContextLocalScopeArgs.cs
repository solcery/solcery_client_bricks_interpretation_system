namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Args
{
    public interface IContextLocalScopeArgs
    {
        bool Contains(string name);
        void Update(string name, int value);
        bool TryGetValue(string name, out int value);
    }
}