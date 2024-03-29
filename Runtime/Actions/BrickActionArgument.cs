using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionArgument : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionArgument(type, subType);
        }
        
        public BrickActionArgument(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0 
                && parameters[0].TryParseBrickParameter(out _, out string argName))
            {
                //var args = context.GameArgs.Pop();
                var localScope = context.LocalScopes.Pop();
                if (localScope.Args.TryGetValue(argName, out var brickObject))
                {
                    if (serviceBricks.ExecuteActionBrick(brickObject, context, level + 1))
                    {
                        context.LocalScopes.Push(localScope);
                        return;
                    }
                }
            }

            throw new ArgumentException($"BrickConditionArgument Run has exception! Parameters {parameters}");
        }
    }
}