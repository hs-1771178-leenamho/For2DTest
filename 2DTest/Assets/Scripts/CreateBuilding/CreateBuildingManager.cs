using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBuildingManager : MonoBehaviour
{
    public Tilemap ground;
    public Camera cam;
    public LayerMask layerMask;
    public Transform Tower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            // ��� Ŭ�� �Ǿ��°�? -> layermask���ؼ� ������ �Ʒ� ����
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, layerMask);
            if (hit.collider != null)
            {
                Debug.Log("hit");
                var worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int tilemapPos = ground.WorldToCell(worldPos);
                Debug.Log("tile pos : " + tilemapPos);
                Vector3 cellToworldPos = ground.CellToWorld(tilemapPos);
                Debug.Log("cellTowrold pos : " + cellToworldPos);

                Instantiate(Tower, new Vector3(cellToworldPos.x + 0.5f, cellToworldPos.y + 0.5f, 0), Quaternion.identity);
            }
            
            
        }
    }

    
}
