// Names: Braeden Watkins and Chris Ramos

using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    [SerializeField] GameObject door;

    public Camera newcamera;

    List<GameObject> playerOrder = new List<GameObject>(); // Used for check and also to revert buttons if the player order is incorrect
    List<GameObject> correctOrder;

    // Start is called before the first frame update
    void Start()
    {
        correctOrder = new List<GameObject>() { tridentButton, clawButton, fButton, funkyLButton, triangleButton, diamondButton, splitLButton }; // Used to check if the playerOrder == correctOrder

    }

    void Update()
    {
        if (playerOrder.Count < 7)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = newcamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // objectHit = hit.transform.GameObject;
                    if (hit.collider.gameObject == tridentButton)
                    {
                        tridentButton.transform.position = new Vector3(tridentButton.transform.position.x, tridentButton.transform.position.y - .125f, tridentButton.transform.position.z);
                        playerOrder.Add(tridentButton);
                    }
                    if (hit.collider.gameObject == triangleButton)
                    {
                        triangleButton.transform.position = new Vector3(triangleButton.transform.position.x, triangleButton.transform.position.y - .125f, triangleButton.transform.position.z);
                        playerOrder.Add(triangleButton);
                    }
                    if (hit.collider.gameObject == splitLButton)
                    {
                        splitLButton.transform.position = new Vector3(splitLButton.transform.position.x, splitLButton.transform.position.y - .125f, splitLButton.transform.position.z);
                        playerOrder.Add(splitLButton);
                    }
                    if (hit.collider.gameObject == rButton)
                    {
                        rButton.transform.position = new Vector3(rButton.transform.position.x, rButton.transform.position.y - .125f, rButton.transform.position.z);
                        playerOrder.Add(rButton);
                    }
                    if (hit.collider.gameObject == iButton)
                    {
                        iButton.transform.position = new Vector3(iButton.transform.position.x, iButton.transform.position.y - .125f, iButton.transform.position.z);
                        playerOrder.Add(iButton);
                    }
                    if (hit.collider.gameObject == emptyButton)
                    {
                        emptyButton.transform.position = new Vector3(emptyButton.transform.position.x, emptyButton.transform.position.y - .125f, emptyButton.transform.position.z);
                        playerOrder.Add(emptyButton);
                    }
                    if (hit.collider.gameObject == glassButton)
                    {
                        glassButton.transform.position = new Vector3(glassButton.transform.position.x, glassButton.transform.position.y - .125f, glassButton.transform.position.z);
                        playerOrder.Add(glassButton);
                    }
                    if (hit.collider.gameObject == funkyLButton)
                    {
                        funkyLButton.transform.position = new Vector3(funkyLButton.transform.position.x, funkyLButton.transform.position.y - .125f, funkyLButton.transform.position.z);
                        playerOrder.Add(funkyLButton);
                    }
                    if (hit.collider.gameObject == fButton)
                    {
                        fButton.transform.position = new Vector3(fButton.transform.position.x, fButton.transform.position.y - .125f, fButton.transform.position.z);
                        playerOrder.Add(fButton);
                    }
                    if (hit.collider.gameObject == diamondButton)
                    {
                        diamondButton.transform.position = new Vector3(diamondButton.transform.position.x, diamondButton.transform.position.y - .125f, diamondButton.transform.position.z);
                        playerOrder.Add(diamondButton);
                    }
                    if (hit.collider.gameObject == bButton)
                    {
                        bButton.transform.position = new Vector3(bButton.transform.position.x, bButton.transform.position.y - .125f, bButton.transform.position.z);
                        playerOrder.Add(bButton);
                    }
                    if (hit.collider.gameObject == clawButton)
                    {
                        clawButton.transform.position = new Vector3(clawButton.transform.position.x, clawButton.transform.position.y - .125f, clawButton.transform.position.z);
                        playerOrder.Add(clawButton);
                    }
                }
            }
        }
        else if (playerOrder.Count == 7)
        {
            int count = 0;
            // PlayerOrder should be 6 at this point so we can compare
            //Debug.Log($"The correct order vector size is {correctOrder.Count}");
            for (int i = 0; i < correctOrder.Count; i++)
            {
                if (playerOrder[i] == correctOrder[i])
                {
                    count++;
                }
            }
            if (count == correctOrder.Count) // They all matched so unlock door
            {
                door.transform.position = new Vector3(door.transform.rotation.x, door.transform.rotation.y + 275, door.transform.rotation.z);
            }
            else
            {
                foreach (var butt in playerOrder)
                {
                    butt.transform.position = new Vector3(butt.transform.position.x, butt.transform.position.y + .125f, butt.transform.position.z);
                }
                playerOrder.Clear();
            }
        }
    }
}

// [SerializeField] GameObject tridentButton = buttons.transform.Find("TridentButton")?.[SerializeField] GameObject;
// tridentButton.GetComponent<Renderer>().material = red;
