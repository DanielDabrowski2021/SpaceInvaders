using UnityEngine;
using System.Collections;

public class right_script : MonoBehaviour {

    private GameControl gm;
    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
    }

    public void right()
    {

        if (gm.player.transform.position.x < 27.5)
        {
            gm.player.transform.position = new Vector3(gm.player.transform.position.x + 2.5F, gm.player.transform.position.y, gm.player.transform.position.z);
        }
    }
}
