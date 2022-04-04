using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Values
{
    public sealed class BrickValueIteratorMax : BrickValue
    {
        public static BrickValue Create(int type, int subType)
        {
            return new BrickValueIteratorMax(type, subType);
        }
        
        private BrickValueIteratorMax(int type, int subType) : base(type, subType) { }
        
        public override void Reset() { }

        public override int Run(IServiceBricksInternal serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 3
                && parameters[0].TryParseBrickParameter(out _, out JObject conditionBrickIteration)
                && parameters[1].TryParseBrickParameter(out _, out JObject valueBrickTarget)
                && parameters[2].TryParseBrickParameter(out _, out JObject valueBrickFallback)
                && serviceBricks.ExecuteValueBrick(valueBrickFallback, context, level + 1, out var fallback))
            {
                var selectedObjects = context.GameObjects.GetAllCardTypeObject();
                var resultObjects = new List<object>();
                var oldObjectExist = context.Object.TryPop(out object oldObject);

                while (!selectedObjects.IsEmpty())
                {
                    var selectedObject = selectedObjects.Pop();
                    context.Object.Push(selectedObject);
                    if (serviceBricks.ExecuteConditionBrick(conditionBrickIteration, context, level + 1, out var conditionResult) 
                        && conditionResult)
                    {
                        resultObjects.Add(selectedObject);
                    }

                    context.Object.TryPop<object>(out _);
                }

                var maxResult = int.MinValue;
                var foundResult = false;
                foreach (var resultObject in resultObjects)
                {
                    context.Object.Push(resultObject);
                    if (!serviceBricks.ExecuteValueBrick(valueBrickTarget, context, level + 1, out var res))
                    {
                        throw new Exception($"BrickValueIteratorMax Run parameters {parameters}!");
                    }
                    context.Object.TryPop<object>(out _);
                    if (maxResult < res)
                    {
                        maxResult = res;
                        foundResult = true;
                    }
                }

                if (oldObjectExist)
                {
                    context.Object.Push(oldObject);
                }

                return foundResult ? maxResult : fallback;
            }
            
            throw new Exception($"BrickValueIteratorMax Run parameters {parameters}!");
        }
    }
}