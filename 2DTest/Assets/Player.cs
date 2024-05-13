using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Chacter
{
    public Transform bullet;
    public Transform firePos;
    Camera cam;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            Fire(mousePos);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.gameObject.transform.Translate(Vector2.up * Time.deltaTime * 5f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.gameObject.transform.Translate(Vector2.down * Time.deltaTime * 5f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.Translate(Vector2.left * Time.deltaTime * 5f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.Translate(Vector2.right * Time.deltaTime * 5f);
        }
    }

    void Fire(Vector2 pos)
    {
        Vector2 playerPos = this.transform.position;
        Vector2 dir = pos - playerPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        firePos.rotation = Quaternion.Euler(0, 0, angle);
        Transform b = Instantiate(bullet, firePos.position, firePos.rotation);
    }

}
