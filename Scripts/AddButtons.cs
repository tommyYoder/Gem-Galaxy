using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for first level.
public class AddButtons : MonoBehaviour {
    [SerializeField]
    public Transform gamePanel;

    [SerializeField]
    public GameObject btn;

    // Adds buttons by instantiating one button, sets each buttons name, and deparents the buttons from the game panel.
    void Awake()
    {
        for(int i = 0; i < 8; i++)
        {
           
             GameObject button = Instantiate (btn);
             button.name = "" +i;
             button.transform.SetParent(gamePanel, false);
        }

    }
		
}

