using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHurt : MonoBehaviour
{
    [SerializeField] private Image blood;
    [SerializeField] private Volume boxVolume;
    [SerializeField] private IngameUI ingameUI;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private CapsuleCollider monsterCollider;
    public int bloodValue = 100;
    private float fillAmount = 0f;
    private float timePassed = 0;
    ChannelMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        boxVolume.profile.TryGet<ChannelMixer>(out mixer);
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        fillAmount = (float)bloodValue / 100.0f;
        blood.fillAmount = fillAmount;

        if (bloodValue <= 0) {
            ingameUI.OnFailed();
        }
    }

    public void DecreaseHealth() {
        if (timePassed > 2f) {
            timePassed = 0;
            bloodValue -= 5;
            mixer.redOutRedIn.value = 200;
            // Physics.IgnoreCollision(playerCollider, monsterCollider, true);
            StartCoroutine(DelayRestore());
            // StartCoroutine(DelayRestoreCollision());
        }
    }

    IEnumerator DelayRestore() {
        yield return new WaitForSeconds(0.5f);
        mixer.redOutRedIn.value = 65;
    }

    IEnumerator DelayRestoreCollision() {
        yield return new WaitForSeconds(2f);
        Physics.IgnoreCollision(playerCollider, monsterCollider, false);
    }
}
