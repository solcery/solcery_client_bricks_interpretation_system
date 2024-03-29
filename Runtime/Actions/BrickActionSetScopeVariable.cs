﻿using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public sealed class BrickActionSetScopeVariable : BrickAction
    {
        public static BrickAction Create(int type, int subType)
        {
            return new BrickActionSetScopeVariable(type, subType);
        }
        
        private BrickActionSetScopeVariable(int type, int subType) : base(type, subType) { }

        public override void Reset() { }

        public override void Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 2
                && parameters[0].TryParseBrickParameter(out _, out string varName)
                && parameters[1].TryParseBrickParameter(out _, out JObject valueBrick)
                && serviceBricks.ExecuteValueBrick(valueBrick, context, level + 1, out var value))
            {
                var localScope = context.LocalScopes.Pop();
                localScope.Vars.Update(varName, value);
                context.LocalScopes.Push(localScope);
                return;
            }
            
            throw new Exception($"BrickActionSetScopeVariable Run parameters {parameters}!");
        }
    }
}