using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public GameObject emptyTilePrefab;
    public GameObject wallTilePrefab;
    public GameObject finishTilePrefab;

    public LevelController currentLevel;
    public GameController gameController;

    List<GameObject> spawnedTiles = new List<GameObject>();

    void SpawnTiles()
    {
        for (int y = 0; y < currentLevel.tiles.Length; y++)
        {
            for (int x = 0; x < currentLevel.tiles[0].Length; x++)
            {
                int tile = currentLevel.tiles[y][x];
                switch (tile)
                {
                    case 0:
                        var emptyTile = Instantiate(emptyTilePrefab, new Vector2(x, -y), Quaternion.identity, transform);
                        emptyTile.GetComponent<EmptyTileController>().gameController = gameController;
                        spawnedTiles.Add(emptyTile);
                        break;
                    case 1:
                        spawnedTiles.Add(Instantiate(wallTilePrefab, new Vector2(x, -y), Quaternion.identity, transform));
                        break;
                    case 9:
                        spawnedTiles.Add(Instantiate(finishTilePrefab, new Vector2(x, -y), Quaternion.identity, transform));
                        break;
                    default:
                        break;

                }

            }
        }
    }

    private void Start()
    {
        SpawnTiles();
    }

    public void ResetTiles()
    {
        foreach (var tile in spawnedTiles)
        {
            Destroy(tile);
        }
        spawnedTiles = new List<GameObject>();
        SpawnTiles();
    }

}
