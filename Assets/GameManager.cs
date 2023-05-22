using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int coins;
    public Text coinsCollected;

    private void Update() {
        coinsCollected.text = coins.ToString();
    }

}
