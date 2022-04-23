using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{

    public GameObject hatObj;
    public GameObject sunObj;
    public GameObject tireObj;
    public GameObject tumbleObj;
    public GameObject currency;

    int hatCost = 20;
    int sunCost = 80;
    int tireCost = 180;
    int tumbleCost = 400;

    bool hatBought;
    bool sunBought;
    bool tireBought;
    bool tumbleBought;

    // Start is called before the first frame update
    void Start()
    {
        hatBought = false;
        sunBought = false;
        tireBought = false;
        tumbleBought = false;
        updateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateItems()
    {
        if (PlayerPrefs.GetString("tire_pic") == "yes")
        {
            tireObj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            tireObj.GetComponentInChildren<RawImage>().color = new Color(1, 1, 1, .4f);
            tireObj.GetComponentInChildren<TextMeshProUGUI>().text = "SOLD OUT";
            tireBought = true;
        }
        if (PlayerPrefs.GetString("sun_pic") == "yes")
        {
            sunObj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            sunObj.GetComponentInChildren<RawImage>().color = new Color(1, 1, 1, .4f);
            sunObj.GetComponentInChildren<TextMeshProUGUI>().text = "SOLD OUT";
            sunBought = true;
        }
        if (PlayerPrefs.GetString("cowboyhat_pic") == "yes")
        {
            hatObj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            hatObj.GetComponentInChildren<RawImage>().color = new Color(1, 1, 1, .4f);
            hatObj.GetComponentInChildren<TextMeshProUGUI>().text = "SOLD OUT";
            hatBought = true;
        }
        if (PlayerPrefs.GetString("tumbleweed_pic") == "yes")
        {
            tumbleObj.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            tumbleObj.GetComponentInChildren<RawImage>().color = new Color(1, 1, 1, .4f);
            tumbleObj.GetComponentInChildren<TextMeshProUGUI>().text = "SOLD OUT";
            tumbleBought = true;
        }

        currency.GetComponent<CurrencyUpdate>().updateCurrency();
    }

    public void purchaseBall(string ball)
    {
        if(ball == "hat")
        {
            if(PlayerPrefs.GetInt("Score") >= hatCost && !hatBought)
            {
                PlayerPrefs.SetString("cowboyhat_pic", "yes");
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - hatCost);

            }
        }
        if(ball == "tire")
        {
            if (PlayerPrefs.GetInt("Score") >= tireCost && !tireBought)
            {
                PlayerPrefs.SetString("tire_pic", "yes");
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - tireCost);

            }
        }
        if(ball == "sun")
        {
            if (PlayerPrefs.GetInt("Score") >= sunCost && !sunBought)
            {
                PlayerPrefs.SetString("sun_pic", "yes");
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - sunCost);

            }
        }
        if(ball == "tumbleweed")
        {
            if (PlayerPrefs.GetInt("Score") >= tumbleCost && !tumbleBought)
            {
                PlayerPrefs.SetString("tumbleweed_pic", "yes");
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") - tumbleCost);

            }
        }

        updateItems();
    }
}
