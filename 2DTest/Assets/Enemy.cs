using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Chacter
{
    public Transform target;
    public Transform bullet;
    public Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireBullet());
    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            float fireTime = Random.Range(1f, 2.5f);
            Vector2 dir = pos.position - target.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            pos.rotation = Quaternion.Euler(0, 0, angle + 180f);
            Transform b = Instantiate(bullet, pos.transform.position, pos.transform.rotation);
            b.SetParent(null);
            yield return new WaitForSeconds(fireTime);
        }
    }
}
