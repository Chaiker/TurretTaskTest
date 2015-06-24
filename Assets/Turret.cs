using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    public float UpMaxAngle = 90f;
    public float DownMaxAngle = 30f;
    public Transform GunPivotTransform;
    public Transform EnemyTarget;
    public LayerMask EnemyLayer;

    Gun gun;

    Ray2D nearTargetRay;
    RaycastHit2D nearTargetHit;

    Quaternion targetRotation;
    float targetAngle;

	void Start ()
    {
        gun = GetComponentInChildren<Gun>();

        nearTargetRay = new Ray2D(transform.position, Vector2.right);
	}
	
	void Update ()
    {
        if (EnemyTarget != null)
        {
            RotateToTargetAngle(CalculateAngle());
        }

        nearTargetHit = Physics2D.Raycast(nearTargetRay.origin, nearTargetRay.direction, 30f, EnemyLayer);

        if (nearTargetHit.collider != null)
        {
            EnemyTarget = nearTargetHit.collider.transform;
        }
	}

    float CalculateAngle()
    {
        float angle = 0f;

        float distance = Vector2.Distance(gun.BulletPosition.position, EnemyTarget.position);
        float startSpeed = gun.BulletStartSpeed;
        float g = 9.81f;

        angle = Mathf.Asin(distance * g / Mathf.Pow(startSpeed, 2f)) / 2f;

        return angle * Mathf.Rad2Deg;
    }

    void RotateToTargetAngle(float angle)
    {
        //Debug.Log(angle);

        targetAngle = Mathf.MoveTowardsAngle(GunPivotTransform.eulerAngles.z, angle, 60f * Time.deltaTime);
        targetAngle = Mathf.Clamp(targetAngle, DownMaxAngle, UpMaxAngle);

        targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        GunPivotTransform.rotation = targetRotation;
    }
}
