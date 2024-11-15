// Names: Braeden Watkins and Chris Ramos

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonClicker : MonoBehaviour
{
    [SerializeField] GameObject tridentButton;
    [SerializeField] GameObject triangleButton;
    [SerializeField] GameObject splitLButton;
    [SerializeField] GameObject rButton;
    [SerializeField] GameObject iButton;
    [SerializeField] GameObject emptyButton;
    [SerializeField] GameObject glassButton;
    [SerializeField] GameObject funkyLButton;
    [SerializeField] GameObject fButton;
    [SerializeField] GameObject diamondButton;
    [SerializeField] GameObject bButton;
    [SerializeField] GameObject clawButton;

    public Camera newcamera;

    // [SerializeField] GameObject buttons;
    // Start is called before the first frame update
    // void Start()
    // {
    //     buttonDown = false;
    // }

    void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            var ray = newcamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if( Physics.Raycast(ray, out hit) )
            {
                // objectHit = hit.transform.GameObject;
                if( hit.collider.gameObject == tridentButton )
                {
                    tridentButton.transform.position = new Vector3(tridentButton.transform.position.x, tridentButton.transform.position.y - 0.5f, tridentButton.transform.position.z);
                }
                if( hit.collider.gameObject == triangleButton )
                {
                    triangleButton.transform.position = new Vector3(triangleButton.transform.position.x, triangleButton.transform.position.y - 0.5f, triangleButton.transform.position.z);
                }
                if( hit.collider.gameObject == splitLButton )
                {
                    splitLButton.transform.position = new Vector3(splitLButton.transform.position.x, splitLButton.transform.position.y - 0.5f, splitLButton.transform.position.z);
                }
                if( hit.collider.gameObject == rButton )
                {
                    rButton.transform.position = new Vector3(rButton.transform.position.x, rButton.transform.position.y - 0.5f, rButton.transform.position.z);
                }
                if( hit.collider.gameObject == iButton )
                {
                    iButton.transform.position = new Vector3(iButton.transform.position.x, iButton.transform.position.y - 0.5f, iButton.transform.position.z);
                }
                if( hit.collider.gameObject == emptyButton )
                {
                    emptyButton.transform.position = new Vector3(emptyButton.transform.position.x, emptyButton.transform.position.y - 0.5f, emptyButton.transform.position.z);
                }
                if( hit.collider.gameObject == glassButton )
                {
                    glassButton.transform.position = new Vector3(glassButton.transform.position.x, glassButton.transform.position.y - 0.5f, glassButton.transform.position.z);
                }
                if( hit.collider.gameObject == funkyLButton )
                {
                    funkyLButton.transform.position = new Vector3(funkyLButton.transform.position.x, funkyLButton.transform.position.y - 0.5f, funkyLButton.transform.position.z);
                }
                if( hit.collider.gameObject == fButton )
                {
                    fButton.transform.position = new Vector3(fButton.transform.position.x, fButton.transform.position.y - 0.5f, fButton.transform.position.z);
                }
                if( hit.collider.gameObject == diamondButton )
                {
                    diamondButton.transform.position = new Vector3(diamondButton.transform.position.x, diamondButton.transform.position.y - 0.5f, diamondButton.transform.position.z);
                }
                if( hit.collider.gameObject == bButton )
                {
                    bButton.transform.position = new Vector3(bButton.transform.position.x, bButton.transform.position.y - 0.5f, bButton.transform.position.z);
                }
                if( hit.collider.gameObject == clawButton )
                {
                    clawButton.transform.position = new Vector3(clawButton.transform.position.x, clawButton.transform.position.y - 0.5f, clawButton.transform.position.z);
                }
            }
        }
    }
}

// [SerializeField] GameObject tridentButton = buttons.transform.Find("TridentButton")?.[SerializeField] GameObject;
// tridentButton.GetComponent<Renderer>().material = red;
