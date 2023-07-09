using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Buying, Playing, Lost, Placing
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

    // Start is called before the first frame update
    void Start()
    {
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
