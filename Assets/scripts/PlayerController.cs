using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;

    void Update()
    {
        Vector3 paddlePosition = new Vector3(0.5f, 0.5f);
        paddlePosition.x = Mathf.Clamp(Input.mousePosition.x / Screen.width * 16, 1.25f, 14.75f);
        transform.position = paddlePosition;

        //if (Input.GetKey(KeyCode.LeftArrow))
        //    transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
        //if (Input.GetKey(KeyCode.RightArrow))
        //    transform.position += Vector3.right * MoveSpeed * Time.deltaTime;
    }
}
