using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleIfCanNextLevel : MonoBehaviour
{
    public bool show;
    public GameController gameController;
    // Start is called before the first frame update
    void Update()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var value = gameController.CanNextLevel();
            if (!show)
            {
                value = !value;
            }
            gameObject.transform.GetChild(i).gameObject.SetActive(value);
        }
    }
}

