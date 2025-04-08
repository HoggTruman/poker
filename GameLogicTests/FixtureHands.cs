using GameLogic;
using GameLogic.Enums;

namespace GameLogicTests;

public static class FixtureHands
{
    public static Hand RoyalFlush => new([
        new Card(CardValue.Ace, CardSuit.Hearts),
        new Card(CardValue.King, CardSuit.Hearts),
        new Card(CardValue.Queen, CardSuit.Hearts),
        new Card(CardValue.Jack, CardSuit.Hearts),
        new Card(CardValue.Ten, CardSuit.Hearts)
    ]);

    public static Hand StraightFlush => new([
        new Card(CardValue.Two, CardSuit.Hearts),
        new Card(CardValue.Three, CardSuit.Hearts),
        new Card(CardValue.Four, CardSuit.Hearts),
        new Card(CardValue.Five, CardSuit.Hearts),
        new Card(CardValue.Six, CardSuit.Hearts)
    ]);

    public static Hand FourOfAKind => new([
        new Card(CardValue.Two, CardSuit.Hearts),
        new Card(CardValue.Two, CardSuit.Diamonds),
        new Card(CardValue.Two, CardSuit.Clubs),
        new Card(CardValue.Two, CardSuit.Spades),
        new Card(CardValue.Six, CardSuit.Hearts)
    ]);

    public static Hand FullHouse => new([
        new Card(CardValue.Two, CardSuit.Hearts),
        new Card(CardValue.Two, CardSuit.Diamonds),
        new Card(CardValue.Two, CardSuit.Clubs),
        new Card(CardValue.Six, CardSuit.Spades),
        new Card(CardValue.Six, CardSuit.Hearts)
    ]);

    public static Hand Flush => new([
        new Card(CardValue.Ace, CardSuit.Hearts),
        new Card(CardValue.Five, CardSuit.Hearts),
        new Card(CardValue.Two, CardSuit.Hearts),
        new Card(CardValue.Nine, CardSuit.Hearts),
        new Card(CardValue.Six, CardSuit.Hearts)
    ]);

    public static Hand Straight => new([
        new Card(CardValue.Ace, CardSuit.Hearts),
        new Card(CardValue.Two, CardSuit.Spades),
        new Card(CardValue.Three, CardSuit.Diamonds),
        new Card(CardValue.Four, CardSuit.Clubs),
        new Card(CardValue.Five, CardSuit.Hearts)
    ]);

    public static Hand ThreeOfAKind => new([
        new Card(CardValue.Seven, CardSuit.Hearts),
        new Card(CardValue.Seven, CardSuit.Spades),
        new Card(CardValue.Seven, CardSuit.Diamonds),
        new Card(CardValue.Four, CardSuit.Clubs),
        new Card(CardValue.Five, CardSuit.Hearts)
    ]);

    public static Hand TwoPairs => new([
        new Card(CardValue.Seven, CardSuit.Hearts),
        new Card(CardValue.Seven, CardSuit.Spades),
        new Card(CardValue.Four, CardSuit.Diamonds),
        new Card(CardValue.Four, CardSuit.Clubs),
        new Card(CardValue.Five, CardSuit.Hearts)
    ]);

    public static Hand Pair => new([
        new Card(CardValue.Seven, CardSuit.Hearts),
        new Card(CardValue.Seven, CardSuit.Spades),
        new Card(CardValue.Four, CardSuit.Diamonds),
        new Card(CardValue.Jack, CardSuit.Clubs),
        new Card(CardValue.Five, CardSuit.Hearts)
    ]);

    public static Hand HighCard => new([
        new Card(CardValue.Seven, CardSuit.Hearts),
        new Card(CardValue.Nine, CardSuit.Spades),
        new Card(CardValue.Four, CardSuit.Diamonds),
        new Card(CardValue.Jack, CardSuit.Clubs),
        new Card(CardValue.Five, CardSuit.Hearts)
    ]);
}
