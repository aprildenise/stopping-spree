using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float projectileSpeed;

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.right * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Destructible>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
