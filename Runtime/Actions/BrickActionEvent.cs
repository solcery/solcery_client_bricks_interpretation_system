using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionEvent : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionEvent(type, subType);
        }
        
        private BrickActionEvent(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0
                && parameters[0].TryParseBrickParameter(out _, out string eventName)
                && context.Object.TryPeek(out object @object)
                && context.GameObjects.TryGetCardTypeData(@object, out var objectData))
            {
                if (objectData.TryGetValue($"action_on_{eventName}", out JObject actionBrick))
                {
                    if (serviceBricks.ExecuteActionBrick(actionBrick, context, level + 1))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            
            throw new Exception($"BrickActionEvent Run parameters {parameters}!");
        }
    }
}