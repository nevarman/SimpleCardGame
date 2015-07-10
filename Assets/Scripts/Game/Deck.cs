using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    readonly int _deckSize = 52;
    private List<Card> _deck = new List<Card>();

    public int NumberOfCards
    {
        get { return _deck.Count; }
    }

    void Start()
    {
        CreateCardDeck();
    }

    private void CreateCardDeck()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= _deckSize / 4; j++)
            {
                CardSuit suit = (CardSuit)i;
                Card card = new Card(suit, j);
                _deck.Add(card);
            }
        }
    }

    public Card GetRandomCard()
    {
        int i = Random.Range(0, NumberOfCards);
        Card c = _deck[i];
        _deck.RemoveAt(i);
        return c;
    }
}
