using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Conditions
{
    public sealed class BrickConditionLesserThan : BrickCondition
    {
        public static BrickCondition Create(int type, int subType)
        {
            return new BrickConditionLesserThan(type, subType);
        }

        private BrickConditionLesserThan(int type, int subType) : base(type, subType) { }
        
        public override bool Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 2
                && parameters[0].TryParseBrickParameter(out _, out JObject valueBrick1)
                && parameters[1].TryParseBrickParameter(out _, out JObject valueBrick2)
                && serviceBricks.ExecuteValueBrick(valueBrick1, context, level + 1, out var value1)
                && serviceBricks.ExecuteValueBrick(valueBrick2, context, level + 1, out var value2))
            {
                return value1 < value2;
            }

            throw new Exception($"BrickConditionLesserThan Run parameters {parameters}!");
        }

        public override void Reset() { }
    }
}