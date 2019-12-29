using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float lifeMax = 5f;
    float currentLife;
    public List<Firearm> vulnerableTo;
    List<string> vulnerableToNames;

    private void Start()
    {
        this.currentLife = this.lifeMax;
        this.vulnerableToNames = (from weapon in vulnerableTo select weapon.bulletTrigger).ToList();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && vulnerableToNames.Contains(collision.gameObject.name))
        {
            Debug.Log("hittou");
            //TODO: reduce life, add dying animation, those things
            Destroy(gameObject, 0.15f);
        }
    }


}
