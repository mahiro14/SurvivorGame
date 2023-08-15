using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rigidbody2d;
    Animator animator;
    float moveSpeed = 2;

    [SerializeField] GameSceneDirector sceneDirector;
    [SerializeField] Slider sliderHP;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        Vector2 dir = Vector2.zero;
        string trigger = "";

        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector2.up;
            trigger = "isUp";
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir -= Vector2.up;
            trigger = "isDown";
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector2.right;
            trigger = "isRight";
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir -= Vector2.right;
            trigger = "isLeft";
        }

        if (Vector2.zero == dir) return;

        rigidbody2d.position += dir.normalized * moveSpeed * Time.deltaTime;

        animator.SetTrigger(trigger);

        // if(rigidbody2d.position.x < sceneDirector.WorldStart.x)
        // {
        //     Vector2 pos = rigidbody2d.position;
        //     pos.x = sceneDirector.WorldStart.x;
        //     rigidbody2d.position = pos;
        // }
        // if(rigidbody2d.position.y < sceneDirector.WorldStart.y)
        // {
        //     Vector2 pos = rigidbody2d.position;
        //     pos.y = sceneDirector.WorldStart.y;
        //     rigidbody2d.position = pos;
        // }
        // if(sceneDirector.WorldEnd.x < rigidbody2d.position.x)
        // {
        //     Vector2 pos = rigidbody2d.position;
        //     pos.x = sceneDirector.WorldStart.x;
        //     rigidbody2d.position = pos;
        // }
        // if(sceneDirector.WorldEnd.y < rigidbody2d.position.y)
        // {
        //     Vector2 pos = rigidbody2d.position;
        //     pos.y = sceneDirector.WorldStart.y;
        //     rigidbody2d.position = pos;
        // }
    }
    // void moveCamera()
    // {
    //     Vector3 pos = Transform.position;
    //     pos.z = Camera.main.transform.position.z;

    //     if(pos.x < sceneDirector.TileMapStart.x)
    //     {
    //         pos.x = sceneDirector.TileMapStart.x;
    //     }
    //     if(pos.y < sceneDirector.TileMapStart.y)
    //     {
    //         pos.y = sceneDirector.TileMapStart.y;
    //     }

    //     if(sceneDirector.TileMapEnd.x < pos.x)
    //     {
    //         pos.x = sceneDirector.TileMapEnd.x;
    //     }
    //     if(sceneDirector.TileMapEnd.y < pos.y)
    //     {
    //         pos.y = sceneDirector.TileMapEnd.y;
    //     }

    //     Camera.main.transform.position = pos;
    // }

    // void moveSliderHP()
    // {
    //     Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
    // }
}
