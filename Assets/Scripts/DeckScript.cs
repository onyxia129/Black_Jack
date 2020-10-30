using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;
    void Start()
    {
        GetCardValues();
    }


    void GetCardValues()
    {
        int num = 0;
        for(int i = 0; i < cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            if(num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
    }

    public void shuffle()
    {
        for (int i = cardSprites.Length - 1; i > 0; i--)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
            currentIndex = 1;
        }
    }

    public int DealCard(CardScript cardScript)
    {
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetValueCard();
        
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }

    public int GetNextCardValue()
    {
        return cardValues[currentIndex];
    }
}
