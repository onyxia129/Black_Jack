using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CardScript cardScript;
    public DeckScript deckScript;

    public int handvalue = 0;

    private int money = 1000;

    //手牌
    public GameObject[] hand;

    public int cardIndex = 0;
    //public int aceCount = 0;

    List<CardScript> aceList = new List<CardScript>();
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());

        hand[cardIndex].GetComponent<Renderer>().enabled = true;

        handvalue += cardValue;

        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());

        }

        AceCheck();
        cardIndex++;
        return handvalue;
    }

    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    public int GetMoney()
    {
        return money;
    }

    public void AceCheck()
    {
        foreach(CardScript ace in aceList)
        {
            if (handvalue + 10 < 22 && ace.GetValueCard() == 1)
            {
                ace.SetValue(11);
                handvalue += 10;
            }else if (handvalue > 21 && ace.GetValueCard() == 10)
            {
                ace.SetValue(1);
                handvalue -= 10;
            }
        }

    }

    public void ResetHand()
    {
        for(int i = 0; i <hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handvalue = 0;
        aceList = new List<CardScript>();
    }
}
