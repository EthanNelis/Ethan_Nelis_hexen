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

    private List<CardView> _cards = new List<CardView>();

    [SerializeField]
    private Transform _hand;

    private void OnEnable()
    {
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
        int randomValue = UnityEngine.Random.Range(0, _cardTypes);

        CardView cardView = null;

        if(randomValue == 0)
        {
            cardView = Instantiate(_teleportCard, transform);
        }
        if(randomValue == 1)
        {
            cardView = Instantiate(_slashCard, transform);

        }
        if (randomValue == 2)
        {
            cardView = Instantiate(_swipeCard, transform);
        }
        if (randomValue == 3)
        {
            cardView = Instantiate(_pushbackCard, transform);
        }

        return cardView;
    }



}

