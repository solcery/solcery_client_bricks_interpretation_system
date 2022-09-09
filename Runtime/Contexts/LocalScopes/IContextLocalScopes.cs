namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes
{
    public interface IContextLocalScopes
    {
        IContextLocalScope Push();
        bool TryPeek(out IContextLocalScope localScope);
        void Pop();
    }
}