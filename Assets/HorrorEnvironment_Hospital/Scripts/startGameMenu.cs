using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGameMenu : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onStartClicked() {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
