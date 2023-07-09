using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceSlimeButtonUIController : MonoBehaviour
{
    public SlimeController slimeControllerPrefab;
    public GameController gameController;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Place Slime (" + slimeControllerPrefab.price + "M)";
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Button>().interactable = gameController.money >= slimeControllerPrefab.price;
    }
}
