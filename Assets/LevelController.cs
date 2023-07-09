using UnityEngine;
using System.Linq;
using System;

public class LevelController : MonoBehaviour
{
    public TextAsset tilesFile;

    public int initialMoney = 10;

    // lets create a tilemap
    // tiles with 0 are empty space, tiles with 1 are walls
    public int[][] tiles;

    public Vector2Int initialHeroPos = new Vector2Int(0, 0);

    public Vector2Int[] heroPath = new Vector2Int[] {
            new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0),  new Vector2Int(3, 0), new Vector2Int(4, 0),
            new Vector2Int(4, 1),
            new Vector2Int(4, 2),  new Vector2Int(3, 2),  new Vector2Int(2, 2), new Vector2Int(1, 2), new Vector2Int(0, 2),
            new Vector2Int(0, 3),
            new Vector2Int(0, 4), new Vector2Int(1, 4), new Vector2Int(2, 4), new Vector2Int(3, 4), new Vector2Int(4, 4),
        };

    private void Awake()
    {

        var lines = tilesFile.text.Split("\n");
        tiles = new int[lines.Length][];
        for (int y = 0; y < lines.Length; y++)
        {
            var chars = lines[y].Split(",");
            tiles[y] = new int[chars.Length];
            for (int x = 0; x < chars.Length; x++)
            {
                tiles[y][x] = int.Parse(chars[x]);
            }
        }



    }
}
