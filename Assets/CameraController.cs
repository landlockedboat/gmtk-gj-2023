using UnityEngine;

public class CameraController : MonoBehaviour
{
    public DungeonController dungeonController;

    void Start()
    {
        transform.position = new Vector3((dungeonController.currentLevel.tiles.Length -1) / 2, -(dungeonController.currentLevel.tiles[0].Length -1) / 2, -10);
    }
}
