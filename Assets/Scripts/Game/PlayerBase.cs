using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class PlayerBase : MonoBehaviour
{
    public int Score 
    { 
        get { return _score; } 
        set 
        { 
            _score = value; 
            _textScore.text = "Score " + _score; 
        }
    }
    public int CardCount { get { return mDeck.Count; } }
    protected Dictionary<int, Card> mDeck = new Dictionary<int, Card>();
    protected Game mGame;
    protected bool _isMyTurn;
    [SerializeField]
    protected Text _textScore;
    private int _score;
    
    public abstract bool IsMyTurn { get; set; }
    public abstract void ThrowCard(int index);

    public virtual void Start()
    {
        mGame = FindObjectOfType<Game>();
    }

    public virtual void AddCard(int index,Card card)
    {
        mDeck.Add(index, card);
    }
}
