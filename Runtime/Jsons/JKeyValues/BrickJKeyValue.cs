using System;
using Newtonsoft.Json.Linq;

namespace Solcery.BrickInterpretation.Runtime.Jsons.JKeyValues
{
    public abstract class BrickJKeyValue : Brick<Tuple<string, JToken>>
    {
        protected BrickJKeyValue(int type, int subType) : base(type, subType) { }
    }
}