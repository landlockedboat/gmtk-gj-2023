using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTileController : MonoBehaviour
{
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameState == GameState.Placing)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (worldPosition.x > transform.position.x - 0.5 && worldPosition.x < transform.position.x + 0.5 &&
                worldPosition.y < transform.position.y + 0.5 && worldPosition.y > transform.position.y - 0.5)
            {
                GetComponent<SpriteRenderer>().color = Color.grey;
                if (Input.GetMouseButton(0))
                {
                    gameController.PlaceSlime(transform.position);
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }

        }
    }
}
