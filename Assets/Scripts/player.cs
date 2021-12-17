using UnityEngine;
using System.Collections;
using System;
public class player : MonoBehaviour {

    public GameObject projectile;
    private GameObject pocisk1;
    private GameControl gm;
    public float speed = 20;

    private float fire = 0;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
    }

    void Start () {
	
	}
 
    void fire_()
    {
        if (gm.game_over >= 1)
        {
            return;
        }
        if (Time.time-fire< 2.0f)
        {
            return;
        }
        pocisk1 = Instantiate(projectile, new Vector3(this.transform.position.x, this.transform.position.y + 5, 0), Quaternion.identity) as GameObject;
        pocisk1.GetComponent<Rigidbody>().velocity = Vector3.up * speed;
        fire = Time.time;

    }
    void Update ()
    {
        fire_();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Enemy_projectile(Clone)")
        {

            int value;
            value = Int32.Parse(gm.points.text);
            value = value - 8;
            gm.points.text = value.ToString();
            GameObject destroy_it = col.gameObject;
            Destroy(destroy_it);
        }

    }

}
