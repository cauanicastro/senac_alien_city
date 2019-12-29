using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Gunfire", order = 1)]
public class Firearm : Weapon
{
    public float bulletForce = 20f;
    public GameObject bulletPrefab;
    public string bulletTrigger;

    public override void Attack(Transform firePoint)
    {
        (bulletPrefab.GetComponent<Bullet>()).animatorTrigger = this.bulletTrigger;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.name = this.bulletTrigger;
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
