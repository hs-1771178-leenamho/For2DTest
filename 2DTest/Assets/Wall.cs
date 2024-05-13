using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public LayerMask layerMask;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & layerMask) != 0)
        {
            Debug.Log(collision.gameObject.name + " hit!");
            Destroy(collision.gameObject);
        }
    }
}
