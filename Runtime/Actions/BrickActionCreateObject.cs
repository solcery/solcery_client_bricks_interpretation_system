using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionCreateObject : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionCreateObject(type, subType);
        }
        
        private BrickActionCreateObject(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 3
                && parameters[0].TryParseBrickParameter(out _, out JObject valueBrickCardType)
                && parameters[1].TryParseBrickParameter(out _, out JObject valueBrickPlace)
                && parameters[2].TryParseBrickParameter(out _, out JObject actionBrickOnCreate)
                && serviceBricks.ExecuteValueBrick(valueBrickCardType, context, level + 1, out var cardType)
                && serviceBricks.ExecuteValueBrick(valueBrickPlace, context, level + 1, out var place)
                && context.TryCreateObject(new JObject
                    {
                        {"card_type", new JValue(cardType)},
                        {"place", new JValue(place)}
                    }
                    , out var @object))
            {
                context.Object.Push(@object);
                if (serviceBricks.ExecuteActionBrick(actionBrickOnCreate, context, level + 1))
                {
                    context.Object.TryPop(out object _);
                    return;
                }
            }
            
            throw new Exception($"BrickActionCreateObject Run parameters {parameters}!");
        }
    }
}