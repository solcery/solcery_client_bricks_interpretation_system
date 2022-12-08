using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionPushActionJToken : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionPushActionJToken(type, subType);
        }
        
        private BrickActionPushActionJToken(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if(parameters.Count > 1 
               && parameters[0].TryParseBrickParameter(out _, out int actionType)
               && parameters[1].TryParseBrickParameter(out _, out JObject jTokenBrick)
               && serviceBricks.ExecuteJTokenBrick(jTokenBrick, context, level + 1, out var value))
            {
                context.GameStates.PushActionJToken(actionType, value);
                return;
            }
            
            throw new Exception($"BrickActionPlaySound Run parameters {parameters}!");
        }
    }
}