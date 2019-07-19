using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossAction : MonoBehaviour
{
    public float HP = 1000.0f;
    public float maxHP = 1000.0f;
    private float shootWaitingTime = 5.0f;
    public float maxShootWaitingTime = 5.0f;

    public GameObject character;
    public GameObject projectile;

    public float direction = -1;
    public float shootAngle;
    public float shootForce;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        loseHP(0); 
    }
    // Update is called once per frame
    void Update()
    {
        shootWaitingTime -= Time.deltaTime;
        if (shootWaitingTime < 0)
        {
            shootWaitingTime = maxShootWaitingTime;
            Vector3 horizontalVector = Vector3.right;
            Bounds characterBound = character.GetComponent<SpriteRenderer>().sprite.bounds;
            CapsuleCollider2D capCollider = GetComponent<CapsuleCollider2D>();
            Bounds capsuleColliderBound = capCollider.bounds;
            Vector3 diagonal = new Vector3(
                capCollider.transform.position.x - capsuleColliderBound.extents.x - (character.transform.position.x + 0.5f * characterBound.extents.x),
                capCollider.transform.position.y - capsuleColliderBound.extents.y - (character.transform.position.y - 0.5f*characterBound.extents.y));
            //Vector3 diagonal = new Vector3(
            //   transform.position.x - (character.transform.position.x + 0.5f * character.GetComponent<SpriteRenderer>().sprite.bounds.extents.x),
            //   transform.position.y - (character.transform.position.y + 0.5f * character.GetComponent<SpriteRenderer>().sprite.bounds.extents.y));
            shootAngle = Vector3.Angle(horizontalVector, diagonal);
            //Debug.Log("shootAngle:" + shootAngle);
            shootForce = 21000.0f;
            BossShoot();
        }
    }
    public void loseHP(float amount)
    {
        //Debug.Log("boss lose hp " + amount); 
        this.HP -= amount;
        var bgBar = transform.GetChild(0);

        var curBar = bgBar.transform.GetChild(0);
        var percent = this.HP / this.maxHP;
        if (percent < 0)
        {
            percent = 0;
            SceneManager.LoadScene("WinScreen"); 
        }
        //percent = percent * bgBar.transform.localScale.x;

        curBar.localScale = new Vector3(percent, 1);

    }

    public void BossShoot()
    {

        GameObject iceBolt = Object.Instantiate(projectile);
        iceBolt.SetActive(true);
        var orb = GetComponent<CapsuleCollider2D>();
        iceBolt.transform.position = GetComponent<CapsuleCollider2D>().bounds.center; 
        //iceBolt.transform.position = new Vector3(orb.transform.position.x, orb.transform.position.y);
        var characterBound = this.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        CapsuleCollider2D capCollider = GetComponent<CapsuleCollider2D>();
        Bounds capsuleColliderBound = capCollider.bounds;
        Vector3 diagonal = new Vector3(
               capCollider.transform.position.x - capsuleColliderBound.extents.x - (character.transform.position.x + 0.5f * characterBound.x),
               capCollider.transform.position.y - capsuleColliderBound.extents.y - (character.transform.position.y - characterBound.y));

        var newScale = new Vector3();
        var laserSize = characterBound;
        newScale.x = diagonal.x / (laserSize.x * 2 / iceBolt.transform.localScale.x);
        newScale.y = diagonal.y / (laserSize.y * 2 / iceBolt.transform.localScale.y);
        iceBolt.transform.localScale = new Vector3(0.1f, 0.1f); 
        //Debug.Log("newScale" + newScale.ToString());
        StraightPath icePath = iceBolt.GetComponent<StraightPath>();
        icePath.throwAngle = shootAngle;
        icePath.eye = this.GetComponent<CapsuleCollider2D>();
        icePath.endVector = diagonal;
        icePath.initialScale = new Vector3(newScale.x, newScale.y);
        //Debug.Log("initialScale" + icePath.initialScale);
        //Debug.log
        if (direction < 0)
            icePath.throwAngle = 180 + shootAngle;

        //icePath.throwForce = shootForce;
        icePath.SendMessage("Shoot");
        Debug.Log("Boss shoot");

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //do stuff
        Debug.Log("mode:" + PlayerPrefs.GetString("mode")); 
        CharacterMovement c = character.GetComponent<CharacterMovement>();
        if (PlayerPrefs.GetString("mode") == "easy")
        {
            c.jumpForce = 5;
            maxShootWaitingTime = 10;
            c.hpLostOnHitLaser = 20; 
        }
        else
        {
            c.jumpForce = 35;
            maxShootWaitingTime = 15;
            c.hpLostOnHitLaser = 30;
        }

    }
}