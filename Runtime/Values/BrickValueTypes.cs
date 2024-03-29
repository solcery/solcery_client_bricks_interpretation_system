namespace Solcery.BrickInterpretation.Runtime.Values
{
    public enum BrickValueTypes
    {
        Constant = 0,
        Variable = 1,
        Attribute = 2,
        Argument = 3,
        IfThenElse = 4,
        Addition = 5,
        Subtraction = 6,
        Multiplication = 7,
        Division = 8,
        Modulo = 9,
        Random = 10,
        CardId = 11,
        CardTypeId = 12,
        GameAttribute = 13,
        IteratorSum = 14,
        SetVariable = 15,
        IteratorMax = 16,
        IteratorMin = 17,
        GetScopeVariable = 18,
        SetScopeVariable = 19
    }
}