using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CraftBuilding
{
    public TowerAttackType towerAttackType;
    public TowerPerception perception;
    public float towerAttackCooldown = 1f; // 공격 쿨타임
    public int damage = 10; // 타워의 공격력

    private float towerAttackTimer = 0f; // 공격 타이머

    void Start()
    {
        perception = GetComponentInChildren<TowerPerception>();
        perception.detectEnemyAct.AddListener(OnEnemyDetected);
        perception.lostEnemyAct.AddListener(OnEnemyLost);
    }

    void Update()
    {
        towerAttackTimer -= Time.deltaTime;

        if (towerAttackTimer <= 0f)
        {
            GameObject targetEnemy = FindClosestEnemy();

            if (targetEnemy != null)
            {
                Attack(targetEnemy);
                towerAttackTimer = towerAttackCooldown;
            }
        }
    }

    void OnEnemyDetected(GameObject enemy)
    {
        Enemy enemyHealth = enemy.GetComponent<Enemy>();
        if (enemyHealth != null)
        {
            enemyHealth.onEnemyDeath.AddListener(RemoveEnemyFromList);
        }
    }

    void OnEnemyLost(GameObject enemy)
    {
        RemoveEnemyFromList(enemy);
    }

    void RemoveEnemyFromList(GameObject enemy)
    {
        perception.GetDetectedEnemies().Remove(enemy);
    }

    // 가장 가까운 적을 찾는 함수
    GameObject FindClosestEnemy()
    {
        List<GameObject> enemies = perception.GetDetectedEnemies();
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                // Vector2.Distance를 사용하여 X, Y 거리만 계산
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;
    }

    // 적을 공격하는 함수
    void Attack(GameObject enemy)
    {
        // 적의 Health 컴포넌트를 가져와서 데미지를 줍니다.
        Enemy enemyHealth = enemy.GetComponent<Enemy>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    // 공격 범위를 시각적으로 보여주는 함수 (디버그용)
    void OnDrawGizmosSelected()
    {
        if (perception != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, perception.GetComponent<CircleCollider2D>().radius);
        }
    }
}
