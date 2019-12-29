using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    public Firearm weapon;
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        this.posOffset = transform.position;
    }

    void Update()
    {
        this.tempPos = this.posOffset;
        this.tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * this.floatFrequency) * this.floatAmplitude;

        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.tag == "Player")
            {
                pickupWeapon(collision.gameObject.GetComponent<PlayerController>());
            }
        }
        catch
        {
            //Do nothing
        }
    }

    void pickupWeapon(PlayerController player)
    {
        if (!player.inventory.Contains(this.weapon))
            player.inventory.Add(this.weapon);
        player.currentWeapon = this.weapon;
        this.weapon.setCurrentWeapon();

        Destroy(gameObject);
    }
}
