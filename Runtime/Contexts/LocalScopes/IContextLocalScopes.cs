namespace Solcery.BrickInterpretation.Runtime.Contexts.LocalScopes
{
    public interface IContextLocalScopes
    {
        IContextLocalScope New();
        void Push(IContextLocalScope localScope);
        IContextLocalScope Pop();
    }
}