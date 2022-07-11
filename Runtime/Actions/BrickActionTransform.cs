using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionTransform : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionTransform(type, subType);
        }
        
        private BrickActionTransform(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count == 1
                && parameters[0].TryParseBrickParameter(out _, out int tplId)
                && context.Object.TryPeek<object>(out var @object)
                && context.GameObjects.SetCardTypeId(@object, tplId))
            {
                return;
            }
            
            throw new Exception($"BrickActionTransform Run parameters {parameters}!");
        }
    }
}