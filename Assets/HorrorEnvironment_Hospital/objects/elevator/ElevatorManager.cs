using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonB1;
    [SerializeField] private ElevatorMessageBehaviour elevatorMsg;
    // Start is called before the first frame update
    void Start()
    {
        buttonB1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (elevatorMsg.isMsgShow) {
            buttonB1.SetActive(true);
        }
    }
}
