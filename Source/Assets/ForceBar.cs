using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBar : MonoBehaviour
{
    // Start is called before the first frame update
    public float spaceHoldTime;
    public float upHodlTime;

    public float maxUpDegree;
    public float minUpDegree;

    public float maxShootForce = 10;
    public float aimAngle;

    public float characterDirection;

    public float forcePercentPerSecond = 20;
    public GameObject gainedForceBar;
    public GameObject character;
    public GameObject aimingCircle;
    public GameObject forceBarCompound;


    void Start()
    {
        //this.character = GameObject.Find("Character");
       

    }
    private void Awake()
    {
        var oldScale = this.aimingCircle.transform.eulerAngles;
        aimingCircle.transform.eulerAngles = new Vector3(oldScale.x, oldScale.y, this.minUpDegree * this.characterDirection);
    }
    // Update is called once per frame
    void Update()
    {
        //this.aimAngle = this.aimingCircle.transform.rotation.z;
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.spaceHoldTime = 0;
            var oldScale = this.aimingCircle.transform.eulerAngles;
            aimingCircle.transform.eulerAngles = new Vector3(oldScale.x, oldScale.y, this.minUpDegree * this.characterDirection);
            aimingCircle.SetActive(true);
            forceBarCompound.SetActive(true);
            Debug.Log("hold time"); 
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            var m = this.character.GetComponent<CharacterMovement>();
              m.prepareToFly(); 
        }
        
       
         

        if (Input.GetKey(KeyCode.Space))
        {
            if (this.spaceHoldTime < maxShootForce)
            {
                this.spaceHoldTime += Time.deltaTime * forcePercentPerSecond;
                var oldScale = this.gainedForceBar.transform.localScale;
                this.gainedForceBar.transform.localScale = new Vector3(this.spaceHoldTime / maxShootForce, oldScale.y, oldScale.z);
            }
            else performShoot();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && spaceHoldTime > 0)
        {
            performShoot();
        }

        if (aimingCircle.activeSelf)
        {
            //this.aimAngle = this.aimingCircle.transform.rotation.z; 
            this.characterDirection = character.GetComponent<CharacterMovement>().direction;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.upHodlTime = 0;
                //aimingCircle.SetActive(true);
                //forceBarCompound.SetActive(true);
            }
            if (aimAngle < minUpDegree)
                aimAngle = minUpDegree;
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                this.upHodlTime += Time.deltaTime * 1f;

                var oldScale = this.aimingCircle.transform.eulerAngles;
                this.aimAngle += Time.deltaTime / 2 * (maxUpDegree - minUpDegree);
                //Debug.Log("aimAngle" + aimAngle);
                if (this.aimAngle > maxUpDegree)
                    this.aimAngle = maxUpDegree;
                if (aimAngle < minUpDegree)
                    this.aimAngle = minUpDegree;
                this.aimingCircle.transform.eulerAngles = new Vector3(oldScale.x, oldScale.y, this.aimAngle * this.characterDirection);
            }


            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.upHodlTime -= Time.deltaTime * 1.0f;
                var oldScale = this.aimingCircle.transform.eulerAngles;
                this.aimAngle -= Time.deltaTime / 2 * (maxUpDegree - minUpDegree);
                //Debug.Log("aimAngle" + aimAngle);
                if (this.aimAngle > maxUpDegree)
                    this.aimAngle = maxUpDegree;
                if (aimAngle < minUpDegree)
                    this.aimAngle = minUpDegree;
                this.aimingCircle.transform.eulerAngles = new Vector3(oldScale.x, oldScale.y, this.aimAngle * this.characterDirection);
            }

        }

        //Debug.Log(spaceHoldTime); 
    }
    void performShoot()
    {
        this.character.GetComponent<CharacterMovement>().shootForce = this.spaceHoldTime;
        this.aimAngle = this.aimingCircle.transform.eulerAngles.z;
        this.character.GetComponent<CharacterMovement>().shootAngle = this.aimAngle;
        this.character.GetComponent<CharacterMovement>().SendMessage("CharacterShoot");
        //Debug.Log("Key up");
        aimingCircle.SetActive(false);
        forceBarCompound.SetActive(false);
        spaceHoldTime = 0;
    }
}
