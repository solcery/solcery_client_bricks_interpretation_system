using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Values
{
    public sealed class BrickValueSetVariable : BrickValue
    {
        public static BrickValue Create(int type, int subType)
        {
            return new BrickValueSetVariable(type, subType);
        }
        
        private BrickValueSetVariable(int type, int subType) : base(type, subType) { }
        
        public override void Reset() { }

        public override int Run(IServiceBricks serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 2
                && parameters[0].TryParseBrickParameter(out _, out string varName)
                && parameters[1].TryParseBrickParameter(out _, out JObject valueBrick)
                && serviceBricks.ExecuteValueBrick(valueBrick, context, level + 1, out var value))
            {
                context.GameVars.Update(varName, value);
                return value;
            }
            
            throw new Exception($"BrickActionSetVariable Run parameters {parameters}!");
        }
    }
}