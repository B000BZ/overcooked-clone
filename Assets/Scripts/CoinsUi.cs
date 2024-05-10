using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUi : MonoBehaviour
{
    public static CoinsUi instance;

    [SerializeField] private TextMeshProUGUI _coinsText;
    private int _totalCoinsEarned;


    private void Awake()
    {
        instance = this;
    }


    public void UpdateCoins(int _coinsEarned )
    {
            _totalCoinsEarned += _coinsEarned;
            _coinsText.text = _totalCoinsEarned.ToString();


    }
}
