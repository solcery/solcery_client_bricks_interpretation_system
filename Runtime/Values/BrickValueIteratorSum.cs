using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Values
{
    public sealed class BrickValueIteratorSum : BrickValue
    {
        public static BrickValue Create(int type, int subType)
        {
            return new BrickValueIteratorSum(type, subType);
        }
        
        private BrickValueIteratorSum(int type, int subType) : base(type, subType) { }
        
        public override void Reset() { }

        public override int Run(IServiceBricks serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 3
                && parameters[0].TryParseBrickParameter(out _, out JObject conditionBrickIteration)
                && parameters[1].TryParseBrickParameter(out _, out JObject valueBrickTarget)
                && parameters[2].TryParseBrickParameter(out _, out JObject valueBrickLimit)
                && serviceBricks.ExecuteValueBrick(valueBrickLimit, context, level + 1, out var limit))
            {
                var selectedObjects = context.GameObjects.GetAllCardTypeObject();
                selectedObjects.Shuffle();
                var resultObjects = new List<object>();
                var oldObjectExist = context.Object.TryPop(out object oldObject);

                limit = limit < selectedObjects.Count ? limit : selectedObjects.Count;
                while (limit > 0 && !selectedObjects.IsEmpty())
                {
                    var selectedObject = selectedObjects.Pop();
                    context.Object.Push(selectedObject);
                    if (serviceBricks.ExecuteConditionBrick(conditionBrickIteration, context, level + 1, out var conditionResult) 
                        && conditionResult)
                    {
                        resultObjects.Add(selectedObject);
                        --limit;
                    }

                    context.Object.TryPop<object>(out _);
                }

                var result = 0;

                foreach (var resultObject in resultObjects)
                {
                    context.Object.Push(resultObject);
                    if (!serviceBricks.ExecuteValueBrick(valueBrickTarget, context, level + 1, out var res))
                    {
                        throw new Exception($"BrickValueIteratorSum Run parameters {parameters}!");
                    }
                    context.Object.TryPop<object>(out _);

                    result += res;
                }

                if (oldObjectExist)
                {
                    context.Object.Push(oldObject);
                }

                return result;
            }
            
            throw new Exception($"BrickValueIteratorSum Run parameters {parameters}!");
        }
    }
}