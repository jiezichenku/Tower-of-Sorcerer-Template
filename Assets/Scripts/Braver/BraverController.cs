using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BraverController : MonoBehaviour
{
    //Repository attributes
    public RepositoryOfItems repository;
    public BraverStatus status;
    //Movement attributes
    Vector2 moveDir;
    public LayerMask detectLayer;
    // Start is called before the first frame update
    void Start()
    {
        //Singleton pattern constructor
        repository = RepositoryOfItems.GetInstance();
        status = BraverStatus.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        this.braverMovement();

    }

    private void braverMovement()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = Vector2.right;
            //print("right");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir = Vector2.left;
            //print("left");
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir = Vector2.up;
            //print("up");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir = Vector2.down;
            //print("down");
        }

        if (Time.frameCount % 5 == 0)
        {
            if (moveDir != Vector2.zero && moveable(moveDir))
            {
                transform.Translate(moveDir);
            }
        }
        moveDir = Vector2.zero;

    }
    bool moveable(Vector2 dir)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dir, 1f, detectLayer);
        if(!raycastHit2D)
        {
            return true;
        }
        else
        {
            //Check the collider is obstacle, item, or wall
            if(raycastHit2D.collider.GetComponent<Obstacle>() != null)
            {
                Obstacle ob = raycastHit2D.collider.GetComponent<Obstacle>();
                ob.onHitEvent();
            }
            if (raycastHit2D.collider.GetComponent<Item>() != null)
            {
                Item it = raycastHit2D.collider.GetComponent<Item>();
                it.onHitEvent();
                return true;
            }
            return false;
        }
    }
}
