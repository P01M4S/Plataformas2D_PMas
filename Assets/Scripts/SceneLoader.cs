using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    public GameObject _loadingCanvas;
    public Image _loadBar;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        yield return null;
        _loadingCanvas.SetActive(true);
        SceneManager.LoadSceneAsync(sceneName);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        float fakeLoadPorcentage = 0;

        while (!asyncLoad.isDone)
        {
            //_loadBar.fillAmount = asyncLoad.progress;
            fakeLoadPorcentage += 0.01f;
            _loadBar.fillAmount = fakeLoadPorcentage;

            if (asyncLoad.progress >= 0.9f && fakeLoadPorcentage >= 0.99f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }
        _loadingCanvas.SetActive(false);
    }
    
}
