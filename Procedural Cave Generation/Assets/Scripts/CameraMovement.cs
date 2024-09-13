using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float moveSpeed = 8f;
    private Vector3 oldPos;
    private Vector3 newPos;
    private Transform rot;
    private float rotateDegree = 2f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("a"))
        {
            oldPos = this.transform.position;
            newPos = oldPos;
            newPos.x = newPos.x - moveSpeed * Time.deltaTime;
            this.transform.position = newPos;
            oldPos = newPos;
        }
        else if (Input.GetKey("d"))
        {
            oldPos = this.transform.position;
            newPos = oldPos;
            newPos.x = newPos.x + moveSpeed * Time.deltaTime;
            this.transform.position = newPos;
            oldPos = newPos;
        }
        else if (Input.GetKey("w"))
        {
            oldPos = this.transform.position;
            newPos = oldPos;
            newPos.z = newPos.z + moveSpeed * Time.deltaTime;
            this.transform.position = newPos;
            oldPos = newPos;
        }
        else if (Input.GetKey("s"))
        {
            oldPos = this.transform.position;
            newPos = oldPos;
            newPos.z = newPos.z - moveSpeed * Time.deltaTime;
            this.transform.position = newPos;
            oldPos = newPos;
        }
        else if (Input.GetKey("up"))
        {
            oldPos = this.transform.position;
            newPos = oldPos;
            newPos.y = newPos.y + moveSpeed * Time.deltaTime;
            this.transform.position = newPos;
            oldPos = newPos;
        }
        else if (Input.GetKey("down"))
        {
            oldPos = this.transform.position;
            newPos = oldPos;
            newPos.y = newPos.y - moveSpeed * Time.deltaTime;
            this.transform.position = newPos;
            oldPos = newPos;
        }
        else if (Input.GetKey("left"))
        {
            rot = this.transform;
            rot.Rotate(new Vector3(0f,1f,0f), -rotateDegree);
        }
        else if (Input.GetKey("right"))
        {
            rot = this.transform;
            rot.Rotate(new Vector3(0f,1f,0f), rotateDegree);
        }
    }
}
