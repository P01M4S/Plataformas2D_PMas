using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _fxSource;
    public AudioClip _starFX;
    public AudioClip _coinFX;
    public static AudioManager instance { get; private set; }
    public AudioClip playBGM;

    void Awake()
    {
        if (instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void ChangeBGM(AudioClip bgmClip)
    {
        _bgmSource.clip = bgmClip;
        _bgmSource.Play();
    }

    public void Sound(AudioClip clip)
    {
        _fxSource.PlayOneShot(clip);
    }
    
}
