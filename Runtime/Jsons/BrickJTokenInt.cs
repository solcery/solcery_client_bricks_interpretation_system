using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Jsons
{
    public sealed class BrickJTokenInt : BrickJToken
    {
        public static BrickJToken Create(int type, int subType)
        {
            return new BrickJTokenInt(type, subType);
        }
        
        private BrickJTokenInt(int type, int subType) : base(type, subType) { }

        public override JToken Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0
                && parameters[0].TryParseBrickParameter(out _, out JObject valueBrick)
                && serviceBricks.ExecuteValueBrick(valueBrick, context, level + 1, out var value))
            {
                return new JValue(value);
            }
            
            throw new ArgumentException($"BrickJTokenInt Run has exception! Parameters {parameters}");
        }

        public override void Reset() { }
    }
}