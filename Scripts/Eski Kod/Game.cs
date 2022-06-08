using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Clicker
    public Text scoreText;
    public float currentScore;
    public float hitPower;
    public float scoreIncreasedPerSecond;
    public float x;

    //Shop
    public int shop1prize;
    public Text shop1text;

    public int shop2prize;
    public Text shop2text;

    //Amount
    public Text amount1Text;
    public int amount1;
    public float amount1Profit;

    public Text amount2Text;
    public int amount2;
    public float amount2Profit;

    //Upgrade
    public int upgradePrize;
    public Text upgradeText;

    public int allUpgradePrize;
    public Text allUpgradeText;

    //Achievement
    public bool achievementScore;
    public bool achievementShop;

    public Image image1;
    public Image image2;

    //Level System
    public int level;
    public int exp;
    public int expToNextLevel;
    public Text levelText;

    //Highest Score
    public int bestScore;
    public Text bestScoreText;

    //Buttons
    public Sprite sp1, sp2, sp3, sp4;
    public Image clickerButton;

    public Text tx1, tx2, tx3, tx4;

    public int changeCost = 50;
    public int currentButton = 1;

    void Start()
    {
        //Clicker
        currentScore = 0;
        hitPower = 1;
        scoreIncreasedPerSecond = 1;
        x = 0f;

        //We must set all default variables before load
        shop1prize = 25;
        shop2prize = 125;
        amount1 = 0;
        amount1Profit = 1;
        amount2 = 0;
        amount2Profit = 5;

        //Reset Line
        PlayerPrefs.DeleteAll();

        //Load
        currentScore = PlayerPrefs.GetInt("currentScore", 0);
        hitPower = PlayerPrefs.GetInt("hitPower", 1);
        x = PlayerPrefs.GetInt("x", 0);

        shop1prize = PlayerPrefs.GetInt("shop1prize", 25);
        shop2prize = PlayerPrefs.GetInt("shop2prize", 125);
        amount1 = PlayerPrefs.GetInt("amount1", 0);
        amount1Profit = PlayerPrefs.GetInt("amount1Profit", 0);
        amount2 = PlayerPrefs.GetInt("amount2", 0);
        amount2Profit = PlayerPrefs.GetInt("amount2Profit", 0);
        upgradePrize = PlayerPrefs.GetInt("upgradePrize", 50);

        allUpgradePrize = 500;

        bestScore = PlayerPrefs.GetInt("bestScore", 0);


    }

    // Update is called once per frame
    void Update()
    {
        //Clicker
        scoreText.text = (int)currentScore + "$";
        scoreIncreasedPerSecond = x * Time.deltaTime;
        currentScore = currentScore + scoreIncreasedPerSecond;

        //Shop
        shop1text.text = "Tier 1: " + shop1prize + " $";
        shop2text.text = "Tier 2: " + shop2prize + " $";

        //Amount
        amount1Text.text = "Tier 1: " + amount1 + " arts $: " + amount1Profit + "/s";
        amount2Text.text = "Tier 2: " + amount2 + " arts $: " + amount2Profit + "/s";

        //Upgrade
        upgradeText.text = "Cost: " + upgradePrize + " $";

        //Save
        PlayerPrefs.SetInt("currentScore", (int)currentScore);
        PlayerPrefs.SetInt("hitPower", (int)hitPower);
        PlayerPrefs.SetInt("x", (int)x);

        PlayerPrefs.SetInt("shop1prize", (int)shop1prize);
        PlayerPrefs.SetInt("shop2prize", (int)shop2prize);
        PlayerPrefs.SetInt("amount1", (int)amount1);
        PlayerPrefs.SetInt("amount1Profit", (int)amount1Profit);
        PlayerPrefs.SetInt("amount2", (int)amount1);
        PlayerPrefs.SetInt("amount2Profit", (int)amount2Profit);
        PlayerPrefs.SetInt("upgradePrize", (int)upgradePrize);

        allUpgradeText.text = "Cost: " + allUpgradePrize + " $";

        PlayerPrefs.SetInt("bestScore", bestScore);

        //Achievement
        if (currentScore >= 50)
        {
            achievementScore = true;
        }

        if (amount1 >= 2)
        {
            achievementShop = true;
        }

        if (achievementScore == true)
        {
            image1.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image1.color = new Color(0.2f, 0.2f, 0.2f, 0.2f);
        }

        if (achievementShop == true)
        {
            image2.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            image2.color = new Color(0.2f, 0.2f, 0.2f, 0.2f);
        }

        //Level
        if (exp >= expToNextLevel)
        {
            level++;
            exp = 0;
            expToNextLevel *= 2;
        }

        levelText.text = level + " level";

        //Highest Score
        if (currentScore > bestScore)
        {
            bestScore = (int)currentScore;
        }

        bestScoreText.text = bestScore + " Best Score";

        //Buttons
        tx1.text = "Set for: " + changeCost;
        tx2.text = "Set for: " + changeCost;
        tx3.text = "Set for: " + changeCost;
        tx4.text = "Set for: " + changeCost;

        if (currentButton == 1)
        {
            clickerButton.sprite = sp1;
        }

        if (currentButton == 2)
        {
            clickerButton.sprite = sp2;
        }

        if (currentButton == 3)
        {
            clickerButton.sprite = sp3;
        }

        if (currentButton == 4)
        {
            clickerButton.sprite = sp4;
        }

    }

    //Hit
    public void Hit()
    {
        currentScore += hitPower;

        //Exp
        exp++;
    }

    //Shop
    public void Shop1()
    {
        if (currentScore >= shop1prize)
        {
            currentScore -= shop1prize;
            amount1 += 1;
            amount1Profit += 1;
            x += 1;
            shop1prize += 25;
        }
    }

    public void Shop2()
    {
        if (currentScore >= shop2prize)
        {
            currentScore -= shop2prize;
            amount2 += 1;
            amount2Profit += 5;
            x += 5;
            shop2prize += 125;
        }
    }

    //Upgrade
    public void Upgrade()
    {
        if (currentScore >= upgradePrize)
        {
            currentScore -= upgradePrize;
            hitPower *= 2;
            upgradePrize *= 3;
        }
    }

    public void AllProfitsUpgrade()
    {
        if (currentScore >= allUpgradePrize)
        {
            currentScore -= allUpgradePrize;
            x *= 2;
            allUpgradePrize *= 3;
            amount1Profit *= 2;
            amount2Profit *= 2;
        }
    }

    public void Button1()
    {
        if (currentScore >= changeCost)
        {
            currentScore -= changeCost;
            currentButton = 1;
        }
    }
    public void Button2()
    {
        if (currentScore >= changeCost)
        {
            currentScore -= changeCost;
            currentButton = 2;
        }
    }
    public void Button3()
    {
        if (currentScore >= changeCost)
        {
            currentScore -= changeCost;
            currentButton = 3;
        }
    }
    public void Button4()
    {
        if (currentScore >= changeCost)
        {
            currentScore -= changeCost;
            currentButton = 4;
        }
    }
}
