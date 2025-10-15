using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int direction = 1;
    public float speed = 2.5f;
    public Animator _animator;
    public Rigidbody2D _rigidBody;
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _rigidBody.linearVelocity = new Vector2(direction * speed, _rigidBody.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            direction *= -1;
        }
        if (collision.gameObject.layer == 8)
        {
            direction *= -1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControler playerScript = collision.gameObject.GetComponent<PlayerControler>();
            playerScript.TakeDamage(2);
            direction *= -1;
        }
    }

    internal static void Destroy()
    {
        throw new NotImplementedException();
    }
}
