using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEController : MonoBehaviour
{
    public GameObject qteBoard;
    public GameObject qteImage;
    public GameObject qtePointer;
    public Sprite[] qteTypes;
    public AudioClip[] sounds;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject progressBarMask;
    [SerializeField] private GameObject mission;
    Animator pointerAnimator;
    public bool qteShow = false;
    float timePassed = 0f;
    bool soundFlg = false;
    bool qteFlg = false;
    (float, float)[] checkSuccessRange = {
        (335.11f, 297.5f),
        (295.1f, 258.4f),
        (248.7f, 209.9f),
        (197.6f, 160.2f),
        (156.7f, 120f),
        (121.1f, 84.9f)
    };
    int qteSlot = 0;
    float startAngle = 30f;
    float endAngle = -300f;
    bool turnPointer = false;
    float totalTime = 0f;
    float limitTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        qteImage.SetActive(false);
        qtePointer.SetActive(false);
        qteBoard.SetActive(false);
        pointerAnimator = qtePointer.GetComponent<Animator>();
        // StartQTE(15, "剪開枕頭");
    }

    // Update is called once per frame
    void Update()
    {
        if (!qteShow) return;

        totalTime += Time.deltaTime;
        timePassed += Time.deltaTime;
        if (timePassed >= 0.5f) {
            int _tmp = Random.Range(1, 100);
            if (_tmp <= 20) {
                TriggerSoundOnce(ref soundFlg);
                TriggerQTEOnce(ref qteFlg);
            }
            timePassed = 0f;
        }

        if (turnPointer) {
            float acceptStartAngle = checkSuccessRange[qteSlot].Item1;
            float acceptEndAngle = checkSuccessRange[qteSlot].Item2;
            float nowAngle = qtePointer.GetComponent<RectTransform>().rotation.eulerAngles.z;
            Debug.Log(nowAngle);

            if (Input.GetKeyDown(KeyCode.Space)) {

                if (nowAngle < acceptStartAngle && nowAngle > acceptEndAngle) {
                    GetComponent<AudioSource>().PlayOneShot(sounds[1]);
                    pointerAnimator.speed = 0;
                    turnPointer = false;
                    StartCoroutine(EndOfQTE());
                } else {
                    GetComponent<AudioSource>().PlayOneShot(sounds[2]);
                    qteImage.GetComponent<Image>().color = Color.red;
                    pointerAnimator.speed = 0;
                    turnPointer = false;
                    totalTime -= 3f;
                    limitTime += 1f;
                    StartCoroutine(EndOfQTE());
                }
            }

            if (nowAngle < 61 && nowAngle > 31) {
                GetComponent<AudioSource>().PlayOneShot(sounds[2]);
                qteImage.GetComponent<Image>().color = Color.red;
                pointerAnimator.speed = 0;
                turnPointer = false;
                totalTime -= 3f;
                limitTime += 1f;
                StartCoroutine(EndOfQTE());
            }
        }

        float fillAmount = (float)totalTime / (float)limitTime;
        progressBarMask.GetComponent<Image>().fillAmount = fillAmount;

        if (totalTime >= limitTime) {
            if (!soundFlg) {
                qteShow = false;
                qteBoard.SetActive(false);
            }
        }
    }

    IEnumerator EndOfQTE() {
        yield return new WaitForSeconds(0.5f);
        qteImage.GetComponent<Image>().color = Color.white;
        qteImage.SetActive(false);
        qtePointer.SetActive(false);
        soundFlg = false;
        qteFlg = false;
    }

    public void StartQTE(int second, string missionText) {
        qteShow = true;
        qteBoard.SetActive(true);
        mission.GetComponent<Text>().text = missionText;
        limitTime = second;
    }

    void TriggerSoundOnce(ref bool flg) {
        if (!flg) {
            GetComponent<AudioSource>().PlayOneShot(sounds[0]);
            flg = true;
        }
    }

    void TriggerQTEOnce(ref bool flg) {
        if (!flg) {
            StartCoroutine(TriggerQTE());
            flg = true;
        }
    }

    IEnumerator TriggerQTE() {
        yield return new WaitForSeconds(1f);
        qteSlot = Random.Range(0, 5);
        turnPointer = true;
        qteImage.SetActive(true);
        qtePointer.SetActive(true);
        qteImage.GetComponent<Image>().sprite = qteTypes[qteSlot];
        qtePointer.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, startAngle);
        pointerAnimator.speed = 1;
        pointerAnimator.SetTrigger("qte");
    }
}
