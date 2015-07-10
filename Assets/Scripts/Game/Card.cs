using UnityEngine;
using System.Collections;

public class Card
{
    public readonly static int ACE = 1;      // Codes for the non-numeric cards.
    public readonly static int JACK = 11;    //   Cards 2 through 10 have their 
    public readonly static int QUEEN = 12;   //   numerical values for their codes.
    public readonly static int KING = 13;

    public CardSuit cardSuit;
    public int number;

    public Card(CardSuit suit, int number)
    {
        this.cardSuit = suit;
        this.number = number;
    }

    public string NumericName
    {
        get
        {
            switch (number)
            {
                case 1:
                    return "Ace";
                case 11:
                    return "J";
                case 12:
                    return "Q";
                case 13:
                    return "K";
                default:
                    return number.ToString();
            }
        }
    }

    public int GetScore()
    {
        switch (number)
        {
            case 1:
                return 10;
            case 11:
                return 10;
            case 12:
                return 10;
            case 13:
                return 10;
            default:
                return 1;
        }
    }
}
// Not necessary for this game but looks nice!
public enum CardSuit
{
    Clubs, Diamonds, Hearts, Spades
}
