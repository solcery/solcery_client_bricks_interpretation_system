using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;
using Solcery.BrickInterpretation.Runtime.Utils;

namespace Solcery.BrickInterpretation.Runtime.Conditions
{
    public sealed class BrickConditionIteratorOr : BrickCondition
    {
        public static BrickCondition Create(int type, int subType)
        {
            return new BrickConditionIteratorOr(type, subType);
        }
        
        private BrickConditionIteratorOr(int type, int subType) : base(type, subType) { }
        
        public override void Reset() { }

        public override bool Run(IServiceBricks serviceBricks, JArray parameters, IContext context, int level)
        {
            if (parameters.Count >= 3
                && parameters[0].TryParseBrickParameter(out _, out JObject conditionBrickIteration)
                && parameters[1].TryParseBrickParameter(out _, out JObject conditionBrickTarget)
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

                var result = false;

                foreach (var resultObject in resultObjects)
                {
                    context.Object.Push(resultObject);
                    if (!serviceBricks.ExecuteConditionBrick(conditionBrickTarget, context, level + 1, out result))
                    {
                        throw new Exception($"BrickConditionIteratorOr Run parameters {parameters}!");
                    }
                    context.Object.TryPop<object>(out _);

                    if (result)
                    {
                        break;
                    }
                }

                if (oldObjectExist)
                {
                    context.Object.Push(oldObject);
                }

                return result;
            }
            
            throw new Exception($"BrickConditionIteratorOr Run parameters {parameters}!");
        }
    }
}