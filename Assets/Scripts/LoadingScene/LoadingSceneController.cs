using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    public enum E_PHASE
    {
        CREATE_DATA,
        CREATE_TILES,
        LOAD_MAP_DATA,
    }

    [SerializeField]
    Text TextLoading  = null;

    [SerializeField]
    Text TextProgress = null;

    void Start()
    {
        if (!TextLoading || !TextProgress)
            return;

        StartCoroutine(IE_LoadScene());
    }

    IEnumerator IE_LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("WorldScene");
        async.allowSceneActivation = false;

        bool isDone = false;

        while (!isDone)
        {
            int nProgress = (int)(Mathf.Clamp01(async.progress / 0.9f) * 100);
            TextProgress.text = string.Format("Loading : {0}%", nProgress);

            yield return null;

            if (async.progress >= 0.9f)
                isDone = true;
        }

        async.allowSceneActivation = true;
    }
}
