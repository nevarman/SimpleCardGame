using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game : EventReceiverBehaviour
{
    [SerializeField]
    private Text _textThrownCards;
    [SerializeField]
    private Deck _deck;
    [SerializeField]
    private PlayerBase[] _players;
    [SerializeField]
    private Text _textDeskScore;
    //[SerializeField]
    //private Player _player;
    //[SerializeField]
    //private int _numberOfPlayers = 2;
    //[SerializeField]
    //private PlayerAI[] _playersAI;
    private int _playerOrder = 0;
    private List<Card> _thrownDeck = new List<Card>();
    private Card _lastThrownCard = null;
    private int _currentScore;

    private int PlayerOrder
    {
        get { return _playerOrder; }
        set
        {
            _playerOrder = value;
            if (_playerOrder > _players.Length - 1)
                _playerOrder = 0;
        }
    }
    private int CurrentScore
    {
        get { return _currentScore; }
        set
        {
            _currentScore = value;
            _textDeskScore.text = "Desk Score: " + _currentScore;
        }
    }

    public override void OnStart()
    {
        base.OnStart();
        // start with 4 cards
        Setup();
    }

    void Setup()
    {
        for (int i = 0; i < 4; i++)
        {
            Card card = _deck.GetRandomCard();
            _thrownDeck.Add(card);
            CurrentScore += card.GetScore();
            _lastThrownCard = card;
            _textThrownCards.text = string.Format("\n {0} {1}", card.cardSuit.ToString(), card.NumericName);
            //Debug.Log("Setup " + _lastThrownCard.NumericName);
        }
        DealCards();
    }

    private void DealCards()
    {
        if (_deck.NumberOfCards > 0)
        {
            Debug.Log("Dealing");
            for (int i = 0; i < _players.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Card card = _deck.GetRandomCard();
                    _players[i].AddCard(j, card);
                }
            }
            GiveTurnToCurrentPlayer();
        }
        else
        {
            // game ended check score
            Debug.Log("cards finished");
            int score = 0;
            int winner = 0;
            for (int i = 0; i < _players.Length; i++)
            {
                if (_players[i].Score > score)
                {
                    score = _players[i].Score;
                    winner = i;
                }
            }
            GameController.instance.EndGame(winner);
        }
    }

    public void ThrowCard(Card card)
    {
        _thrownDeck.Add(card);
        CurrentScore += card.GetScore();
        // show them ?
        _textThrownCards.text = string.Format("\n {0} {1}", card.cardSuit.ToString(), card.NumericName);
        //Debug.Log(_lastThrownCard.NumericName);
        // check card
        if (_lastThrownCard == null)
        {
            Debug.Log("First card");
            _lastThrownCard = card;
            CheckPlayersStatus();
            return;
        }
        // we have thrown card
        if (card.number == _lastThrownCard.number)
        {
            if (_thrownDeck.Count == 1)
            {
                // Pisti take it 
                CurrentScore += 10;
                TakeCardsForCurrentPlayer();
                return;
            }
            // normal take it
            TakeCardsForCurrentPlayer();
            return;
        }
        else if (card.number == Card.JACK)
        {
            // take it all
            TakeCardsForCurrentPlayer();
            return;
        }
        // else continue
        _lastThrownCard = card;
        // check status
        CheckPlayersStatus();
    }

    private void TakeCardsForCurrentPlayer()
    {
        Debug.Log("taking card "+ PlayerOrder);
        // reset UI 
        _textThrownCards.text = string.Empty;
        _lastThrownCard = null;
        _thrownDeck = new List<Card>();
        //TODO give score to player
        AddScoreToPlayer();
        // check status
        CheckPlayersStatus();
    }   

    private void CheckPlayersStatus()
    {
        // change player order
        PlayerOrder++;
        for (int i = 0; i < _players.Length; i++)
        {
            // if we have cards continue
            if (_players[i].CardCount > 0)
            {
                GiveTurnToCurrentPlayer();
                return;
            }
        }
        // else deal card
        DealCards();
    }

    private void GiveTurnToCurrentPlayer()
    {
        _players[PlayerOrder].IsMyTurn = true;
    }
    private void AddScoreToPlayer()
    {
        _players[PlayerOrder].Score += CurrentScore;
        CurrentScore = 0;
    }
}
