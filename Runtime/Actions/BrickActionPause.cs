using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionPause : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionPause(type, subType);
        }
        
        private BrickActionPause(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricks serviceBricks, JArray parameters, IContext context, int level)
        {
            throw new System.NotImplementedException();
        }
    }
}