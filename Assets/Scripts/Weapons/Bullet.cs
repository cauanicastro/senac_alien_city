using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidBody;
    public string animatorTrigger;
    public float bulletMaxLife = 2f;

    private void Start()
    {
        this.animator = GetComponent<Animator>();
        this.rigidBody = GetComponent<Rigidbody2D>();
        animator.SetTrigger(animatorTrigger);
        Destroy(gameObject, bulletMaxLife); //used to remove an object if it never touches anything
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<string> ignoredTargets = new List<string>{ "Player", "Bullet" };
        if (!ignoredTargets.Contains(collision.gameObject.tag))
        {
            rigidBody.velocity = new Vector3(0, 0, 0);
            animator.SetTrigger("explosion_" + animatorTrigger);
            Destroy(gameObject, 0.3f);
        }
    }
}
