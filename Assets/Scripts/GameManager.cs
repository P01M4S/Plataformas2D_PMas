using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public InputActionAsset playerInput;
    public InputAction _pauseInput;
    public bool _isPaused = false;
    int _stars = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        _pauseInput = InputSystem.actions["Pause"];
    }

    public void AddStar()
    {
        _stars++;
        Debug.Log("Estrellazo" + _stars);
    }

    void Update()
    {
        if (_pauseInput.WasPressedThisFrame())
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (_isPaused)
        {
            Time.timeScale = 1;
            GUImanager.Instance.ChangeCanvasStatus(GUImanager.Instance._canvasPause, false);
            playerInput.FindActionMap("player").Enable();
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            GUImanager.Instance.ChangeCanvasStatus(GUImanager.Instance._canvasPause, true);
            playerInput.FindActionMap("player").Disable();
            _isPaused = true;
        }
    }

}
