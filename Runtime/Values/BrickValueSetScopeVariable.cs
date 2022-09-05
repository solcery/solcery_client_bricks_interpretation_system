using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Values
{
    public sealed class BrickValueSetScopeVariable : BrickValue
    {
        public static BrickValue Create(int type, int subType)
        {
            return new BrickValueSetScopeVariable(type, subType);
        }
        
        public BrickValueSetScopeVariable(int type, int subType) : base(type, subType) { }

        public override int Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 2
                && parameters[0].TryParseBrickParameter(out _, out string varName)
                && parameters[1].TryParseBrickParameter(out _, out JObject valueBrick)
                && serviceBricks.ExecuteValueBrick(valueBrick, context, level + 1, out var value)
                && context.LocalScopes.TryPeek(out var localScope))
            {
                localScope.Vars.Update(varName, value);
                return value;
            }
            
            throw new Exception($"BrickActionSetVariable Run parameters {parameters}!");
        }

        public override void Reset() { }
    }
}