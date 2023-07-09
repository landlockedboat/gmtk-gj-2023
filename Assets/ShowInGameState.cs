using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInGameState : MonoBehaviour
{
    public GameState gameState;
    public GameController gameController;
    
    void Update()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(gameController.gameState == gameState);
        }
    }
}
