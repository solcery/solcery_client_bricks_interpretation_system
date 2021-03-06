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
        bool TryGetGameState(int deltaTimeMsec, out JObject gameState);
    }
}