using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : PlayerBase
{
    [SerializeField]
    private Button[] _buttonCards;
    [SerializeField]
    private Text[] _textCards;

    public override bool IsMyTurn
    {
        get { return _isMyTurn; }
        set { _isMyTurn = value; }
    }

    // Called from UI buttons for this time
    public override void ThrowCard(int index)
    {
        if (!_isMyTurn)
        {
            Debug.Log("Not my turn!");
            return;
        }
        // not my turn
        IsMyTurn = false;
        Card card = mDeck[index];
        // remove it
        mDeck.Remove(index);
        Debug.Log("Player Thrown card " + card.NumericName);
        // disable button
        _textCards[index].text = string.Empty;
        _buttonCards[index].interactable = false;
        // tell game 
        mGame.ThrowCard(card);
    }

    // Adds card to players deck
    public override void AddCard(int index, Card card)
    {
        base.AddCard(index, card);
        // when round finishes and we get back 4 cards show them
        if (base.CardCount == 4)
            SetMyCards();
        //Debug.Log("Added player Card " + card.NumericName);
    }

    // Set UI for visual
    private void SetMyCards()
    {
        for (int i = 0; i < base.CardCount; i++)
        {
            _textCards[i].text = string.Format("{0}\n{1}", base.mDeck[i].cardSuit.ToString(), base.mDeck[i].NumericName);
            _buttonCards[i].interactable = true;
        }
    }
}
