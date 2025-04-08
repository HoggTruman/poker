using GameLogic.Enums;

namespace GameLogic;

public class Hand
{
    public List<Card> Cards { get; private set; } = [];
    public HandRanking Ranking { get; private set; }
    private List<CardValue> _tieBreakers = [];

    public Hand(IEnumerable<Card> cards)
    {
        AddCards(cards);
    }

    public Result CompareWith(Hand hand)
    {
        if (Ranking == hand.Ranking) 
        {
            return BreakTie(hand);
        }
            
        return Ranking > hand.Ranking? Result.Win: Result.Loss;            
    }

    public void AddCards(IEnumerable<Card> newCards)
    {
        if (Cards.Count + newCards.Count() > 5)
        {
            throw new ArgumentException("A hand may not exceed 5 cards.");
        }

        Cards = [..Cards.Concat(newCards).OrderBy(x => x.Value)];

        if (Cards.Count == 5)
        {
            Ranking = GetRanking();
            _tieBreakers = GetTieBreakers(Ranking);
        }        
    }

    internal HandRanking GetRanking()
    {
        if (Cards.Count != 5)
        {
            throw new InvalidOperationException("In order to obtain a ranking a hand must contain 5 cards.");
        }

        bool isStraight = IsStraight();
        bool isFlush = IsFlush();
        Dictionary<CardValue, int> valueCounts = GetValueCounts();

        if (isStraight && isFlush) return HandRanking.StraightFlush;
        if (valueCounts.Any(x => x.Value == 4)) return HandRanking.FourOfAKind;
        if (valueCounts.Count == 2) return HandRanking.FullHouse;
        if (isFlush) return HandRanking.Flush;
        if (isStraight) return HandRanking.Straight;
        if (valueCounts.Any(x => x.Value == 3)) return HandRanking.ThreeOfAKind;
        if (valueCounts.Count == 3) return HandRanking.TwoPairs;
        if (valueCounts.Count == 4) return HandRanking.Pair;
        return HandRanking.HighCard;
    }

    private List<CardValue> GetTieBreakers(HandRanking ranking)
    {
        Dictionary<CardValue, int> valueCounts = GetValueCounts();

        if (ranking == HandRanking.StraightFlush ||            
            ranking == HandRanking.Straight)
        {
            return Cards.First().Value == CardValue.Two?
                [CardValue.Five]:
                [Cards.Last().Value];
        }
        if (ranking == HandRanking.FourOfAKind)
        {
            return [
                valueCounts.First(x => x.Value == 4).Key,
                valueCounts.First(x => x.Value == 1).Key
            ];
        }
        if (ranking == HandRanking.FullHouse)
        {
            return [
                valueCounts.First(x => x.Value == 3).Key,
                valueCounts.First(x => x.Value == 2).Key
            ];
        }
        if (ranking == HandRanking.ThreeOfAKind)
        {
            return [
                valueCounts.First(x => x.Value == 3).Key,
                ..valueCounts.Where(x => x.Value == 1).Select(x => x.Key).OrderDescending(),
            ];
        }
        if (ranking == HandRanking.TwoPairs)
        {
            return [
                ..valueCounts.Where(x => x.Value == 2).Select(x => x.Key).OrderDescending(),
                valueCounts.First(x => x.Value == 1).Key,
            ];
        }
        if (ranking == HandRanking.Pair)
        {
            return [
                valueCounts.First(x => x.Value == 2).Key,
                ..valueCounts.Where(x => x.Value == 1).Select(x => x.Key).OrderDescending()
            ];                
        }
        if (ranking == HandRanking.Flush ||
            ranking == HandRanking.HighCard)
        {
            return Cards.Select(x => x.Value).OrderDescending().ToList();
        }

        throw new Exception("Invalid hand evaluation provided");
    }

    private Result BreakTie(Hand opp)
    {
        for (int i = 0; i < _tieBreakers.Count; ++i)
        {
            if (_tieBreakers[i] > opp._tieBreakers[i]) {
                return Result.Win;
            }
            if (_tieBreakers[i] < opp._tieBreakers[i]) {
                return Result.Loss;
            }
        }

        return Result.Tie;        
    }

    internal bool IsFlush()
    {
        return Cards.All(x => x.Suit == Cards[0].Suit);
    }

    internal bool IsStraight()
    {
        if (Cards[0].Value == CardValue.Two &&
            Cards[1].Value == CardValue.Three &&
            Cards[2].Value == CardValue.Four &&
            Cards[3].Value == CardValue.Five &&
            Cards[4].Value == CardValue.Ace)
        {
            return true;
        }

        for (int i = 0; i < Cards.Count - 1; ++i)
        {
            if (Cards[i].Value + 1 != Cards[i + 1].Value)
            {
                return false;
            }
        }

        return true;
    }

    private Dictionary<CardValue, int> GetValueCounts()
    {
        Dictionary<CardValue, int> counts = [];
        foreach (Card card in Cards)
        {
            counts[card.Value] = counts.TryGetValue(card.Value, out int val)? val + 1: 1;
        }

        return counts;
    }
}
