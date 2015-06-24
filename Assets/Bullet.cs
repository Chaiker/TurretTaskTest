using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameObject BlowParticles;

	void Start ()
    {
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Enemy target = col.gameObject.GetComponent<Enemy>();

        if(target != null)
        {
            target.TakeDamage();
        }

        BlowParticles.SetActive(true);
        BlowParticles.transform.parent = null;
        Destroy(BlowParticles, 2f);
        Destroy(gameObject);
    }
}
