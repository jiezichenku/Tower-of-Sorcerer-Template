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
    public LayerMask gatewayLayer;
    // Start is called before the first frame update
    void Start()
    {
        //Set Global varibles
        GlobalVariables.braver = gameObject;
        //Singleton pattern constructor
        repository = RepositoryOfItems.GetInstance();
        status = BraverStatus.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        braverMovement();
    }

    private void braverMovement()
    {
        moveDir = Vector2.zero;
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
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, dir, 1f, detectLayer);
        RaycastHit2D gatewayHit = Physics2D.Raycast(transform.position, dir, 1f, gatewayLayer);
        if (gatewayHit)
        {
            if (gatewayHit.distance > 0.5)
            {
                Gateway gt = gatewayHit.collider.GetComponent<Gateway>();
                GlobalVariables.currentEventName = gt.name;
                gt.onHitEvent();
            }
        }
        if (!raycastHit)
        {
            return true;
        }
        else
        {
            //Check the collider is obstacle, item, gateway, or wall
            if(raycastHit.collider.GetComponent<Obstacle>() != null)
            {
                Obstacle ob = raycastHit.collider.GetComponent<Obstacle>();
                GlobalVariables.currentEventName = ob.name;
                ob.onHitEvent();
            }
            if (raycastHit.collider.GetComponent<Item>() != null)
            {
                Item it = raycastHit.collider.GetComponent<Item>();
                GlobalVariables.currentEventName = it.name;
                it.onHitEvent();
                return true;
            }
            if (raycastHit.collider.GetComponent<Enemy>() != null)
            {
                Enemy e = raycastHit.collider.GetComponent<Enemy>();
                GlobalVariables.currentEventName = e.name;
                e.battle();
            }

            return false;
        }
    }
}
