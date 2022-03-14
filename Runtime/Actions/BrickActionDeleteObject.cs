using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionDeleteObject : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionDeleteObject(type, subType);
        }
        
        private BrickActionDeleteObject(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (context.Object.TryPop(out object @object) 
                && context.DeleteObject(@object))
            {
                return;
            }
            
            throw new Exception($"BrickActionDeleteEntity Run parameters {parameters}!");
        }
    }
}