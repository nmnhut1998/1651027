using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject explosion;
    public GameObject aim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == aim.tag)
        {
            //Debug.Log("The bullet has collided with another object!");
            this.gameObject.SetActive(false);
            float x = this.gameObject.transform.position.x;
            float y = this.gameObject.transform.position.y;
            GameObject exp = Instantiate(explosion, new Vector3(x, y, 0), Quaternion.identity);
            exp.transform.localScale *= 0.1f;
            exp.SetActive(true);
            aim.GetComponent<BossAction>().loseHP(10);
            Debug.Log("collision " + collision.collider.GetType()); 
            //this.gameObject.SetActive(false); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == aim.tag)
        {
            //Debug.Log("The bullet has collided with another object!");
            float x = this.gameObject.transform.position.x;
            float y = this.gameObject.transform.position.y;
            GameObject exp = Instantiate(explosion, new Vector3(x, y, 0), Quaternion.identity);
            exp.transform.localScale *= 0.1f;
            exp.SetActive(true);
            aim.GetComponent<BossAction>().loseHP(300);
            //Debug.Log("collision " + collision.collider.GetType());
            //this.gameObject.SetActive(false); 
        }
        this.gameObject.SetActive(false);

    }
}
