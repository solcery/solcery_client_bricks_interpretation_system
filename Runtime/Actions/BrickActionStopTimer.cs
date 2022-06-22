using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionStopTimer : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionStopTimer(type, subType);
        }
        
        private BrickActionStopTimer(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            context.GameStates.PushStopTimer();
        }
    }
}