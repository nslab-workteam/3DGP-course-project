using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMessageBehaviour : MonoBehaviour
{
    [SerializeField] private HoldingItem hold;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private ParticleSystem waterDropPS;
    [SerializeField] private MeshRenderer elevatorMsg;
    [SerializeField] private AudioSource pourSoundSource;
    public bool isMsgShow = false;
    // Start is called before the first frame update
    void Start()
    {
        elevatorMsg.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 3f)) {
                if (Input.GetMouseButtonDown(0)) {
                    if ((hit.collider.name == "ElevatorMessageWall" || hit.collider.tag == "WallMessage") && hold.holdingObject == (int)ObjectToPick.liquid &&
                        movement.isStart) {
                        waterDropPS.Play();
                        this.StartCoroutine(_ElevatorMessageShowUp());
                        pourSoundSource.Play();
                    }
                }
            }
    }

    IEnumerator _ElevatorMessageShowUp() {
        yield return new WaitForSeconds(3f);
        elevatorMsg.enabled = true;
        isMsgShow = true;
    }
}
