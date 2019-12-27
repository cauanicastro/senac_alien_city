using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player == null)
        {
            GameObject effect = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(effect, 0.3f);
            Destroy(gameObject);
        }
    }
}
