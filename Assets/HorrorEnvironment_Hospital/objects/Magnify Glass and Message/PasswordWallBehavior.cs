using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordWallBehavior : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject passwordSpotLight;
    [SerializeField] private GameObject passwordCamera;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            if (Input.GetMouseButtonDown(0) && 
                hit.collider.name == "suitcasePwdPlane" &&
                player.GetComponent<HoldingItem>().holdingObject == (int)ObjectToPick.magnifier) {
                    //打開燈
                    passwordSpotLight.SetActive(true);
                    //切換相機
                    Camera.main.GetComponent<AudioListener>().enabled = false;
                    passwordCamera.SetActive(true);
                    passwordCamera.GetComponent<AudioListener>().enabled = true;
                    this.StartCoroutine(_ZoomOutPassword());
                }
        }
    }

    IEnumerator _ZoomOutPassword(){
        yield return new WaitForSeconds(3f);
        passwordSpotLight.SetActive(false);
        passwordCamera.GetComponent<AudioListener>().enabled = false;
        passwordCamera.SetActive(false);
        Camera.main.GetComponent<AudioListener>().enabled = true;
    }
}
