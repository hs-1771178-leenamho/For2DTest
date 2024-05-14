using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chacter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void TakeDamage(Vector2 dir, float force)
    {
        StartCoroutine(DamaginEffect(dir, force));
    }

    IEnumerator DamaginEffect(Vector2 dir, float force)
    {
        spriteRenderer.color = Color.red;
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        if(rigid != null)
        {
            rigid.AddForce(dir * force, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.2f);
        if (rigid != null) rigid.velocity = Vector2.zero;
        spriteRenderer.color = Color.white;
    }
}
