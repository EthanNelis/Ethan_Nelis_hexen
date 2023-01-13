using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private CardView _teleportCard;

    [SerializeField]
    private CardView _slashCard;

    [SerializeField]
    private CardView _swipeCard;

    [SerializeField]
    private CardView _pushbackCard;

    private int _maxCardCount = 12;
    private int _maxCardsInHand = 5;
    private int _cardTypes = 4;
    private int _maxCardsPerType;

    private List<CardView> _cards = new List<CardView>();

    [SerializeField]
    private Transform _hand;

    private void OnEnable()
    {
        _maxCardsPerType = _maxCardCount / _cardTypes;

        InstantiateDeck(_maxCardCount);

        for (int i = 0; i < _maxCardsInHand; i++)
        {
            DrawCard();
        }
    }


    public void DrawCard()
    {   
        if(_cards.Count > 0)
        {
            int index = _cards.Count - 1;
            _cards[index].transform.SetParent(_hand);
            _cards[index].gameObject.SetActive(true);
            _cards.RemoveAt(index);
        }
    }

    public void InstantiateDeck(int maxCardCount)
    {
        for(int i = 0; i < maxCardCount; i++)
        {
            CardView card = GetRandomCard();
            _cards.Add(card);
            card.gameObject.SetActive(false);
        }
    }



    public CardView GetRandomCard()
    {
        CardView cardView = null;

        while(cardView == null)
        {
            int randomValue = UnityEngine.Random.Range(0, _cardTypes);


            if (randomValue == 0 && CanAddCardOfType(CardType.TeleportCard))
            {
                cardView = Instantiate(_teleportCard, transform);
            }
            if (randomValue == 1 && CanAddCardOfType(CardType.SlashCard))
            {
                cardView = Instantiate(_slashCard, transform);

            }
            if (randomValue == 2 && CanAddCardOfType(CardType.SwipeCard))
            {
                cardView = Instantiate(_swipeCard, transform);
            }
            if (randomValue == 3 && CanAddCardOfType(CardType.PushbackCard))
            {
                cardView = Instantiate(_pushbackCard, transform);
            }
        }

        return cardView;
    }

    private bool CanAddCardOfType(CardType type)
    {
        int counter = 0;

        if (_cards.Count > 0)
        {
            foreach (CardView card in _cards)
            {
                if (card.Type == type)
                {
                    counter++;
                }
            }
            return counter < _maxCardsPerType;
        }
        else return true;
    }

}

