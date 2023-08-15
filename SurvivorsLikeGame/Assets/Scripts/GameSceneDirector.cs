using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameSceneDirector : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    public Vector2 TileMapStart;
    public Vector2 TileMapEnd;
    public Vector2 WorldStart;
    public Vector2 WorldEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform item in grid.GetComponentsInChildren<Transform>())
        {
            if(TileMapStart.x > item.position.x)
            {
                TileMapStart.x = item.position.x;
            }
            if(TileMapStart.y > item.position.y)
            {
                TileMapStart.y = item.position.y;
            }

            if(TileMapEnd.x < item.position.x)
            {
                TileMapEnd.x = item.position.x;
            }
            if(TileMapEnd.y < item.position.y)
            {
                TileMapEnd.y = item.position.y;
            }
        }

        float cameraSize = Camera.main.orthographicSize;
        float aspect = (float)Screen.width / (float)Screen.height;
        WorldStart = new Vector2(TileMapStart.x - cameraSize * aspect, TileMapStart.y - cameraSize);
        WorldEnd = new Vector2(TileMapEnd.x + cameraSize * aspect, TileMapEnd.y + cameraSize);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
