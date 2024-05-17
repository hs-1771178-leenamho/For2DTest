using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Tower : CraftBuilding
{
    public TowerAttackType towerAttackType;
    public TowerPerception perception;
    public float towerAttackCooldown = 1f; // 공격 쿨타임
    public float damage = 10f; // 타워의 공격력
    public Transform bullet;
    public Transform attackPoint;
    public Transform header;
    private float towerAttackTimer = 0f; // 공격 타이머

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
                Vector2 dir = targetEnemy.transform.position - attackPoint.transform.position;
                float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                float currentAngle = header.rotation.eulerAngles.z;

                // Convert angles to range [-180, 180]
                targetAngle = (targetAngle + 360f) % 360f;
                currentAngle = (currentAngle + 360f) % 360f;
                if (targetAngle > 180f) targetAngle -= 360f;
                if (currentAngle > 180f) currentAngle -= 360f;

                // Clamp targetAngle to the desired range [-60, 60] based on currentAngle
                float clampedAngle = Mathf.Clamp(targetAngle, -60f, 60f);

                // Smoothly rotate towards the clamped angle
                float newAngle = Mathf.LerpAngle(currentAngle, clampedAngle, Time.deltaTime * 5f); // Adjust 5f to control rotation speed
                header.rotation = Quaternion.Euler(0, 0, newAngle);

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
    // 무조건 맞는건데 투사체가 보였으면 좋겠음
    void Attack(GameObject enemy)
    {
        Enemy targetEnemy = enemy.GetComponent<Enemy>();

        if (targetEnemy != null && bullet != null)
        {
            //targetEnemy.TakeDamage(damage * GetEfficiency()); // 현재 내구도 상태에 따라 데미지를 달리 줌
            //targetEnemy.TakeDamage(damage);
            var towerBullet = Instantiate(bullet, attackPoint.transform.position, Quaternion.identity);
            towerBullet.transform.SetParent(null);
            towerBullet.GetComponent<Bullet>().SetTarget(enemy.transform);
            towerBullet.GetComponent<Bullet>().SetDamage(damage);
            towerBullet.GetComponent<Bullet>().SetRotation(enemy.transform);

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
