using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    // HERO
    public Vector2Int heroPosition;
    Vector2Int[] heroPath;
    // Walking
    int heroState = 1;

    public float heroStepDelay = 2f;
    float currentHeroStepDelay;
    int currentHeroPathIndex = 0;

    public GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        currentHeroStepDelay = heroStepDelay;
        // TODO implement pathfinding algorithm
        heroPath = new Vector2Int[] {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),  new Vector2Int(3, 0), new Vector2Int(4, 0),
            new Vector2Int(4, 1),
            new Vector2Int(4, 2),  new Vector2Int(3, 2),  new Vector2Int(2, 2), new Vector2Int(1, 2), new Vector2Int(0, 2),
            new Vector2Int(0, 3),
            new Vector2Int(0, 4), new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4),
        };

        UpdateHeroVisualPosition();
    }

    void UpdateHeroVisualPosition()
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
