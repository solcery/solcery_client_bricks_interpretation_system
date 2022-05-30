using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionClearAttrs : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionClearAttrs(type, subType);
        }
        
        private BrickActionClearAttrs(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (context.Object.TryPeek<object>(out var @object)
                && context.TryResetObjectAttrs(@object))
            {
                return;
            }
            
            throw new Exception($"BrickActionClearAttrs Run parameters {parameters}!");
        }
    }
}