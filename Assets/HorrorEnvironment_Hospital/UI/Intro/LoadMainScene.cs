using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] private GameObject onLoadHint;
    [SerializeField] private GameObject loadingGif;
    [SerializeField] private GameObject hint;
    AsyncOperation op;

    private void Start() {
        
    }
    private void Update() {

    }

    private void OnEnable() {
        onLoadHint.SetActive(true);
        hint.SetActive(false);
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad() {
        op = SceneManager.LoadSceneAsync("Hespital_EXAMPLE");
        op.allowSceneActivation = false;
        float progressValue;
        bool flg = false;
        while (!op.isDone) {
            if (op.progress < 0.9) {
                progressValue = op.progress;
            } else {
                progressValue = 1.0f;
            }
            if (progressValue >= 0.9f) {
                if (!flg) {
                    hint.SetActive(true);
                    loadingGif.SetActive(false);
                    flg = !flg;
                }
                if (Input.GetMouseButtonDown(0)) {
                    op.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
