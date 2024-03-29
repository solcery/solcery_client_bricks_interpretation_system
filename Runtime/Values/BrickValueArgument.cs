using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Values
{
    public sealed class BrickValueArgument : BrickValue
    {
        public static BrickValue Create(int type, int subType)
        {
            return new BrickValueArgument(type, subType);
        }
        
        private BrickValueArgument(int type, int subType) : base(type, subType) { }

        public override int Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0 
                && parameters[0].TryParseBrickParameter(out _, out string argName))
            {
                //var args = context.GameArgs.Pop();
                var localScope = context.LocalScopes.Pop();
                if (localScope.Args.TryGetValue(argName, out var brickObject))
                {
                    if (serviceBricks.ExecuteValueBrick(brickObject, context, level + 1, out var result))
                    {
                        context.LocalScopes.Push(localScope);
                        return result;
                    }
                }
            }

            throw new ArgumentException($"BrickValueArgument Run has exception! Parameters {parameters}");
        }

        public override void Reset() { }
    }
}