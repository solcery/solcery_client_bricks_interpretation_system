using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Solcery.BrickInterpretation.Runtime.Contexts.Objects
{
    public interface IContextGameObjects
    {
        List<object> GetAllCardTypeObject();
        bool TryGetCardTypeValue(object @object, string key, out JToken value);
        bool TryGetCardId(object @object, out int cardId);
        bool TryGetCardTypeId(object @object, out int cardTypeId);
        bool SetCardTypeId(object @object, int cardTypeId);
    }
}