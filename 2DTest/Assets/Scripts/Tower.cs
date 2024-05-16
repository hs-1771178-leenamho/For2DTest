using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CraftBuilding
{
    public TowerAttackType towerAttackType;
    public TowerPerception perception;
    public float towerAttackCooldown = 1f; // ���� ��Ÿ��
    public int damage = 10; // Ÿ���� ���ݷ�

    private float towerAttackTimer = 0f; // ���� Ÿ�̸�

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

    // ���� ����� ���� ã�� �Լ�
    GameObject FindClosestEnemy()
    {
        List<GameObject> enemies = perception.GetDetectedEnemies();
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                // Vector2.Distance�� ����Ͽ� X, Y �Ÿ��� ���
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

    // ���� �����ϴ� �Լ�
    void Attack(GameObject enemy)
    {
        // ���� Health ������Ʈ�� �����ͼ� �������� �ݴϴ�.
        Enemy enemyHealth = enemy.GetComponent<Enemy>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    // ���� ������ �ð������� �����ִ� �Լ� (����׿�)
    void OnDrawGizmosSelected()
    {
        if (perception != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, perception.GetComponent<CircleCollider2D>().radius);
        }
    }
}
