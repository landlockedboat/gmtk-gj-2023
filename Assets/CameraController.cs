using UnityEngine;

public class CameraController : MonoBehaviour
{
    public DungeonController dungeonController;

    void Start()
    {
        transform.position = new Vector3((dungeonController.tiles.Length -1) / 2, -(dungeonController.tiles[0].Length -1) / 2, -10);
    }
}
