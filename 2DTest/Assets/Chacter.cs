using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chacter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public void TakeDamage()
    {
        StartCoroutine(DamaginEffect());
    }

    IEnumerator DamaginEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
