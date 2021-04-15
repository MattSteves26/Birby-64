using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public float timeCounter = 0;

    public float speed;
    public float width;
    public float height;

    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        width = 4;
        height = 4;

        Vector3 startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = startPos.x + Mathf.Cos(timeCounter) * width;
        float y = startPos.y + Mathf.Sin(timeCounter * 2) * height / 2 ;
        float z = startPos.z;

        transform.position = new Vector3(x, y, z);
    }
}
