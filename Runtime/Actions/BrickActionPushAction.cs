using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionPushAction : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionPushAction(type, subType);
        }
        
        private BrickActionPushAction(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if(parameters.Count > 0 
              && parameters[0].TryParseBrickParameter(out _, out int actionType))
            {
                var localScope = context.LocalScopes.Pop();
                var valueKeys = localScope.Vars.AllVarName;
                var values = new Dictionary<string, int>(valueKeys.Count);
                foreach (var valueKey in valueKeys)
                {
                    if (!values.ContainsKey(valueKey) 
                        && localScope.Vars.TryGet(valueKey, out var value))
                    {
                        values.Add(valueKey, value);
                    }
                }
                context.LocalScopes.Push(localScope);
                context.GameStates.PushAction(actionType, values);
                values.Clear();
                return;
            }
            
            throw new Exception($"BrickActionPlaySound Run parameters {parameters}!");
        }
    }
}