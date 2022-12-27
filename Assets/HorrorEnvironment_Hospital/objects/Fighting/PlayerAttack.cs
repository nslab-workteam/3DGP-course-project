using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public float force = 5;
    private float timePassed = 0;
    private float attkDelay = 0;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject player;
    public bool canAtk = false;
    // Start is called before the first frame update
    void Start()
    {
        arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAtk) return;
        attkDelay += Time.deltaTime;
        if (Input.GetMouseButton(1)) {
                timePassed += Time.deltaTime;
                if (timePassed >= 1f) {
                    force += 2.5f;
                    force = Mathf.Clamp(force, 5f, 10f);
                }
            }
        if (attkDelay >= 2f) {
            if (Input.GetMouseButtonUp(1)) {
                Vector3 playerRotation = player.transform.rotation.eulerAngles;
                playerRotation.y += 90f;
                GameObject arrow_ = Instantiate(arrow, player.transform.position, Quaternion.Euler(playerRotation));
                arrow_.SetActive(true);
                arrow_.transform.parent = player.transform;
                arrow_.transform.localPosition = arrow_.transform.localPosition + new Vector3(0.25f, 2f, 1f);
                arrow_.GetComponent<Rigidbody>().useGravity = true;
                arrow_.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * force * 2;
                arrow_.GetComponent<ArrowDirectionControl>().destroyAfterDelay = true;

                force = 5;
                timePassed = 0;
                attkDelay = 0;
            }
        }
        
    }
}
