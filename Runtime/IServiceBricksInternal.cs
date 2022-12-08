using System;
using Newtonsoft.Json.Linq;
using Solcery.BrickInterpretation.Runtime.Contexts;

namespace Solcery.BrickInterpretation.Runtime
{
    public interface IServiceBricksInternal
    {
        bool ExecuteActionBrick(JObject brickObject, IContext context, int level);
        bool ExecuteValueBrick(JObject brickObject, IContext context, int level, out int result);
        bool ExecuteConditionBrick(JObject brickObject, IContext context, int level, out bool result);
        bool ExecuteJKeyValueBrick(JObject brickObject, IContext context, int level, out Tuple<string, JToken> result);
        bool ExecuteJTokenBrick(JObject brickObject, IContext context, int level, out JToken result);
    }
}