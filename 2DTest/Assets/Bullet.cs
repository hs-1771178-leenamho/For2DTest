using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector2.right * Time.deltaTime * 9f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((1 << collision.gameObject.layer & layerMask) != 0)
        {
            Debug.Log(collision.gameObject.name + " hit!");
            Chacter character = collision.gameObject.GetComponent<Chacter>();
            character.TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
