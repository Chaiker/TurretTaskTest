using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float enemySpeed = 3f; // targetSpeed
    public float Health = 3;
    Animator enemyAnimator;

    Rigidbody2D body;

	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        //body.AddForce(-Vector2.right * enemySpeed, ForceMode2D.Impulse);

        //body.velocity = Vector2.ClampMagnitude(body.velocity, enemySpeed);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        Health--;

        enemyAnimator.Play("Damaged");

        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
