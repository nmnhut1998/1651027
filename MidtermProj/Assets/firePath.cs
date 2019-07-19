using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firePath : MonoBehaviour
{
    public bool isFlying = false;
    public float throwAngle = 60;
    public float throwForce;
    private float flyingTime = 0;
    public float gravity = 10f;
    private float preY, preX;
    private float x0, y0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Shoot()
    {
        float angle = 180 - Mathf.PI * (180-throwAngle)/ 180;

        float forceY = Mathf.Sin(angle) * throwForce * 2;
        float forceX = Mathf.Cos(angle) * throwForce / 2;

        transform.eulerAngles = new Vector3(0, 0, (180-throwAngle));
        x0 = transform.position.x;
        y0 = transform.position.y;
        flyingTime = 0;
        //flyingTime += Time.deltaTime; 
        isFlying = true;
        //GetComponent<Rigidbody2D>().
        preX = preY = 0;
        if (throwAngle > 90)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        float angle =  Mathf.PI * (180 - throwAngle) / 180;

        if (isFlying)
        {
            float x = x0 + throwForce * flyingTime * Mathf.Cos(angle);
            float y = y0 + Mathf.Abs(throwForce) * flyingTime * Mathf.Sin(angle) - 0.5f * gravity * flyingTime * flyingTime;
            transform.position = new Vector3(x, y, 0.0f);

            //Debug.Log("postion: "+  x + " " + y + "flyingTime:" + flyingTime ); 
            float _tan = Mathf.Tan(angle);
            var _cos = Mathf.Cos(angle);
            var _sin = Mathf.Sin(angle);

            float dy = _tan - (x - x0) * gravity / (throwForce * throwForce * _cos * _cos);

            if (preY > y)
            {
                //dy =  (x - x0) * (1+ _tan * _tan) * gravity / (throwForce * throwForce);

            }
            float lineAngle = (System.Math.Abs(dy) < 0.01) ? 0 : Mathf.Atan(dy) * 180f / Mathf.PI;
            //lineAngle = Mathf.Abs(lineAngle);

            preY = y;
            //transform.eulerAngles = new Vector3(0, 0, lineAngle);
            //Debug.Log("Line Angle: "+ lineAngle); 
            flyingTime += Time.deltaTime * 2;

            //if (y < y0)
            //{
            //    isFlying = false;
            //    Destroy(gameObject);
            //    flyingTime = 0;
            //}
        }
    }
}
