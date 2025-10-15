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
    public Text _loadingText;
    public string _sceneMuerte = "Death";
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
            Mathf.Clamp01(fakeLoadPorcentage);
            _loadBar.fillAmount = fakeLoadPorcentage;
            _loadingText.text = (fakeLoadPorcentage * 100).ToString("F0") + "%";

            if (asyncLoad.progress >= 0.9f && fakeLoadPorcentage >= 0.99f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return new WaitForSecondsRealtime(0.1f);
        }
        Time.timeScale = 1;
        GameManager.instance.playerInput.FindActionMap("Player").Enable();
        GameManager.instance._isPaused = false;

        _loadingCanvas.SetActive(false);
    }

}

