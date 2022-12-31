using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MoveSetCollection
{
    private Dictionary<CardType, CardMoveSet> _moveSets = new Dictionary<CardType, CardMoveSet>();

    public CardMoveSet MoveSet(CardType cardType)
    {
        return _moveSets[cardType];
    }

    public MoveSetCollection(Board board)
    {
        _moveSets.Add(CardType.TeleportCard, new TeleportCard(board));
        _moveSets.Add(CardType.SlashCard, new SlashCard(board));
        _moveSets.Add(CardType.SwipeCard, new SwipeCard(board));
        _moveSets.Add(CardType.PushbackCard, new PushbackCard(board));
    }

}

