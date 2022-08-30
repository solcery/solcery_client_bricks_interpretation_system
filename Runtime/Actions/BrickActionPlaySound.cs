using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public class BrickActionPlaySound : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionPlaySound(type, subType);
        }
        
        private BrickActionPlaySound(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if(parameters.Count > 0 
               && parameters[0].TryParseBrickParameter(out _, out int soundId))
            {
                context.GameStates.PushPlaySound(soundId);
                return;
            }
            
            throw new Exception($"BrickActionPlaySound Run parameters {parameters}!");
        }
    }
}