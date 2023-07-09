using UnityEngine;

public interface ILiving
{
    public float Life { get; set; }
}

public class HeroController : MonoBehaviour, ILiving
{
    // HERO
    public Vector2Int heroPosition;
    public Vector2Int[] heroPath;
    // Walking
    public int heroState = 1;

    public float heroStepDelay = 2f;
    float currentHeroStepDelay;
    public int currentHeroPathIndex = 0;

    public GameController gameController;

    public float currentlife;

    public float fightDamage = 1f;

    public float Life { get => currentlife; set => currentlife = value; }


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

    public void DisengageIfNoEnemiesClose()
    {
        foreach (var slime in gameController.slimeControllers)
        {
            // fighting
            if (slime.state == 2)
            {
                return;
            }
        }
        // back to walking makina
        heroState = 1;
    }

    // Update is called once per frame
    public void Tick()
    {
        if (currentlife <= 0)
        {
            // The hero is dead! Long live the hero!
            heroState = -1;
            gameController.gameState = GameState.Won;
        }

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
            // fight!
            case 2:
                // fight to the death!
                break;
            case -1:
                // ur dead bro lmao
                break;
            default:
                Debug.LogWarning("Uncontrolled hero state " + heroState);
                break;
        }
    }
}
