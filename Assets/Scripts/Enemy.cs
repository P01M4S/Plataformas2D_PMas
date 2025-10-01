using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int direction = 1;
    public float speed = 2.5f;
    public Animator _animator;
    public AudioSource _audioSource;
    public Rigidbody2D _rigidBody;
    public BoxCollider2D _boxCollider;
    public GameManager _gameManager;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    void FixedUpdate()
    {
        _rigidBody.linearVelocity = new Vector2(direction * speed, _rigidBody.linearVelocity.y);
    }
    void OnBecameVisible()
    {
        direction = 1;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerControler playerScript = collision.gameObject.GetComponent<PlayerControler>();
            playerScript.Death();
        }
    }
}
