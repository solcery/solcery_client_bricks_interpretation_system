namespace Solcery.BrickInterpretation.Runtime.Actions
{
    public enum BrickActionTypes
    {
        Void = 0,
        TwoActions = 1,
        IfThenElse = 2,
        Loop = 3,
        Argument = 4,
        Iterator = 5,
        SetVariable = 6,
        SetAttribute = 7,
        UseCard = 8,
        SetGameAttribute = 9,
        Pause = 10,
        Event = 11,
        CreateObject = 12,
        DeleteObject = 13,
        ClearAttrs = 14,
        StartTimer = 15,
        StopTimer = 16,
        Transform = 17,
        ConsoleLog = 256
    }
}