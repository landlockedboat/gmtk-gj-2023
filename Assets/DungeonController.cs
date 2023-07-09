using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    // lets create a tilemap
    // tiles with 0 are empty space, tiles with 1 are walls
    public int[][] tiles = new int[][]
        {
            new int[] { 0, 0, 0, 0, 0 },
            new int[] { 1, 1, 1, 1, 0 },
            new int[] { 0, 0, 0, 0, 0 },
            new int[] { 0, 1, 1, 1, 1 },
            new int[] { 0, 0, 0, 0, 9 }
        };

    public GameObject emptyTilePrefab;
    public GameObject wallTilePrefab;
    public GameObject finishTilePrefab;

    public GameController gameController;

    private void Start()
    {
        for (int y = 0; y < tiles.Length; y++)
        {
            for (int x = 0; x < tiles[0].Length; x++)
            {
                int tile = tiles[y][x];
                switch (tile)
                {
                    case 0:
                        var emptyTile = Instantiate(emptyTilePrefab, new Vector2(x, -y), Quaternion.identity, transform);
                        emptyTile.GetComponent<EmptyTileController>().gameController = gameController;
                        break;
                    case 1:
                        Instantiate(wallTilePrefab, new Vector2(x, -y), Quaternion.identity, transform);
                        break;
                    case 9:
                        Instantiate(finishTilePrefab, new Vector2(x, -y), Quaternion.identity, transform);
                        break;
                    default:
                        break;

                }

            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
