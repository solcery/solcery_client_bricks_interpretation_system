using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionPause : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionPause(type, subType);
        }
        
        private BrickActionPause(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if(parameters.Count > 0 
               && parameters[0].TryParseBrickParameter(out _, out JObject valueBrick) 
               && serviceBricks.ExecuteValueBrick(valueBrick, context, level + 1, out var delayMsec))
            {
                context.GameStates.PushGameState();
                context.GameStates.PushDelay(delayMsec);
                return;
            }
            
            throw new Exception($"BrickActionPause Run parameters {parameters}!");
        }
    }
}