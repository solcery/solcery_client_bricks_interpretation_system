using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Jsons
{
    public sealed class BrickJTokenArray : BrickJToken
    {
        public static BrickJToken Create(int type, int subType)
        {
            return new BrickJTokenArray(type, subType);
        }
        
        private BrickJTokenArray(int type, int subType) : base(type, subType) { }

        public override JToken Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0
                && parameters[0].TryParseBrickParameter(out _, out JArray jTokenBricks))
            {
                var result = new JArray();

                foreach (var jTokenBrickToken in jTokenBricks)
                {
                    if (jTokenBrickToken is JObject jTokenBrick
                        && serviceBricks.ExecuteJTokenBrick(jTokenBrick, context, level + 1, out var value))
                    {
                        result.Add(value);
                    }
                    else
                    {
                        throw new ArgumentException($"BrickJTokenArray Run has exception! Parameters {parameters}");
                    }
                }
                
                return result;
            }
            
            throw new ArgumentException($"BrickJTokenArray Run has exception! Parameters {parameters}");
        }

        public override void Reset() { }
    }
}