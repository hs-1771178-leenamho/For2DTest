using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Chacter
{
    public Transform bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireBullet());
    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            Transform b = Instantiate(bullet, this.transform);
            b.SetParent(null);
            yield return new WaitForSeconds(2.5f);
        }
    }
}
