using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Jsons.JKeyValues
{
    public sealed class BrickJKeyValueArgument : BrickJKeyValue
    {
        public static BrickJKeyValue Create(int type, int subType)
        {
            return new BrickJKeyValueArgument(type, subType);
        }
        
        private BrickJKeyValueArgument(int type, int subType) : base(type, subType) { }

        public override Tuple<string, JToken> Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0 
                && parameters[0].TryParseBrickParameter(out _, out string argName))
            {
                var localScope = context.LocalScopes.Pop();
                if (localScope.Args.TryGetValue(argName, out var brickObject))
                {
                    if (serviceBricks.ExecuteJKeyValueBrick(brickObject, context, level + 1, out var result))
                    {
                        context.LocalScopes.Push(localScope);
                        return result;
                    }
                }
            }

            throw new ArgumentException($"BrickJKeyValueArgument Run has exception! Parameters {parameters}");
        }

        public override void Reset() { }
    }
}