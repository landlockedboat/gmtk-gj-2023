using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private Vector2 heroPosition;
    private int[][] dungeonTiles;
    private Vector2Int[] heroPath;
    // Walking
    private int heroState = 0;

    private float heroStepDelay = 2f;
    private float currentHeroStepDelay = 2f;
    private int currentHeroPathIndex = 0;
    // Playing
    private int gameState = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        // Top left
        heroPosition = new Vector2(0, 0);

        // lets create a tilemap
        // tiles with 0 are empty space, tiles with 1 are walls
        dungeonTiles = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0 },
            new int[] { 1, 1, 1, 1, 0 },
            new int[] { 0, 0, 0, 0, 0 },
            new int[] { 0, 1, 1, 1, 1 },
            new int[] { 0, 0, 0, 0, 9 }
        };

        // TODO implement pathfinding algorithm
        heroPath = new Vector2Int[] {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),  new Vector2Int(3, 0), new Vector2Int(4, 0),
            new Vector2Int(4, 1),
            new Vector2Int(4, 2),  new Vector2Int(3, 2),  new Vector2Int(2, 2), new Vector2Int(1, 2), new Vector2Int(0, 2),
            new Vector2Int(0, 3),
            new Vector2Int(0, 4), new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4),
        };

    }

    // Update is called once per frame
    void Update()
    {
        // Main game loop
        // Return if hero has won
        if(gameState == 1)
        {
            return;
        }

        // hero movement
        // 0 = moving
        switch (heroState)
        {
            case 0:
                if (currentHeroStepDelay > 0)
                {
                    currentHeroStepDelay -= Time.deltaTime;
                    break;
                }
                currentHeroStepDelay = heroStepDelay;
                ++currentHeroPathIndex;
                heroPosition = heroPath[currentHeroPathIndex];
                Debug.Log("The hero has moved to (" + heroPosition.x + ", " + heroPosition.y + ")");
                if (currentHeroPathIndex == heroPath.Length - 1)
                {
                    // the hero has won
                    Debug.Log("The hero has won!");
                    gameState = 1;
                }
                break;
            default:
                break;
        }

    }
}
