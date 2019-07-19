using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightPath : MonoBehaviour
{
    public float throwAngle = 60;
    public float laserTime = 6;
    public float maxLaserTime = 3; 
    public Vector3 endVector; 
    public CapsuleCollider2D eye;
    public GameObject character;
   public Vector3 initialScale;
    private bool isVisible; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(new Vector3(0,0,100), new Vector3(endVector.x,endVector.x,100); 
        if (isVisible)
        {
            laserTime -= Time.deltaTime;
            gameObject.transform.localScale = initialScale * (1-laserTime / maxLaserTime);
            var oldColor = GetComponent<SpriteRenderer>().color;
            //GetComponent<SpriteRenderer>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1 - laserTime / maxLaserTime);
            if (laserTime < 0)
            {
                this.gameObject.SetActive(false);
                laserTime = maxLaserTime;
                if (isInRangeOfCharacter())
                {
                    Debug.Log("laser hurts character!");
                    character.GetComponent<CharacterMovement>().loseHP(40);
                }

            }
        }
    }

    void Shoot()
    {
        laserTime = maxLaserTime; 
        transform.eulerAngles = new Vector3(0, 0, throwAngle);
        isVisible = true;
        character.GetComponent<CharacterMovement>().loseHP(5);

        //Bounds bound = this.GetComponent<SpriteRenderer>().sprite.bounds;

        //transform.position = eye.transform.position - new Vector3(bound.extents.x, -bound.extents.y / 2);

    }
    bool isInRangeOfCharacter()
    {
        if (character.GetComponent<SpriteRenderer>().sprite.bounds.Contains(endVector))
        {
            Debug.Log("collided!");
            return true;
        }
        Debug.Log("not collided!");

        return false;
        //Vector3 direction = Quaternion.Euler(transform.eulerAngles) * Vector3.right;
        //DrawLine(Vector3.zero, endVector, Color.blue); 
        //return Physics.Raycast(Vector3.zero, direction); 
        //return true; 
    }
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 10.0f)
    {
        Debug.DrawLine(start, end, color); 
    }

}
