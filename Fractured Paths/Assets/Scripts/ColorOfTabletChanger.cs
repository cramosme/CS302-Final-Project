using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOfTabletChanger : MonoBehaviour
{

    public Material myMaterial;
    public Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.color = myColor;
    }
}
