using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Buying, Playing, Lost, Placing, Won
}

public class GameController : MonoBehaviour
{
    public DungeonController dungeonController;
    public HeroController heroController;
    public List<SlimeController> slimeControllers = new List<SlimeController>();

    public int money = 10;

    public GameObject slimePrefab;

    // Playing
    public GameState gameState = GameState.Buying;

    public LevelController[] levels;

    public int currentLevel = 0;

    
    public void InitializeLevel()
    {
        dungeonController.currentLevel = levels[currentLevel];
        dungeonController.ResetTiles();
        heroController.heroState = 1;
        heroController.heroPosition = dungeonController.currentLevel.initialHeroPos;
        heroController.UpdateHeroVisualPosition();
        heroController.heroPath = dungeonController.currentLevel.heroPath;
        heroController.currentHeroPathIndex = 0;
        heroController.currentlife = dungeonController.currentLevel.initialHeroLife;
        money = dungeonController.currentLevel.initialMoney;

        foreach (var slime in slimeControllers)
        {
            Destroy(slime.gameObject);
        }
        slimeControllers = new List<SlimeController>();
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeLevel();
    }

    public bool CanNextLevel()
    {
        return currentLevel < levels.Length - 1;
    }

    public void LoadNextLevel()
    {
        ++currentLevel;
        InitializeLevel();
        BeginBuying();
    }

    public void ResetLevel()
    {
        InitializeLevel();
        BeginBuying();
    }


    public void BeginPlaying()
    {
        gameState = GameState.Playing;

    }

    public void BeginBuying()
    {
        gameState = GameState.Buying;

    }

    public void BeginPlacingSlime()
    {
        gameState = GameState.Placing;
    }

    public void PlaceSlime(Vector3 position)
    {
        gameState = GameState.Buying;
        var slimeController = Instantiate(slimePrefab, position, Quaternion.identity, transform).GetComponent<SlimeController>();
        slimeController.gameController = GetComponent<GameController>();
        slimeController.slimePosition = 
            new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(-position.y));
        slimeControllers.Add(slimeController);
        money -= slimeController.price;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState != GameState.Playing)
        {
            return;
        }

        heroController.Tick();
        foreach (var slime in slimeControllers)
        {
            slime.Tick();
        }


    }
}
