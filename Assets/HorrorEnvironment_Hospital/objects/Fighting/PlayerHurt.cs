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
    private Animator playerAni;
    [SerializeField] private AudioClip hurtSound;
    public int bloodValue = 100;
    private float fillAmount = 0f;
    private float timePassed = 0;
    ChannelMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        boxVolume.profile.TryGet<ChannelMixer>(out mixer);
        playerAni = GameObject.FindWithTag("Player").GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        fillAmount = (float)bloodValue / 100.0f;
        blood.fillAmount = fillAmount;

        if (bloodValue <= 0) {
            ingameUI.LockPlayer();
            ingameUI.OnFailed();
        }
    }

    public void DecreaseHealth() {
        if (playerAni.GetCurrentAnimatorStateInfo(0).IsName("Sprint")){
            Debug.Log("On Sprint");
            return;
        }
        bloodValue -= 5;
        mixer.redOutRedIn.value = 200;
        GetComponent<AudioSource>().PlayOneShot(hurtSound);
        StartCoroutine(DelayRestore());
    }

    IEnumerator DelayRestore() {
        yield return new WaitForSeconds(0.5f);
        mixer.redOutRedIn.value = 65;
    }
}
