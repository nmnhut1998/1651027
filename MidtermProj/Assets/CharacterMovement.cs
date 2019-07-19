using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP = 100.0f;
    public float maxHP = 100.0f;
    public bool isSliding = false;
    public bool isJumping = false;
    public bool isUsingPlane;

    public GameObject planeSprite;

    public Rigidbody2D rigidBody2D;
    public float thrustForce = 1000000f;
    public bool isGrounded = true;
    public float shootWaitingTime = 3;


    public float jumpAngle, jumpForce;

    public Animator animator;
    private GameObject iceBolt;
    public GameObject aimingArrow;
    public GameObject magicOrb;
    public GameObject shootHelper;
    public int direction = 1;

    public GameObject textShootIt;

    public GameObject projectile;
    public float shootAngle;
    public float shootForce;

    private float preX;
    public float hpLostOnHitLaser;
    //private float spaceHoldTime; 

    void Start()
    {
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) 
        //animator.SetFloat("characterSpeed", 2.0f);
    }

    private void FixedUpdate()
    {
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    animator.SetFloat("characterSpeed", 2.0f);
        //}
        //else animator.SetFloat("characterSpeed", 0.0f); 


    }
    // Update is called once per frame
    private void Awake()
    {

        loseHP(0);
        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    animator.SetFloat("characterSpeed", 2.0f);

        //}
        //else animator.SetFloat("characterSpeed", 0.0f);
    }

    void Update()
    {
        //float mmmmm = 0;
        //animator.SetFloat("characterSpeed", 0.0f);
        shootWaitingTime -= Time.deltaTime;
        if (shootWaitingTime >= 0)
        {
            magicOrb.SetActive(true);
            textShootIt.SetActive(true);
            shootHelper.SetActive(true);
        }
        else
        {
            magicOrb.SetActive(false);
            shootHelper.SetActive(false);
            textShootIt.SetActive(false);
            if (shootWaitingTime < -5)
                shootWaitingTime = 5.0f;
        }
        //if (Input.GetKey(KeyCode.UpArrow) && isGrounded && !aimingArrow.activeSelf)
        //{
        //    //transform.Translate(Vector2.up*3);
        //    float forceY = -Mathf.Sin(jumpAngle) * jumpForce * 2;
        //    float forceX = -direction*Mathf.Cos(jumpAngle) * jumpForce / 2;
        //    Debug.Log(forceX + " " + forceY); 
        //    rigidBody2D.AddForce(new Vector2(forceX, forceY));
        //    isGrounded = false;
        //    isJumping = true;
        //    Debug.Log("jumping!"); 

        //}
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            direction = -1;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);

            float forceX = direction * jumpForce * 3;
            float forceY = 0;
            rigidBody2D.AddForce(new Vector2(forceX, forceY));

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            direction = 1;
            float forceX = direction * jumpForce * 3;
            float forceY = 0;
            rigidBody2D.AddForce(new Vector2(forceX, forceY));
        }



        //else llllll = 0;
        var deltaX = Mathf.Abs(transform.position.x - preX);
        preX = transform.position.x;
        animator.SetFloat("characterSpeed", deltaX);

        //animator.SetBool("isSliding", isSliding);
        animator.SetBool("isJumping", isJumping);
        //animator.SetFloat("HP", HP);
    }
    public void CharacterShoot()
    {
        if (isUsingPlane)
            characterFly();
        else
        {

            iceBolt = Object.Instantiate(projectile);
            var orb = transform.GetChild(1);
            iceBolt.transform.position = new Vector3(orb.position.x + 0.05f, orb.transform.position.y + 0.2f, orb.position.z);
            //iceBolt.transform.position = new Vector3(transform.position.x, transform.position.y - orb.transform.position.y, transform.position.y);

            TrajectoryHelper icePath = iceBolt.GetComponent<TrajectoryHelper>();
            icePath.throwAngle = shootAngle;
            icePath.isPlane = false;
            //Debug.log
            if (direction < 0)
                icePath.throwAngle = 180 + shootAngle;

            icePath.throwForce = shootForce;
            icePath.SendMessage("Shoot");
        }

    }
    public void prepareToFly()
    {
        this.isUsingPlane = true;

        this.planeSprite.SetActive(true); 
    }

    public void unloadFly()
    {
        this.isUsingPlane = false;

        this.planeSprite.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision entered");
        if (collision.gameObject.tag == "platform")
        {
            //Debug.Log("Grounded!!!");s
            isGrounded = true;
            isJumping = false;
            animator.SetBool("isJumping", isJumping);
           

        }
        //if (collision.gameObject.tag == "")
        if (collision.gameObject.tag == "bossLaser")
        {

            this.loseHP(hpLostOnHitLaser);
            Debug.Log("laser hit!!!"); 
        }
        if (collision.gameObject.tag == "bossBody")
        {
            Debug.Log("ollision.gameObject.tag == bossBody");
            this.loseHP(100); 
        }
        if (collision.gameObject.tag == "playerBullet")
        {
            Debug.Log("ollision.gameObject.tag == playerBullet");
            this.loseHP(10);
        }
        unloadFly(); 

    }
    public void loseHP(float amount)
    {
        //Debug.Log("Character loses HP! " +amount);
        this.HP -= amount;
        var bgBar = transform.GetChild(0);

        var curBar = bgBar.transform.GetChild(0);
        var percent = this.HP / this.maxHP;
        if (percent < 0)
            percent = 0;
        //percent = percent * bgBar.transform.localScale.x;
        curBar.localScale = new Vector3(percent, 1);

        if (percent <= 0)
        {
            animator.SetFloat("HP", 0);
            SceneManager.LoadScene("LoseScreen"); 
              
        }
    }
    public void characterFly()
    {
        iceBolt = Object.Instantiate(GameObject.Find("paper_plane"));
        iceBolt.SetActive(true);
        var orb = transform.GetChild(1);
        iceBolt.transform.position = new Vector3(orb.position.x + 1, orb.transform.position.y + 1, orb.position.z);
        //iceBolt.transform.position = new Vector3(transform.position.x, transform.position.y - orb.transform.position.y, transform.position.y);

        TrajectoryHelper icePath = iceBolt.GetComponent<TrajectoryHelper>();
        icePath.throwAngle = shootAngle;
        //Debug.log
        if (direction < 0)
            icePath.throwAngle = 180 + shootAngle;

        icePath.throwForce = shootForce;
        icePath.SendMessage("Shoot");
    }

}
