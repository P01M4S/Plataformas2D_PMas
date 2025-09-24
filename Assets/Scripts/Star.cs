using UnityEngine;

public class Star : MonoBehaviour
{
    //GameManager _gameManager;
    [SerializeField] private AudioClip _starFX;
    void Awake()
    {
        //_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Interaction()
    {
        //_gameManager.AddStar();
        GameManager.instance.AddStar();
        AudioManager.instance.Sound(AudioManager.instance._starFX);
        Destroy(gameObject);
    }

}
