using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCurrencyUI : MonoBehaviour
{
    Player player;
    [SerializeField] Text coinText;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnEnable()
    {
        player.currency.onChangeCoin += UpdateCoinUI;
        player.currency.InitializeCurrency();
    }
    private void OnDisable()
    {
        player.currency.onChangeCoin  -= UpdateCoinUI;
    }
    void UpdateCoinUI(int coin)
    {
        coinText.text = coin.ToString(); 
    }
}
