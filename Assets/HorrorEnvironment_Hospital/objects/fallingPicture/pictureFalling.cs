using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pictureFalling : MonoBehaviour
{
    private GameObject cuteCatPic;
    public GameObject glass;
    public GameObject crackGlass;
    private GameObject picture;

    private bool count = false;
    public float timer, interval;

    // Start is called before the first frame update
    void Start()
    {
        cuteCatPic = GameObject.Find("cuteCatPic");
        glass = GameObject.Find("glass");
        crackGlass = GameObject.Find("glassFracture");
        picture = GameObject.Find("Picture Variant");
        crackGlass.SetActive(false);

        timer = 0f;
        interval = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.name == "Picture" || hit.collider.name == "glass" || hit.collider.name == "cuteCatPic")
                {
                    picture.GetComponent<Rigidbody>().useGravity = true;
                    glass.GetComponent<Rigidbody>().useGravity = true;
                    count = true;
                }
            }
        }

        if (count)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                crackGlass.GetComponent<AudioSource>().Play();
                glass.SetActive(false);
                crackGlass.SetActive(true);
                cuteCatPic.SetActive(false);
                timer = 0f;
                count = false;
            }
        }
    }
}
