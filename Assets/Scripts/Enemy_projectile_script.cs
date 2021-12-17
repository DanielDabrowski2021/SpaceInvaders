using UnityEngine;
using System.Collections;

public class Enemy_projectile_script : MonoBehaviour {


	void Start () {
	
	}


    void Update()
    {
        if (this.transform.position.y <= -35)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player_projectile(Clone)")
        {
            Destroy(this.gameObject);
            GameObject destroy_it = col.gameObject;
            Destroy(destroy_it);
        }
        if (col.gameObject.name == "Enemy_projectile(Clone)")
        {
            Destroy(this.gameObject);
            GameObject destroy_it = col.gameObject;
            Destroy(destroy_it);
        }
    }
}
