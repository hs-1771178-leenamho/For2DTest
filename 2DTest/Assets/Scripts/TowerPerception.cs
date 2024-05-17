using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class TowerPerception : MonoBehaviour
{
    public LayerMask enemyMask;
    public UnityEvent<GameObject> detectEnemyAct;
    public UnityEvent<GameObject> lostEnemyAct;
    private List<GameObject> detectedEnemies = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyMask ) != 0)
        {
            detectedEnemies.Add(other.gameObject);
            detectEnemyAct?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyMask) != 0)
        {
            detectedEnemies.Remove(other.gameObject);
            lostEnemyAct?.Invoke(other.gameObject);
        }
    }

    public List<GameObject> GetDetectedEnemies()
    {
        return detectedEnemies;
    }
}
