using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Fist")
        {
            Vector3 fistVelocity = collision.transform.GetComponent<Rigidbody2D>().velocity;
            int damage = (int)(GetDistance(fistVelocity));
            Debug.Log("damage is " + damage);

            Health health = transform.GetComponentInParent<Health>();
            health.TakeDamage(damage);
        }
    }

    private float GetDistance(Vector3 target)
    {
        return Mathf.Sqrt(target.x * target.x + target.y * target.y + target.z * target.z);
    }
}
