using Newtonsoft.Json.Linq;

namespace Solcery.BrickInterpretation.Runtime.Jsons
{
    public abstract class BrickJToken : Brick<JToken>
    {
        protected BrickJToken(int type, int subType) : base(type, subType) { }
    }
}