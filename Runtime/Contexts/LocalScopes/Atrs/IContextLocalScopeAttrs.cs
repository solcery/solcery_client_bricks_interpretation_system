namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes.Atrs
{
    public interface IContextLocalScopeAttrs
    {
        bool Contains(string name);
        void Update(string name, int value);
        bool TryGetValue(string name, out int value);
    }
}