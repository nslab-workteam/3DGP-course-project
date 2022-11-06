using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGameMenu : MonoBehaviour
{

    public GameObject player;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
        // player.GetComponent<MouseLook>().isStart = true;
        player.GetComponentInChildren<Camera>().GetComponent<MouseLook>().isStart = true;
    }
}
