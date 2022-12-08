using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Jsons
{
    public sealed class BrickJTokenObject : BrickJToken
    {
        public static BrickJToken Create(int type, int subType)
        {
            return new BrickJTokenObject(type, subType);
        }
        
        public BrickJTokenObject(int type, int subType) : base(type, subType) { }

        public override JToken Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0
                && parameters[0].TryParseBrickParameter(out _, out JArray jKeyValueBricks))
            {
                var result = new JObject();

                foreach (var jKeyValueBrickToken in jKeyValueBricks)
                {
                    if (jKeyValueBrickToken is JObject jKeyValueBrick
                        && serviceBricks.ExecuteJKeyValueBrick(jKeyValueBrick, context, level + 1, out var value))
                    {
                        result.Add(value.Item1, value.Item2);
                    }
                    else
                    {
                        throw new ArgumentException($"BrickJTokenObject Run has exception! Parameters {parameters}");
                    }
                }
                
                return result;
            }
            
            throw new ArgumentException($"BrickJTokenObject Run has exception! Parameters {parameters}");
        }

        public override void Reset() { }
    }
}