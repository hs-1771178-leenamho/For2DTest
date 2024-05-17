using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    public LayerMask layerMask;
    public Transform target;
    public float damage = 0f;
    public GameObject bulletImg;
    bool readyToFire = false;

    // Update is called once per frame
    void Update()
    {
        if (target == null || !readyToFire) return;
        Vector2 dir = target.position - transform.position;
        float dist = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dist > 1.0f) this.transform.rotation = Quaternion.Euler(0, 0, angle);

        this.gameObject.transform.Translate(Vector2.right * Time.deltaTime * 9f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((1 << collision.gameObject.layer & layerMask) != 0)
        {
            Debug.Log(collision.gameObject.name + " hit!");
            Enemy character = collision.gameObject.GetComponent<Enemy>();
            character.TakeDamage(damage);
            Destroy(this.gameObject);

            
        }
    }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    public void SetDamage(float _damage)
    {
        this.damage = _damage;
    }

    public void SetRotation(Transform _target)
    {
        if (_target == null) return;
        Vector2 dir = _target.position - transform.position;
        float dist = dir.magnitude;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (dist > 1.0f) this.transform.rotation = Quaternion.Euler(0, 0, -angle);
        readyToFire = true;
    }
    
    public void LostTarget()
    {
        Destroy(this.gameObject);
    }

}
