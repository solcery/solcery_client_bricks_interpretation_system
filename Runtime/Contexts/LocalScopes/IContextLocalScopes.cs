namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes
{
    public interface IContextLocalScopes
    {
        void Push();
        bool TryPeek(out IContextLocalScope localScope);
        IContextLocalScope Pop();
    }
}