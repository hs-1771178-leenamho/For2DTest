using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Chacter
{
    public Transform Target;
    public float maxHp = 100f;
    private float curHp;

    // ���� �׾��� �� �˸� �̺�Ʈ
    public UnityEvent<GameObject> onEnemyDeath;

    void Start()
    {
        curHp = maxHp;
    }

    private void Update()
    {
        Vector2 dir = transform.position - Target.position;
        dir.Normalize();

        transform.Translate(-dir * 2f * Time.deltaTime);
    }

    public void TakeDamage(float _dmg)
    {
        curHp -= _dmg;
        StartCoroutine(DamaginEffect());
        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ���� �׾��� �� �ͷ����� �˸�
        onEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    IEnumerator DamaginEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
