using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarController : MonoBehaviour
{
    // Needs to implement ILiving lmao
    public GameObject target;
    public TMPro.TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = target.GetComponent<ILiving>().Life + "L";
    }
}
