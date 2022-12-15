using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LittleGirlBehavior : MonoBehaviour
{
    [SerializeField] private GameObject dialogManager;
    
    bool startDialogFlg = false;
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
            if (Input.GetMouseButtonDown(0) && hit.collider.name == "Little Girl") {
                // read file, check whether paper stack picked up
                string path = Path.Combine(Application.streamingAssetsPath, "gameprogress", "paperstack.json");
                bool fileExists = File.Exists(path);
                if (fileExists) {
                    StreamReader file = new StreamReader(path);
                    PaperStackState state = JsonUtility.FromJson<PaperStackState>(file.ReadToEnd());
                    if (state.state == true) {
                        StartDialogOnce(ref startDialogFlg);
                    }
                }
            }
        }
    }

    void StartDialogOnce(ref bool flg) {
        if (!flg) {
            dialogManager.GetComponent<UsageCase>().StartDialog();
            flg = true;
        }
    }
}
