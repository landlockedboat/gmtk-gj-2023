using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{
    private int[][] dungeonTiles;

    // HERO
    private Vector2Int heroPosition;
    private Vector2Int[] heroPath;
    // Walking
    private int heroState = 1;

    private float heroStepDelay = 2f;
    private float currentHeroStepDelay = 2f;
    private int currentHeroPathIndex = 0;
    // Playing
    private int gameState = 0;

    // SLIME
    private Vector2Int slimePosition = new Vector2Int(4, 2);
    // Idle
    private int slimeState = 0;
    private float slimeStepDelay = 1f;
    private float currentSlimeStepDelay = 1f;
    private Vector2Int nextSlimePosition;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        // Top left
        heroPosition = new Vector2Int(0, 0);

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

    private int FindHeroWithOffset(Vector2Int direction, Vector2Int startingPosition)
    {
        Vector2Int currentLookoutPosition = startingPosition + direction;

        // Just to prevent infinite loops (i am a bad coder)
        while (currentLookoutPosition.sqrMagnitude < 1000)
        {
            // Out of bounds
            if (currentLookoutPosition.y >= dungeonTiles.Length ||
                currentLookoutPosition.y < 0)
            {
                return -1;
            }
            if (currentLookoutPosition.x >= dungeonTiles[currentLookoutPosition.y].Length ||
                currentLookoutPosition.x < 0)
            {
                return -1;
            }

            // found a wall!
            // this blocks our vision, so no hero can be found in this direction
            if (dungeonTiles[currentLookoutPosition.y][currentLookoutPosition.x] == 1)
            {
                return -1;
            }

            if (heroPosition == currentLookoutPosition)
            {
                // we found the hero!
                return Mathf.RoundToInt(Vector2Int.Distance(currentLookoutPosition, startingPosition));
            }

            currentLookoutPosition += direction;
        }
        Debug.LogError("Max lookout position exceeded! " + currentLookoutPosition);
        return -1;
    }

    private void UpdateSlimeIdleState(Vector2Int direction)
    {
        int distanceFromHero = FindHeroWithOffset(direction, slimePosition);
        if (distanceFromHero == -1)
        {
            return;
        }

        nextSlimePosition = slimePosition + direction;
        slimeState = 1;
        Debug.Log("Hero found!");

        if (distanceFromHero <= 1)
        {
            // We're fighting!!
            slimeState = 2;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Main game loop
        // Return if hero has won
        if (gameState == 1)
        {
            return;
        }

        // hero movement
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
                Debug.Log("The hero has moved to (" + heroPosition.x + ", " + heroPosition.y + ")");
                if (currentHeroPathIndex == heroPath.Length - 1)
                {
                    // the hero has won
                    Debug.Log("The hero has won!");
                    gameState = 1;
                }
                break;
            default:
                Debug.LogWarning("Uncontrolled hero state " + heroState);
                break;
        }

        // slime management
        switch (slimeState)
        {
            // 0 = Idle
            case 0:
                // look for the hero
                // start with +x (right)
                UpdateSlimeIdleState(new Vector2Int(1, 0));
                // -x (left)
                UpdateSlimeIdleState(new Vector2Int(-1, 0));
                // +y (down)
                UpdateSlimeIdleState(new Vector2Int(0, 1));
                // -y (up)
                UpdateSlimeIdleState(new Vector2Int(0, -1));
                break;

            // 1 = moving
            case 1:
                if (currentSlimeStepDelay > 0)
                {
                    currentSlimeStepDelay -= Time.deltaTime;
                    break;
                }
                currentSlimeStepDelay = slimeStepDelay;
                ++currentHeroPathIndex;
                slimePosition = nextSlimePosition;
                Debug.Log("The slime has moved to (" + slimePosition.x + ", " + slimePosition.y + ")");
                // idle
                slimeState = 0;
                break;
            // 2 = fighting
            case 2:
                // the wee bastard dies on contact
                Debug.Log("The hero has defeated the slime!");
                // it dead bro
                slimeState = -1;
                break;
            case -1:
                // Dead!
                break;
            default:
                break;
        }

    }
}
