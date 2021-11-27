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
    int dir;
    public LayerMask detectLayer;
    public LayerMask gatewayLayer;
    //Animation controller
    public Animator animator;
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
        braverMovement();
    }

    private void braverMovement()
    {
        moveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir = Vector2.right;
            dir = 1;
            animator.SetInteger("Direction", 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir = Vector2.left;
            dir = 2;
            animator.SetInteger("Direction", 2);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir = Vector2.up;
            dir = 3;
            animator.SetInteger("Direction", 3);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir = Vector2.down;
            dir = 4;
            animator.SetInteger("Direction", 4);
        }

        if (Time.frameCount % 5 == 0)
        {
            if (moveDir != Vector2.zero && moveable(moveDir))
            {
                transform.Translate(moveDir);
            }
            else
            {
                if (dir == 1)
                {
                    animator.Play("RightWalk", 0, 0);
                }
                if (dir == 2)
                {
                    animator.Play("LeftWalk", 0, 0);
                }
                if (dir == 3)
                {
                    animator.Play("BackwardWalk", 0, 0);
                }
                if (dir == 4)
                {
                    animator.Play("ForwardWalk", 0, 0);
                }
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
                return true;
            }
            return false;
        }
    }
}
