using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button doubleBtn;
    public Button resetBtn;
    public Button quitBtn;



    public bool isRoundOver;
    

   // private int standClicks = 0;

    public PlayerScript playerScript;
    public PlayerScript dealerScript;
    public DeckScript deckScript;

    public TMP_Text betsText;
    public TMP_Text delarHandText;
    public TMP_Text playerHandText;
    public TMP_Text cashText;
    public TMP_Text standBtnText;

    public Text mainText;

    public Button bet_5Btn;
    public Button bet_10Btn;
    public Button bet_20Btn;
    public Button bet_50Btn;
    public Button bet_100Btn;
    public Button allinBtn;
    public Button betOverBtn;

    public GameObject hideCard;

    //bet amount
    public int pot = 0;
    public bool canBet;

    public bool gameOver;
    public GameObject gameOverPanel;

    public Text loseAmountText;

    public AudioSource cardAudio;
    public AudioSource betAudio;
    public AudioSource windAudio;
    public AudioSource losedAudio;
    public AudioSource shuffelAudio;
    public AudioSource gameoverAudio;

    void Start()
    {
        // add  listener
        dealBtn.onClick.AddListener(() => Test());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        resetBtn.onClick.AddListener(() => ResetClicked());
        doubleBtn.onClick.AddListener(() => DoubleClicked());
        bet_5Btn.onClick.AddListener(() => BetClicked(bet_5Btn));
        bet_10Btn.onClick.AddListener(() => BetClicked(bet_10Btn));
        bet_20Btn.onClick.AddListener(() => BetClicked(bet_20Btn));
        bet_50Btn.onClick.AddListener(() => BetClicked(bet_50Btn));
        bet_100Btn.onClick.AddListener(() => BetClicked(bet_100Btn));
        allinBtn.onClick.AddListener(() => AllinClicked());
        betOverBtn.onClick.AddListener(() => BetOverClicked());
        quitBtn.onClick.AddListener(() => QuitGame());

    }

    public void ResetClicked()
    {
        playerScript.ResetHand();
        dealerScript.ResetHand();
        delarHandText.gameObject.SetActive(false);
        playerHandText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);

        dealBtn.gameObject.SetActive(true);
        //hitBtn.gameObject.SetActive(true);
        //standBtn.gameObject.SetActive(true);
       // doubleBtn.gameObject.SetActive(true);
        resetBtn.gameObject.SetActive(false);
        betsText.gameObject.SetActive(false);
        isRoundOver = false;
    }

    private void StandClicked()
    {
        
        //standClicks++;
        //if (standClicks > 1) RoundOver();
        standBtn.gameObject.SetActive(false);
        hideCard.gameObject.SetActive(false);
        delarHandText.gameObject.SetActive(true);
        hitBtn.gameObject.SetActive(false);
        doubleBtn.gameObject.SetActive(false);
        StartCoroutine(HitDealer());
       // standBtnText.text = "Call";
        //standBtn.gameObject.SetActive(true);

    }

    private IEnumerator HitDealer()
    {
        for (int i = 0; i < 9; i++)
        {
            yield return new WaitForSeconds(1.0f);
            if (dealerScript.handvalue + deckScript.GetNextCardValue() <= 21 && dealerScript.cardIndex < 9 ||
                dealerScript.handvalue < 15 && dealerScript.cardIndex < 9)
            {
                dealerScript.GetCard();
                cardAudio.Play();
                delarHandText.text = dealerScript.handvalue.ToString();
                if (dealerScript.handvalue > 20)
                {
                    //RoundOver();
                    break;
                    
                }
            }
            else
            {
                //RoundOver();
                break;
            }
        }
        RoundOver();
    }


    /* private void WaitAndDo()
     {


         dealerScript.GetCard();
         delarHandText.text = dealerScript.handvalue.ToString();
         if (dealerScript.handvalue > 20) RoundOver();


     }*/


    private void HitClicked()
    {
        
        if (playerScript.cardIndex < 9)
        {
            Debug.Log("hit complete");
            cardAudio.Play();
            playerScript.GetCard();
            playerHandText.text = playerScript.handvalue.ToString();
        }
        if (playerScript.handvalue > 20) RoundOver();
    }

    /// <summary>
    /// dealClicked
    /// </summary>
    private void Test()
    {


        playerScript.ResetHand();
        dealerScript.ResetHand();

        shuffelAudio.Play();
        GameObject.Find("Deck").GetComponent<DeckScript>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        cardAudio.Play();
        playerHandText.gameObject.SetActive(true);
        playerHandText.text = playerScript.handvalue.ToString();
        delarHandText.text = dealerScript.handvalue.ToString();

        hideCard.gameObject.SetActive(true);

        // //hideCard.GetComponent<Renderer>().enabled = true;


        pot = 40;//already have
        betsText.text = "$" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        Debug.Log("stuck8");

        dealBtn.gameObject.SetActive(false);
        allinBtn.gameObject.SetActive(true);
        bet_5Btn.gameObject.SetActive(true);
        bet_10Btn.gameObject.SetActive(true);
        bet_20Btn.gameObject.SetActive(true);
        bet_50Btn.gameObject.SetActive(true);
        bet_100Btn.gameObject.SetActive(true);
        betOverBtn.gameObject.SetActive(true);
        betsText.gameObject.SetActive(true);
    }

    /*private void DealClicked()
    {
        playerScript.ResetHand();
        dealerScript.ResetHand();
        GameObject.Find("Deck").GetComponent<DeckScript>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        playerHandText.gameObject.SetActive(true);
        playerHandText.text = playerScript.handvalue.ToString();
        delarHandText.text = dealerScript.handvalue.ToString();

        hideCard.gameObject.SetActive(true);
        //hideCard.GetComponent<Renderer>().enabled = true;


        pot = 40;//already have
        betsText.text = "$" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        Debug.Log("stuck4");
        StartBet();
    }*/

   /* private void DealClickedTwo()
    {
        Debug.Log("stuck3");
        playerScript.ResetHand();
        dealerScript.ResetHand();
        GameObject.Find("Deck").GetComponent<DeckScript>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        playerHandText.gameObject.SetActive(true);
        playerHandText.text = playerScript.handvalue.ToString();
        delarHandText.text = dealerScript.handvalue.ToString();

        hideCard.gameObject.SetActive(true);
        //hideCard.GetComponent<Renderer>().enabled = true;


        pot = 40;//already have
        betsText.text = "$" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.GetMoney().ToString();
        Debug.Log("stuck4");
        StartBet();
    }*/

    private void DoubleClicked()
    {
        playerScript.AdjustMoney(-pot/2);
        pot *= 2;
        cashText.text = "$" + playerScript.GetMoney().ToString();
        betsText.text = "$" + pot.ToString();

        HitClicked();
        if (!isRoundOver)
        {
            Debug.Log("ready to do stand");
            StandClicked();
        }

    }

    void RoundOver()
    {
        Debug.Log("RoundOver complete");
        hideCard.gameObject.SetActive(false);
        delarHandText.gameObject.SetActive(true);
        bool playerBust = playerScript.handvalue > 21;
        bool dealerBust = dealerScript.handvalue > 21;
        bool player21 = playerScript.handvalue == 21;
        bool dealer21 = dealerScript.handvalue == 21;

        bool roundOver = true;
        //全爆牌
        if(playerBust && dealerBust)
        {
            mainText.text = "ALL Bust: Bets returned";
            playerScript.AdjustMoney(pot/2);
        }

        //player爆牌或者比庄家小
        else if (playerBust || !dealerBust && (dealerScript.handvalue > playerScript.handvalue))
        {
            int temp = pot / 2;
            losedAudio.Play();
            mainText.text = "You Lose! ::" + "-" + temp.ToString();
        }

        //player win
        else if (dealerBust || !playerBust && (playerScript.handvalue > dealerScript.handvalue))
        {
            int temp = pot / 2;
            windAudio.Play();
            mainText.text = "You Win! ::" + "+" + temp.ToString();
            playerScript.AdjustMoney(pot);
        }
        //平局
        else if (playerScript.handvalue == dealerScript.handvalue)
        {
            mainText.text = "Push: Bets returned";
            playerScript.AdjustMoney(pot / 2);
        }

        else
        {
            roundOver = false;
        }
        //update UI
        if (roundOver)
        {
            dealBtn.gameObject.SetActive(false);
            resetBtn.gameObject.SetActive(true);
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            doubleBtn.gameObject.SetActive(false);
            mainText.gameObject.SetActive(true);

            cashText.text = "$" + playerScript.GetMoney().ToString();


            
        }
        isRoundOver = true;
        if (playerScript.GetMoney() <= 0)
        {
            gameOver = true;
            gameoverAudio.Play();
        }
    }

    void BetClicked(Button button)
    {
        betAudio.Play();
        Text newBet = button.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = Convert.ToInt32(newBet.text);
        if (playerScript.GetMoney() - intBet >= 0)
        {
            playerScript.AdjustMoney(-intBet);
            cashText.text = "$" + playerScript.GetMoney().ToString();
            pot += intBet * 2;
            betsText.text = "$" + pot.ToString();
        }
        ChangeBatStates();
    }

    public void AllinClicked()
    {
        int newBet = playerScript.GetMoney();
        playerScript.AdjustMoney(-newBet);
        pot += newBet * 2;
        cashText.text = "$" + playerScript.GetMoney().ToString();
        betsText.text = "$" + pot.ToString();

        allinBtn.gameObject.SetActive(false);
        bet_5Btn.gameObject.SetActive(false);
        bet_10Btn.gameObject.SetActive(false);
        bet_20Btn.gameObject.SetActive(false);
        bet_50Btn.gameObject.SetActive(false);
        bet_100Btn.gameObject.SetActive(false);
        canBet = false;
    }

     public void ChangeBatStates()
     {

             int cash = playerScript.GetMoney();

             if (cash <=0)
             {
                 allinBtn.gameObject.SetActive(false);
                 bet_5Btn.gameObject.SetActive(false);
                 bet_10Btn.gameObject.SetActive(false);
                 bet_20Btn.gameObject.SetActive(false);
                 bet_50Btn.gameObject.SetActive(false);
                 bet_100Btn.gameObject.SetActive(false);
                 canBet = false;
             }
             else if (cash < 5)
             {
                 bet_5Btn.gameObject.SetActive(false);
                 bet_10Btn.gameObject.SetActive(false);
                 bet_20Btn.gameObject.SetActive(false);
                 bet_50Btn.gameObject.SetActive(false);
                 bet_100Btn.gameObject.SetActive(false);
        }
             else if (cash < 10)
             {
                 bet_10Btn.gameObject.SetActive(false);
                 bet_20Btn.gameObject.SetActive(false);
                 bet_50Btn.gameObject.SetActive(false);
                 bet_100Btn.gameObject.SetActive(false);
        }
             else if (cash < 20)
             {
                 bet_20Btn.gameObject.SetActive(false);
                 bet_50Btn.gameObject.SetActive(false);
                 bet_100Btn.gameObject.SetActive(false);
        }
             else if (cash < 50)
             {
                 bet_50Btn.gameObject.SetActive(false);
                 bet_100Btn.gameObject.SetActive(false);
        }
             else if (cash < 100)
             {
                 bet_100Btn.gameObject.SetActive(false);
             }

     }



    public void BetOverClicked()
    {
        allinBtn.gameObject.SetActive(false);
        bet_5Btn.gameObject.SetActive(false);
        bet_10Btn.gameObject.SetActive(false);
        bet_20Btn.gameObject.SetActive(false);
        bet_50Btn.gameObject.SetActive(false);
        bet_100Btn.gameObject.SetActive(false);
        betOverBtn.gameObject.SetActive(false);

        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        doubleBtn.gameObject.SetActive(true);
    }

    public void GameOver(bool gameover)
    {
        if (gameover)
        {
            resetBtn.gameObject.SetActive(false);
            quitBtn.gameObject.SetActive(false);
            betsText.gameObject.SetActive(false);
            mainText.gameObject.SetActive(false);

            int tmp = 1000 - playerScript.GetMoney();
            loseAmountText.text = tmp.ToString();
        }

        gameOverPanel.SetActive(gameover);
    }

    public void Update()
    {
        GameOver(gameOver);
    }

    public void RestarScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
