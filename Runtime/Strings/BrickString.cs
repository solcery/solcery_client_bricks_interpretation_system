namespace Solcery.BrickInterpretation.Runtime.Strings
{
    public abstract class BrickString : Brick<string>
    {
        protected BrickString(int type, int subType) : base(type, subType) { }
    }
}