using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryHelper : MonoBehaviour
{
    // Start is called before the first frame update
    //public Rigidbody2D rigixdbody; 
    public bool isFlying = false;
    public float throwAngle = 60;
    public float throwForce;
    private float flyingTime = 0;
    public float gravity = 10f;
    private float preY, preX;
    private float x0, y0;
    public float projectileSpeed;
    public float projectileNeedFlipping = 1; 
    public float idealForce = 15;
    public bool isPlane = false; 
    void Start()
    {
        //GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0.1f, 0); 
    }
    private void Awake()
    {
        //isFlying = false;
    }

    void Shoot()
    {
        float angle = Mathf.PI * throwAngle / 180;

        float forceY = Mathf.Sin(angle) * throwForce * 2;
        float forceX = Mathf.Cos(angle) * throwForce / 2;

        //float oldX = transform.rotation.x;
        //float oldY = transform.rotation.y;
        //float oldW = transform.rotation.w;
        //preX = oldX;
        //preY = oldY; 
        //transform.rotation.Set(oldX, oldY, angle, oldW); 
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, forceY));
        transform.eulerAngles = new Vector3(0, 0, throwAngle);
        x0 = transform.position.x;
        y0 = transform.position.y;
        flyingTime = 0;
        //flyingTime += Time.deltaTime; 
        isFlying = true;
        //GetComponent<Rigidbody2D>().
        projectileSpeed = throwForce / idealForce;
        if (projectileSpeed < 0.8f)
            projectileSpeed = 0.8f; 
        preX = preY = 0; 
        if (throwAngle > 90)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*projectileNeedFlipping, transform.localScale.y, transform.localScale.z);
    }
    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.PI * throwAngle / 180;
        //if (Input.GetKey(KeyCode.Space) && !isFlying)
        //{
        //    float forceY = Mathf.Sin(angle) * throwForce*2 ;
        //    float forceX = Mathf.Cos(angle) * throwForce/2;

        //    //float oldX = transform.rotation.x;
        //    //float oldY = transform.rotation.y;
        //    //float oldW = transform.rotation.w;
        //    //preX = oldX;
        //    //preY = oldY; 
        //    //transform.rotation.Set(oldX, oldY, angle, oldW); 
        //    //GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX, forceY));
        //    transform.eulerAngles = new Vector3(0, 0, throwAngle); 
        //    x0 = transform.position.x;
        //    y0 = transform.position.y;
        //    flyingTime = 0;
        //    //flyingTime += Time.deltaTime; 
        //    isFlying = true;
        //    //GetComponent<Rigidbody2D>().
        //    preX = preY = 0;
            //if (throwAngle > 90 && throwAngle < 180)
                //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);

        //}

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
            float lineAngle = System.Math.Abs(dy) < 0.01 ? 0 : Mathf.Atan(dy) * 180f / Mathf.PI;
            //lineAngle = Mathf.Abs(lineAngle);

            preY = y;
            transform.eulerAngles = new Vector3(0, 0, lineAngle);
            //Debug.Log("Line Angle: "+ lineAngle); 
            flyingTime += Time.deltaTime*projectileSpeed;

            if (y < y0-10)
            {
                isFlying = false;
                //Destroy(gameObject); 
                flyingTime = 0;
            }
        }




    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFlying = false;
        if (isPlane == true)
        {
            GameObject character = GameObject.Find("Character");
            character.transform.position = gameObject.transform.position;
            Debug.Log("trajectory debug");
            character.GetComponent<CharacterMovement>().unloadFly(); 
            this.gameObject.SetActive(false); 
        }

        this.gameObject.SetActive(false);
    }
}
