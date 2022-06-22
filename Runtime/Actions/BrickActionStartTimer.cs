using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionStartTimer : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionStartTimer(type, subType);
        }
        
        private BrickActionStartTimer(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count == 1
                && parameters[0].TryParseBrickParameter(out _, out JObject valueBrickDuration)
                && serviceBricks.ExecuteValueBrick(valueBrickDuration, context, level + 1, out var durationMsec)
                && context.Object.TryPeek<object>(out var targetObject)
                && context.GameObjects.TryGetCardId(targetObject, out var targetObjectId))
            {
                
                context.GameStates.PushStartTimer(durationMsec, targetObjectId);
                return;
            }
            
            throw new Exception($"BrickActionStartTimer Run parameters {parameters}!");
        }
    }
}