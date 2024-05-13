using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Chacter
{
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
            Transform b = Instantiate(bullet, pos.transform.position, transform.rotation);
            b.SetParent(null);
            yield return new WaitForSeconds(2.5f);
        }
    }
}
