using GameLogic;
using GameLogic.Enums;

namespace GameLogicTests;

public class HandTests
{
    #region IsStraight Tests

    [Fact]
    public void IsStraight_WithLowAce_ReturnsTrue()
    {
        List<Card> cards = [
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Two, CardSuit.Clubs),
            new(CardValue.Three, CardSuit.Spades),
            new(CardValue.Four, CardSuit.Diamonds),
            new(CardValue.Five, CardSuit.Hearts),
        ];
        Hand hand = new(cards);
        Assert.True(hand.IsStraight());
    }

    [Fact]
    public void IsStraight_WithHighAce_ReturnsTrue()
    {
        List<Card> cards = [
            new(CardValue.Ten, CardSuit.Clubs),
            new(CardValue.Jack, CardSuit.Spades),
            new(CardValue.Queen, CardSuit.Diamonds),
            new(CardValue.King, CardSuit.Hearts),
            new(CardValue.Ace, CardSuit.Hearts),
        ];
        Hand hand = new(cards);
        Assert.True(hand.IsStraight());
    }

    [Theory]
    [InlineData(CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five, CardValue.Six)]
    [InlineData(CardValue.Eight, CardValue.Nine, CardValue.Ten, CardValue.Jack, CardValue.Queen)]
    public void IsStraight_ReturnsTrue(CardValue value1, CardValue value2, CardValue value3, CardValue value4, CardValue value5)
    {
        List<Card> cards = [
            new(value1, CardSuit.Clubs),
            new(value2, CardSuit.Spades),
            new(value3, CardSuit.Diamonds),
            new(value4, CardSuit.Hearts),
            new(value5, CardSuit.Hearts),
        ];
        Hand hand = new(cards);
        Assert.True(hand.IsStraight());
    }

    [Fact]
    public void IsStraight_InAnyOrder_ReturnsTrue()
    {
        List<Card> cards = [
            new(CardValue.Six, CardSuit.Clubs),
            new(CardValue.Four, CardSuit.Spades),
            new(CardValue.Seven, CardSuit.Diamonds),
            new(CardValue.Five, CardSuit.Hearts),
            new(CardValue.Eight, CardSuit.Hearts)
        ];
        Hand hand = new(cards);
        Assert.True(hand.IsStraight());
    }

    [Fact]
    public void IsStraight_WhenNotStraight_ReturnsFalse()
    {
        List<Card> cards = [
            new(CardValue.Two, CardSuit.Clubs),
            new(CardValue.Three, CardSuit.Spades),
            new(CardValue.Four, CardSuit.Diamonds),
            new(CardValue.Five, CardSuit.Hearts),
            new(CardValue.Eight, CardSuit.Hearts)
        ];
        Hand hand = new(cards);
        Assert.False(hand.IsStraight());
    }

    #endregion


    #region IsFlush Tests

    [Theory]
    [InlineData(CardSuit.Hearts)]
    [InlineData(CardSuit.Diamonds)]
    [InlineData(CardSuit.Clubs)]
    [InlineData(CardSuit.Spades)]
    public void IsFlush_WhenFlush_ReturnsTrue(CardSuit suit)
    {
        List<Card> cards = [
            new(CardValue.Two, suit),
            new(CardValue.Three, suit),
            new(CardValue.Four, suit),
            new(CardValue.Five, suit),
            new(CardValue.Eight, suit)
        ];
        Hand hand = new(cards);
        Assert.True(hand.IsFlush());
    }

    [Fact]
    public void IsFlush_WhenNotFlush_ReturnsFalse()
    {
        List<Card> cards = [
            new(CardValue.Two, CardSuit.Spades),
            new(CardValue.Three, CardSuit.Diamonds),
            new(CardValue.Four, CardSuit.Hearts),
            new(CardValue.Five, CardSuit.Clubs),
            new(CardValue.Eight, CardSuit.Hearts)
        ];
        Hand hand = new(cards);
        Assert.False(hand.IsFlush());
    }

    #endregion


    #region GetRanking Tests

    [Fact]
    public void GetRanking_WithoutFiveCards_ThrowsException()
    {
        List<Card> cards = [];
        Hand hand = new(cards);
        Assert.Throws<InvalidOperationException>(() => hand.GetRanking());
    }

    [Theory]
    [InlineData(CardValue.Ace, CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five)]
    [InlineData(CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace)]
    [InlineData(CardValue.Four, CardValue.Five, CardValue.Six, CardValue.Seven, CardValue.Eight)]
    public void GetRanking_WithStraightFlush_ReturnsStraightFlush(CardValue value1, CardValue value2, CardValue value3, CardValue value4, CardValue value5)
    {
        List<Card> cards = [
            new(value1, CardSuit.Spades),
            new(value2, CardSuit.Spades),
            new(value3, CardSuit.Spades),
            new(value4, CardSuit.Spades),
            new(value5, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.StraightFlush, result);
    }

    [Theory]
    [InlineData(CardValue.Ace, CardValue.Two)]
    [InlineData(CardValue.Four, CardValue.King)]
    [InlineData(CardValue.Queen, CardValue.Three)]
    public void GetRanking_WithFourOfAKind_ReturnsFourOfAKind(CardValue quadValue, CardValue singleValue)
    {
        List<Card> cards = [
            new(quadValue, CardSuit.Spades),
            new(quadValue, CardSuit.Diamonds),
            new(quadValue, CardSuit.Hearts),
            new(quadValue, CardSuit.Clubs),
            new(singleValue, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.FourOfAKind, result);
    }

    [Theory]
    [InlineData(CardValue.Ace, CardValue.Two)]
    [InlineData(CardValue.Four, CardValue.King)]
    [InlineData(CardValue.Queen, CardValue.Three)]
    public void GetRanking_WithFullHouse_ReturnsFullHouse(CardValue tripleValue, CardValue doubleValue)
    {
        List<Card> cards = [
            new(tripleValue, CardSuit.Spades),
            new(tripleValue, CardSuit.Diamonds),
            new(tripleValue, CardSuit.Hearts),
            new(doubleValue, CardSuit.Clubs),
            new(doubleValue, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.FullHouse, result);
    }

    [Theory]
    [InlineData(CardSuit.Hearts)]
    [InlineData(CardSuit.Spades)]
    [InlineData(CardSuit.Diamonds)]
    [InlineData(CardSuit.Clubs)]
    public void GetRanking_WithFlush_ReturnsFlush(CardSuit suit)
    {
        List<Card> cards = [
            new(CardValue.Ace, suit),
            new(CardValue.Eight, suit),
            new(CardValue.Four, suit),
            new(CardValue.Queen, suit),
            new(CardValue.Six, suit)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.Flush, result);
    }

    [Theory]
    [InlineData(CardValue.Ace, CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five)]
    [InlineData(CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace)]
    [InlineData(CardValue.Four, CardValue.Five, CardValue.Six, CardValue.Seven, CardValue.Eight)]
    public void GetRanking_WithStraight_ReturnsStraight(CardValue value1, CardValue value2, CardValue value3, CardValue value4, CardValue value5)
    {
        List<Card> cards = [
            new(value1, CardSuit.Spades),
            new(value2, CardSuit.Clubs),
            new(value3, CardSuit.Diamonds),
            new(value4, CardSuit.Clubs),
            new(value5, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.Straight, result);
    }

    [Theory]
    [InlineData(CardValue.Ace, CardValue.Four, CardValue.Five)]
    [InlineData(CardValue.Ten, CardValue.King, CardValue.Ace)]
    [InlineData(CardValue.Four, CardValue.Seven, CardValue.Eight)]
    public void GetRanking_WithThreeOfAKind_ReturnsThreeOfAKind(CardValue tripleValue, CardValue single1, CardValue single2)
    {
        List<Card> cards = [
            new(tripleValue, CardSuit.Spades),
            new(tripleValue, CardSuit.Clubs),
            new(tripleValue, CardSuit.Diamonds),
            new(single1, CardSuit.Clubs),
            new(single2, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.ThreeOfAKind, result);
    }

    [Theory]
    [InlineData(CardValue.Queen, CardValue.Six, CardValue.Five)]
    [InlineData(CardValue.Nine, CardValue.King, CardValue.Two)]
    [InlineData(CardValue.Four, CardValue.Ace, CardValue.Eight)]
    public void GetRanking_WithTwoPairs_ReturnsTwoPairs(CardValue pair1, CardValue pair2, CardValue single)
    {
        List<Card> cards = [
            new(pair1, CardSuit.Spades),
            new(pair1, CardSuit.Clubs),
            new(pair2, CardSuit.Diamonds),
            new(pair2, CardSuit.Clubs),
            new(single, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.TwoPairs, result);
    }

    [Theory]
    [InlineData(CardValue.Queen, CardValue.Six, CardValue.Five, CardValue.Eight)]
    [InlineData(CardValue.Nine, CardValue.King, CardValue.Two, CardValue.Jack)]
    [InlineData(CardValue.Four, CardValue.Ace, CardValue.Eight, CardValue.Two)]
    public void GetRanking_WithPair_ReturnsPair(CardValue pair, CardValue single1, CardValue single2, CardValue single3)
    {
        List<Card> cards = [
            new(pair, CardSuit.Spades),
            new(pair, CardSuit.Clubs),
            new(single1, CardSuit.Diamonds),
            new(single2, CardSuit.Clubs),
            new(single3, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.Pair, result);
    }

    [Theory]
    [InlineData(CardValue.Queen, CardValue.Six, CardValue.Five, CardValue.Eight, CardValue.Seven)]
    [InlineData(CardValue.Nine, CardValue.King, CardValue.Two, CardValue.Jack, CardValue.Four)]
    [InlineData(CardValue.Four, CardValue.Ace, CardValue.Eight, CardValue.Two, CardValue.Queen)]
    public void GetRanking_WithHighCard_ReturnsHighCard(CardValue value1, CardValue value2, CardValue value3, CardValue value4, CardValue value5)
    {
        List<Card> cards = [
            new(value1, CardSuit.Spades),
            new(value2, CardSuit.Clubs),
            new(value3, CardSuit.Diamonds),
            new(value4, CardSuit.Clubs),
            new(value5, CardSuit.Spades)
        ];
        Hand hand = new(cards);
        HandRanking result = hand.GetRanking();
        Assert.Equal(HandRanking.HighCard, result);
    }

    #endregion


    #region CompareWith Tests Without Tiebreakers

    [Fact]
    public void CompareWith_ForStraightFlush()
    {
        Hand hand = FixtureHands.StraightFlush;
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForFourOfAKind()
    {
        Hand hand = FixtureHands.FourOfAKind;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForFullHouse()
    {
        Hand hand = FixtureHands.FullHouse;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForFlush()
    {
        Hand hand = FixtureHands.Flush;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForStraight()
    {
        Hand hand = FixtureHands.Straight;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForThreeOfAKind()
    {
        Hand hand = FixtureHands.ThreeOfAKind;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForTwoPairs()
    {
        Hand hand = FixtureHands.TwoPairs;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForPair()
    {
        Hand hand = FixtureHands.Pair;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Win, hand.CompareWith(FixtureHands.HighCard));
    }

    [Fact]
    public void CompareWith_ForHighCard()
    {
        Hand hand = FixtureHands.HighCard;
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.StraightFlush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FourOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.FullHouse));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Flush));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Straight));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.ThreeOfAKind));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.TwoPairs));
        Assert.Equal(Result.Loss, hand.CompareWith(FixtureHands.Pair));
        Assert.Equal(Result.Tie, hand.CompareWith(FixtureHands.HighCard));
    }

    #endregion


    #region CompareWith Tests With Tiebreakers

    [Theory]
    [InlineData(CardValue.Ace, CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five)]
    [InlineData(CardValue.Five, CardValue.Six, CardValue.Seven, CardValue.Eight, CardValue.Nine)]
    public void CompareWith_StraightFlushWithTiebreaker(CardValue opp1, CardValue opp2, CardValue opp3, CardValue opp4, CardValue opp5)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.King, CardSuit.Hearts),
            new(CardValue.Queen, CardSuit.Hearts),
            new(CardValue.Jack, CardSuit.Hearts),
            new(CardValue.Ten, CardSuit.Hearts)
        ]);

        Hand opponentHand = new([
            new(opp1, CardSuit.Diamonds),
            new(opp2, CardSuit.Diamonds),
            new(opp3, CardSuit.Diamonds),
            new(opp4, CardSuit.Diamonds),
            new(opp5, CardSuit.Diamonds)
        ]);

        Assert.Equal(Result.Win, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.Two, CardValue.Six, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Four, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Six, Result.Loss)]
    public void CompareWith_FourOfAKindWithTiebreaker(CardValue oppQuad, CardValue oppSingle, Result expected)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Ace, CardSuit.Diamonds),
            new(CardValue.Ace, CardSuit.Clubs),
            new(CardValue.Ace, CardSuit.Spades),
            new(CardValue.Five, CardSuit.Hearts)
        ]);

        Hand opponentHand = new([
            new(oppQuad, CardSuit.Hearts),
            new(oppQuad, CardSuit.Diamonds),
            new(oppQuad, CardSuit.Clubs),
            new(oppQuad, CardSuit.Spades),
            new(oppSingle, CardSuit.Hearts)
        ]);

        Assert.Equal(expected, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.Two, CardValue.Six, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Four, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Six, Result.Loss)]
    public void CompareWith_FullHouseWithTiebreaker(CardValue oppTriple, CardValue oppDouble, Result expected)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Ace, CardSuit.Diamonds),
            new(CardValue.Ace, CardSuit.Clubs),
            new(CardValue.Five, CardSuit.Spades),
            new(CardValue.Five, CardSuit.Hearts)
        ]);

        Hand opponentHand = new([
            new(oppTriple, CardSuit.Hearts),
            new(oppTriple, CardSuit.Diamonds),
            new(oppTriple, CardSuit.Clubs),
            new(oppDouble, CardSuit.Spades),
            new(oppDouble, CardSuit.Hearts)
        ]);

        Assert.Equal(expected, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.Queen, CardValue.Six, CardValue.Five, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Six, CardValue.Five, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Five, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Seven, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Seven, CardValue.Four, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.King, CardValue.Seven, CardValue.Three, CardValue.Two, Result.Loss)]
    public void CompareWith_FlushWithTiebreaker(CardValue opp1, CardValue opp2, CardValue opp3, CardValue opp4, CardValue opp5, Result expected)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Ten, CardSuit.Hearts),
            new(CardValue.Seven, CardSuit.Hearts),
            new(CardValue.Four, CardSuit.Hearts),
            new(CardValue.Three, CardSuit.Hearts)
        ]);

        Hand opponentHand = new([
            new(opp1, CardSuit.Diamonds),
            new(opp2, CardSuit.Diamonds),
            new(opp3, CardSuit.Diamonds),
            new(opp4, CardSuit.Diamonds),
            new(opp5, CardSuit.Diamonds)
        ]);

        Assert.Equal(expected, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.Ace, CardValue.Two, CardValue.Three, CardValue.Four, CardValue.Five)]
    [InlineData(CardValue.Five, CardValue.Six, CardValue.Seven, CardValue.Eight, CardValue.Nine)]
    public void CompareWith_StraightWithTiebreaker(CardValue opp1, CardValue opp2, CardValue opp3, CardValue opp4, CardValue opp5)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.King, CardSuit.Clubs),
            new(CardValue.Queen, CardSuit.Diamonds),
            new(CardValue.Jack, CardSuit.Spades),
            new(CardValue.Ten, CardSuit.Hearts)
        ]);

        Hand opponentHand = new([
            new(opp1, CardSuit.Hearts),
            new(opp2, CardSuit.Clubs),
            new(opp3, CardSuit.Diamonds),
            new(opp4, CardSuit.Spades),
            new(opp5, CardSuit.Diamonds)
        ]);

        Assert.Equal(Result.Win, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.Queen, CardValue.Six, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Six, CardValue.Two,  Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Seven, CardValue.Two,  Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Seven,  Result.Loss)]
    [InlineData(CardValue.Ace, CardValue.Seven, CardValue.Four,  Result.Loss)]
    public void CompareWith_ThreeOfAKindWithTiebreaker(CardValue oppTriple, CardValue oppSingle1, CardValue oppSingle2, Result expected)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Ace, CardSuit.Diamonds),
            new(CardValue.Ace, CardSuit.Spades),
            new(CardValue.Seven, CardSuit.Hearts),
            new(CardValue.Three, CardSuit.Diamonds)
        ]);

        Hand opponentHand = new([
            new(oppTriple, CardSuit.Hearts),
            new(oppTriple, CardSuit.Diamonds),
            new(oppTriple, CardSuit.Spades),
            new(oppSingle1, CardSuit.Hearts),
            new(oppSingle2, CardSuit.Diamonds)
        ]);

        Assert.Equal(expected, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.King, CardValue.Ten, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Two,  Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Jack, CardValue.Two,  Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Queen, CardValue.Seven,  Result.Loss)]
    [InlineData(CardValue.Ace, CardValue.Jack, CardValue.Four,  Result.Loss)]
    public void CompareWith_TwoPairsWithTiebreaker(CardValue oppPair1, CardValue oppPair2, CardValue oppSingle, Result expected)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Ace, CardSuit.Diamonds),
            new(CardValue.Jack, CardSuit.Spades),
            new(CardValue.Jack, CardSuit.Hearts),
            new(CardValue.Three, CardSuit.Diamonds)
        ]);

        Hand opponentHand = new([
            new(oppPair1, CardSuit.Hearts),
            new(oppPair1, CardSuit.Diamonds),
            new(oppPair2, CardSuit.Spades),
            new(oppPair2, CardSuit.Hearts),
            new(oppSingle, CardSuit.Diamonds)
        ]);

        Assert.Equal(expected, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.King, CardValue.Ten, CardValue.Six, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Six, CardValue.Two,  Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Jack, CardValue.Six, CardValue.Two,  Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Jack, CardValue.Seven, CardValue.Two,  Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Queen, CardValue.Six, CardValue.Two,  Result.Loss)]
    [InlineData(CardValue.Ace, CardValue.Jack, CardValue.Nine, CardValue.Four,  Result.Loss)]
    public void CompareWith_PairWithTiebreaker(CardValue oppPair, CardValue oppSingle1, CardValue oppSingle2, CardValue oppSingle3, Result expected)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Ace, CardSuit.Diamonds),
            new(CardValue.Jack, CardSuit.Spades),
            new(CardValue.Seven, CardSuit.Hearts),
            new(CardValue.Three, CardSuit.Diamonds)
        ]);

        Hand opponentHand = new([
            new(oppPair, CardSuit.Hearts),
            new(oppPair, CardSuit.Diamonds),
            new(oppSingle1, CardSuit.Spades),
            new(oppSingle2, CardSuit.Hearts),
            new(oppSingle3, CardSuit.Diamonds)
        ]);

        Assert.Equal(expected, hand.CompareWith(opponentHand));
    }

    [Theory]
    [InlineData(CardValue.Queen, CardValue.Six, CardValue.Five, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Six, CardValue.Five, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Five, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Seven, CardValue.Three, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.Ten, CardValue.Seven, CardValue.Four, CardValue.Two, Result.Win)]
    [InlineData(CardValue.Ace, CardValue.King, CardValue.Seven, CardValue.Three, CardValue.Two, Result.Loss)]
    public void CompareWith_HighCardWithTiebreaker(CardValue opp1, CardValue opp2, CardValue opp3, CardValue opp4, CardValue opp5, Result expected)
    {
        Hand hand = new([
            new(CardValue.Ace, CardSuit.Hearts),
            new(CardValue.Ten, CardSuit.Diamonds),
            new(CardValue.Seven, CardSuit.Clubs),
            new(CardValue.Four, CardSuit.Spades),
            new(CardValue.Three, CardSuit.Hearts)
        ]);

        Hand opponentHand = new([
            new(opp1, CardSuit.Diamonds),
            new(opp2, CardSuit.Hearts),
            new(opp3, CardSuit.Clubs),
            new(opp4, CardSuit.Spades),
            new(opp5, CardSuit.Diamonds)
        ]);

        Assert.Equal(expected, hand.CompareWith(opponentHand));
    }



    #endregion
}