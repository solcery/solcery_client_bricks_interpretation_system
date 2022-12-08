using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Solcery.BrickInterpretation.Runtime.Contexts.GameStates
{
    public interface IContextGameStates
    {
        bool IsEmpty { get; }
        
        void PushGameState();
        void PushDelay(int msec);
        void PushStartTimer(int durationMsec, int targetObjectId);
        void PushStopTimer();
        void PushPlaySound(int soundId, int volume);
        void PushAction(int actionType, IReadOnlyDictionary<string, int> value);
        void PushActionJToken(int actionType, JToken value);
        bool TryGetGameState(int deltaTimeMsec, out JObject gameState);
    }
}