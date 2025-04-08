using GameLogic.Enums;

namespace GameLogic;

public readonly struct Card(CardValue value, CardSuit suit)
{
    public readonly CardValue Value = value;
    public readonly CardSuit Suit = suit;
}
