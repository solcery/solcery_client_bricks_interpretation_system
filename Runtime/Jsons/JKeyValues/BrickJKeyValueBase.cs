using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Jsons.JKeyValues
{
    public sealed class BrickJKeyValueBase : BrickJKeyValue
    {
        public static BrickJKeyValue Create(int type, int subType)
        {
            return new BrickJKeyValueBase(type, subType);
        }
        
        private BrickJKeyValueBase(int type, int subType) : base(type, subType) { }

        public override Tuple<string, JToken> Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0
                && parameters[0].TryParseBrickParameter(out _, out string key)
                && parameters[1].TryParseBrickParameter(out _, out JObject jTokenBrick)
                && serviceBricks.ExecuteJTokenBrick(jTokenBrick, context, level + 1, out var value))
            {
                return new Tuple<string, JToken>(key, value);
            }
            
            throw new ArgumentException($"BrickJKeyValueBase Run has exception! Parameters {parameters}");
        }

        public override void Reset() { }
    }
}