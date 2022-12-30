using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Deck
{
    private int _totalCardsAmount;
    private int _cardsInHand;

    public int TotalCardsAmount => _totalCardsAmount;
    public int CardsInHand => _cardsInHand;

    public Deck(int totalCardsAmount, int cardsInHand)
    {
        _totalCardsAmount = totalCardsAmount;
        _cardsInHand = cardsInHand;
    }

}

