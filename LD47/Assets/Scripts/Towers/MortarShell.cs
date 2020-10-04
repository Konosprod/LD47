using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShell : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float explosionRadius;

    public SpriteRenderer spriteRenderer;
    public Collider2D shellCollider;
    public new Rigidbody2D rigidbody2D;

    public ParticleSystem explosionParticleSystem;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] zombies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, LayerMask.GetMask("Zombie"));
        // Deal the damage
        if(zombies.Length > 0)
        {
            for (int i = 0; i < zombies.Length; i++)
            {
                //Debug.Log($"Distance to zombie : {zombies[i].Distance(shellCollider).distance} / Radius of explosion : {explosionRadius}");

                zombies[i].gameObject.GetComponent<Monster>().TakeDamage(damage / (explosionRadius / (explosionRadius + zombies[i].Distance(shellCollider).distance)));
            }
        }

        // Destroy after a delay to let the particles do their job
        explosionParticleSystem.gameObject.SetActive(true);
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.isKinematic = true;
        spriteRenderer.enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1f);
    }


    private void Update()
    {
        Vector2 v = rigidbody2D.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
