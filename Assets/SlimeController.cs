using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    // SLIME
    public Vector2Int slimePosition = new Vector2Int(4, 2);
    // Idle
    int state = 0;
    public float stepDelay = 1f;
    float currentStepDelay = 1f;
    Vector2Int nextPosition;

    public int price = 5;

    DungeonController dungeonController;
    public GameController gameController;

    private int FindHeroWithOffset(Vector2Int direction, Vector2Int startingPosition)
    {
        Vector2Int currentLookoutPosition = startingPosition + direction;
        dungeonController = gameController.dungeonController;

        // Just to prevent infinite loops (i am a bad coder)
        while (currentLookoutPosition.sqrMagnitude < 1000)
        {
            // Out of bounds
            if (currentLookoutPosition.y >= dungeonController.currentLevel.tiles.Length ||
                currentLookoutPosition.y < 0)
            {
                return -1;
            }
            if (currentLookoutPosition.x >= dungeonController.currentLevel.tiles[currentLookoutPosition.y].Length ||
                currentLookoutPosition.x < 0)
            {
                return -1;
            }

            // found a wall!
            // this blocks our vision, so no hero can be found in this direction
            if (dungeonController.currentLevel.tiles[currentLookoutPosition.y][currentLookoutPosition.x] == 1)
            {
                return -1;
            }

            if (gameController.heroController.heroPosition == currentLookoutPosition)
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

        nextPosition = slimePosition + direction;
        state = 1;
        Debug.Log("Hero found!");

        if (distanceFromHero <= 1)
        {
            // We're fighting!!
            state = 2;
        }
    }

    void Start()
    {
        currentStepDelay = stepDelay;
        UpdateSlimeVisualPosition();
    }

    public void UpdateSlimeVisualPosition()
    {
        transform.position = new Vector3(slimePosition.x, -slimePosition.y);
    }


    // Update is called once per frame
    public void Tick()
    {
        // Main game loop
        // Return if hero has won
        if (gameController.gameState == GameState.Lost)
        {
            return;
        }

        // slime management
        switch (state)
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
                if (currentStepDelay > 0)
                {
                    currentStepDelay -= Time.deltaTime;
                    break;
                }
                currentStepDelay = stepDelay;
                slimePosition = nextPosition;
                Debug.Log("The slime has moved to (" + slimePosition.x + ", " + slimePosition.y + ")");
                UpdateSlimeVisualPosition();
                // idle
                state = 0;
                break;
            // 2 = fighting
            case 2:
                // the wee bastard dies on contact
                Debug.Log("The hero has defeated the slime!");
                // it dead bro
                state = -1;
                break;
            case -1:
                // Dead!
                break;
            default:
                break;
        }
    }
}
