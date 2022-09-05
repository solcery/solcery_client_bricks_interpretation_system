using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Values
{
    public sealed class BrickValueGetScopeVariable : BrickValue
    {
        public static BrickValue Create(int type, int subType)
        {
            return new BrickValueGetScopeVariable(type, subType);
        }
        
        public BrickValueGetScopeVariable(int type, int subType) : base(type, subType) { }

        public override int Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count > 0 
                && parameters[0].TryParseBrickParameter(out _, out string varName)
                && context.LocalScopes.TryPeek(out var localScope)
                && localScope.Vars.TryGet(varName, out var value))
            {
                return value;
            }

            throw new ArgumentException($"BrickValueVariable Run has exception! Parameters {parameters}");
        }

        public override void Reset() { }
    }
}