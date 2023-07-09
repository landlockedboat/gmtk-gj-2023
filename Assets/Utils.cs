using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public Vector3 GetWorldPosition (Vector2Int position)
    {
        return new Vector3(position.x, -position.y);
    }
}
