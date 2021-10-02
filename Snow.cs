using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public float speed = 10f;

    float radian = 0;
    float perRandian = 0.03f;
    float radius = 0.2f;
    Vector3 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //radian += perRandian;
        //float dy = Mathf.Cos(radian) * radius;
        //transform.localPosition = oldPos + new Vector3(0, dy, 0);

        transform.Rotate(Vector3.up * speed);
    }
}
