using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject ShotParticlesPrefab;
    public GameObject BulletPrefab;
    public Transform BulletPosition;
    public Transform ShotPosition;
    public float BulletStartSpeed = 10f;
    public float ReloadTime = 1f;

    public int MaxAmmo = 50;

    public Text ShotsLabel;
    public Text AmmoLabel;

    bool reloaded = true;
    GameObject spawnedBullet;

    int ammo = 50;
    int shots = 0;

	void Start ()
    {
	    ammo  = MaxAmmo;
        UpdateLabels();
	}

    void UpdateLabels()
    {
        ShotsLabel.text = shots + "";
        AmmoLabel.text = ammo + "/" + MaxAmmo;
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            FireRequest();
        }
    }
	
    public void FireRequest()
    {
        StartCoroutine(fire());
    }

    IEnumerator fire()
    {
        if (reloaded && ammo > 0)
        {
            spawnedBullet = (GameObject)Instantiate(BulletPrefab, BulletPosition.position, BulletPosition.rotation);
            spawnedBullet.GetComponent<Rigidbody2D>().AddForce(spawnedBullet.transform.up * BulletStartSpeed, ForceMode2D.Impulse);

            GameObject shotParticles = (GameObject)Instantiate(ShotParticlesPrefab, BulletPosition.position, ShotPosition.rotation);
            Destroy(shotParticles, 2f);

            ammo--;
            shots++;
            UpdateLabels();

            reloaded = false;
            yield return new WaitForSeconds(ReloadTime); // Перезарядка между выстрелами
            reloaded = true;

            if(ammo == 0)
            {
                yield return new WaitForSeconds(ReloadTime); // Перезарядка снарядов в "патронтаже"
                ammo = MaxAmmo;
            }
        }

        yield return null;
    }
}
