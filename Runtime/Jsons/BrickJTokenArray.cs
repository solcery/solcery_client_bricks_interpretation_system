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
            if (parameters.Count > 0)
            {
                var result = new JArray();

                foreach (var parameter in parameters)
                {
                    if (parameter.TryParseBrickParameter(out _, out JObject jTokenBrick)
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