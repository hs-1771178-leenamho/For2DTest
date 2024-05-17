using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : CraftBuilding
{
    public TowerAttackType towerAttackType;
    public TowerPerception perception;
    public float towerAttackCooldown = 1f; // ���� ��Ÿ��
    public float damage = 10f; // Ÿ���� ���ݷ�

    private float towerAttackTimer = 0f; // ���� Ÿ�̸�

    void OnEnable()
    {
        perception = GetComponentInChildren<TowerPerception>();
        perception.detectEnemyAct.AddListener(OnEnemyDetected);
        perception.lostEnemyAct.AddListener(OnEnemyLost);
    }

    void Update()
    {
        //if (this.buildingState != BuildingState.BuildComplete) return;
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
        Enemy targetEnemy = enemy.GetComponent<Enemy>();
        if (targetEnemy != null)
        {
            targetEnemy.onEnemyDeath.AddListener(RemoveEnemyFromList);
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
    // ������ �´°ǵ� ����ü�� �������� ������
    void Attack(GameObject enemy)
    {
        Enemy targetEnemy = enemy.GetComponent<Enemy>();

        if (targetEnemy != null)
        {
            //targetEnemy.TakeDamage(damage * GetEfficiency()); // ���� ������ ���¿� ���� �������� �޸� ��
            targetEnemy.TakeDamage(damage);
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
