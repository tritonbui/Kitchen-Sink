using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject endPoint;
    public bool isReturning;
    private Vector3 posMove;
    private Vector3 negMove;
    public Vector3 moveSpeed;
    private Vector3 moveDir;
    private Vector3 moveScale;
    private Vector3 maxScale;
    private Rigidbody _rb;
    
    public void Start()
    {
        this._rb = this.GetComponent<Rigidbody>();
        MovementMath();
    }

    public void FixedUpdate()
    {
        Movement();
    }

    public void MovementMath()
    {
        startPoint.transform.position = this.transform.position;
        moveDir = endPoint.transform.position - startPoint.transform.position;
        //Debug.Log("moveDirBefore = " + moveDir);

        moveScale = moveDir;
        if (moveScale.x > moveScale.y && moveScale.x > moveScale.z)
        {
            if (moveScale.x != 0)
            {
                maxScale = new Vector3(1 / moveScale.x, 1 / moveScale.x, 1 / moveScale.x);
                if (moveScale.x < 0)
                {
                    maxScale = Vector3.Scale(maxScale, -Vector3.one);
                }
            }
            else
            {
                maxScale = new Vector3(moveScale.x, moveScale.x, moveScale.x);
            }
        }
        else if (moveScale.y > moveScale.x && moveScale.y > moveScale.z)
        {
            if (moveScale.y != 0)
            {
                maxScale = new Vector3(1 / moveScale.y, 1 / moveScale.y, 1 / moveScale.y);
                if (moveScale.y < 0)
                {
                    maxScale = Vector3.Scale(maxScale, -Vector3.one);
                }
            }
            else
            {
                maxScale = new Vector3(moveScale.y, moveScale.y, moveScale.y);
            }
        }
        else if (moveScale.z > moveScale.x && moveScale.z > moveScale.y)
        {
            if (moveScale.z != 0)
            {
                maxScale = new Vector3(1 / moveScale.z, 1 / moveScale.z, 1 / moveScale.z);
                if (moveScale.z < 0)
                {
                    maxScale = Vector3.Scale(maxScale, -Vector3.one);
                }
            }
            else
            {
                maxScale = new Vector3(moveScale.z, moveScale.z, moveScale.z);
            }
        }

        moveScale = Vector3.Scale(moveScale, maxScale);
        if (moveScale.x < 0)
        {
            moveScale.x *= -1;
        }

        if (moveScale.y < 0)
        {
            moveScale.y *= -1;
        }

        if (moveScale.z < 0)
        {
            moveScale.z *= -1;
        }
        
        if (moveDir.x != 0)
        {
            if (moveDir.x > 0)
            {
                moveDir.x = 1f;
            }
            else
            {
                moveDir.x = -1f;
            }
        }

        if (moveDir.y != 0)
        {
            if (moveDir.y > 0)
            {
                moveDir.y = 1f;
            }
            else
            {
                moveDir.y = -1f;
            }
        }

        if (moveDir.z != 0)
        {
            if (moveDir.z > 0)
            {
                moveDir.z = 1f;
            }
            else
            {
                moveDir.z = -1f;
            }
        }

        //Debug.Log("moveDirAfter = " + moveDir);

        moveDir = Vector3.Scale(moveDir, moveScale);
        
        posMove = Vector3.Scale(moveSpeed, moveDir);
        negMove = Vector3.Scale(-moveSpeed, moveDir);
        //Debug.Log("posMove = " + posMove);
        //Debug.Log("negMove = " + negMove);
    }

    public void Movement()
    {
        if (!isReturning)
        {
            this.transform.position += posMove * Time.deltaTime;
        }
        else if (isReturning)
        {
            this.transform.position += negMove * Time.deltaTime;
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.name == "startPoint")
        {
            isReturning = false;
        }

        if (col.name == "endPoint")
        {
            isReturning = true;
        }
    }
}
