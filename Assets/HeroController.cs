using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    // HERO
    public Vector2Int heroPosition;
    public Vector2Int[] heroPath;
    // Walking
    int heroState = 1;

    public float heroStepDelay = 2f;
    float currentHeroStepDelay;
    public int currentHeroPathIndex = 0;

    public GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        currentHeroStepDelay = heroStepDelay;
        // TODO implement pathfinding algorithm

        UpdateHeroVisualPosition();
    }

    public void UpdateHeroVisualPosition()
    {
        transform.position = new Vector3(heroPosition.x, -heroPosition.y);
    }

    // Update is called once per frame
    public void Tick()
    {
        switch (heroState)
        {
            // 1 = moving
            case 1:
                if (currentHeroStepDelay > 0)
                {
                    currentHeroStepDelay -= Time.deltaTime;
                    break;
                }
                currentHeroStepDelay = heroStepDelay;
                ++currentHeroPathIndex;
                heroPosition = heroPath[currentHeroPathIndex];
                UpdateHeroVisualPosition();
                if (currentHeroPathIndex == heroPath.Length - 1)
                {
                    // the hero has won
                    Debug.Log("The hero has won!");
                    gameController.gameState = GameState.Lost;
                }
                break;
            default:
                Debug.LogWarning("Uncontrolled hero state " + heroState);
                break;
        }
    }
}
