using System;
using UnityEngine;
using UnityEngine.UI;

public class GUImanager : MonoBehaviour
{
    public static GUImanager Instance;
    public GameObject _canvasPause;
    public Image _healthBar;

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
    }

    public void ChangeCanvasStatus(GameObject canvas, bool status)
    {
        canvas.SetActive(status);
    }

    public void Resume()
    {
        GameManager.instance.Pause();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void ChangeScene(String sceneName)
    {
        SceneLoader.Instance.ChangeScene(sceneName);
    }
}
