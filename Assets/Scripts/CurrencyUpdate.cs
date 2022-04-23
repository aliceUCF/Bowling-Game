using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUpdate : MonoBehaviour
{
    TextMeshProUGUI currencyCounter;

    private void Start()
    {
    }
    // Start is called before the first frame update
    void Awake()
    {
        updateCurrency();
    }

    public void updateCurrency()
    {
        currencyCounter = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        currencyCounter.text = PlayerPrefs.GetInt("Score").ToString();

    }

}
