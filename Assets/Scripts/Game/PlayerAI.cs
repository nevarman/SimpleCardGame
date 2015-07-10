using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerAI : PlayerBase
{

    public override bool IsMyTurn
    {
        get
        {
            return _isMyTurn;
        }
        set
        {
            _isMyTurn = value;
            if (_isMyTurn)
            {
                // TODO do a proper AI here without Linq!!
                int index = Random.Range(0, mDeck.Count);
                ThrowCard(index);
            }
        }
    }

    public override void ThrowCard(int index)
    {
        // get random card
        int key = mDeck.ElementAt(index).Key;
        Card c = mDeck[key];
        mDeck.Remove(key);
        Debug.Log("Player AI Thrown card " + c.NumericName);
        // Throw it
        mGame.ThrowCard(c);
        // not ai turn
        IsMyTurn = false;
        
    }
}
