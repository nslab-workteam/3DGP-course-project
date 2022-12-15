using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

class PaperStackState {
    public string name;
    public bool state;
}

public class PaperStackBehavior : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 3f)) {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log(this.name + ": " + hit.collider.name);
                if (hit.collider.name == "PaperStack") {
                    StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "gameprogress", "paperstack.json"));
                    PaperStackState state = new PaperStackState();
                    state.name = "paperstack";
                    state.state = true;
                    file.Write(JsonUtility.ToJson(state));
                    file.Close();
                    Debug.Log("Save file");
                    GameObject.Find("PaperStack").SetActive(false);
                }
                
            }
        }
    }
}
