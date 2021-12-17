using UnityEngine;
using System.Collections;
using System;
public class Enemy : MonoBehaviour {



    public GameObject enemy_projectile;
    private GameObject pocisk1;
    private GameControl gm;
    public float speed_ = 20;
    public int column_number;
    public int row_number;

    private float move_time = 0;
    private float fire_time = 0;

    enum Move_
    {
        Left,
        Right,
        Down,
        Default,
    }

    private Move_ move_=0;
    private int left_number = 0;
    private int right_number_ = 0;
    private int temp_left_number = 0;
    private int temp_right_number = 0;
    private int temp_change = 0;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
    }

    void Start () {
        move_ = Move_.Left;
	}
    
    void fire()
    {
        if (gm.game_over == 1)
        {
            return;
        }
        DateTime now = DateTime.Now;

        UnityEngine.Random.seed = (now.Second + now.Minute + column_number + now.Millisecond*row_number);
        int wylosowana_liczba = UnityEngine.Random.Range(4, 7);
        gm.gm_fire_difference = (float)wylosowana_liczba;
        float lvl;
        lvl = (float)gm.enemy_lvl;

        gm.gm_fire_difference = gm.gm_fire_difference - (lvl * 0.25f);
        if (Time.time - fire_time < gm.gm_fire_difference)
        {
            return;
        }
        else
        {
            if (row_number == 3) 
            {
                
                pocisk1 = Instantiate(enemy_projectile, new Vector3(this.transform.position.x, this.transform.position.y - 5, 0), Quaternion.identity) as GameObject;
                pocisk1.GetComponent<Rigidbody>().velocity = Vector3.down * speed_;
                fire_time = Time.time;
            }
            else if (row_number==2)
            {
                if (gm.ship_is_ok[(row_number * 8) + column_number+8] == 0)
                {
                    pocisk1 = Instantiate(enemy_projectile, new Vector3(this.transform.position.x, this.transform.position.y - 5, 0), Quaternion.identity) as GameObject;
                    pocisk1.GetComponent<Rigidbody>().velocity = Vector3.down * speed_;
                    fire_time = Time.time;
                }
            }
            else if (row_number == 1)
            {
                if ((gm.ship_is_ok[(row_number * 8) + column_number + 8] == 0) && (gm.ship_is_ok[(row_number * 8) + column_number + 16] == 0))
                {
                    pocisk1 = Instantiate(enemy_projectile, new Vector3(this.transform.position.x, this.transform.position.y - 5, 0), Quaternion.identity) as GameObject;
                    pocisk1.GetComponent<Rigidbody>().velocity = Vector3.down * speed_;
                    fire_time = Time.time;
                }
            }
            else if (row_number == 0)
            {
                if ((gm.ship_is_ok[(row_number * 8) + column_number + 8] == 0) && (gm.ship_is_ok[(row_number * 8) + column_number + 16] == 0) && (gm.ship_is_ok[(row_number * 8) + column_number + 24] == 0))
                {
                    pocisk1 = Instantiate(enemy_projectile, new Vector3(this.transform.position.x, this.transform.position.y - 5, 0), Quaternion.identity) as GameObject;
                    pocisk1.GetComponent<Rigidbody>().velocity = Vector3.down * speed_;
                    fire_time = Time.time;
                }
            }
        }
    }
    int number_of_free_left_colums()
    {
        int left_number=0;
        int temp = 0;
        int end = 0;
        for (int j = 0; j < 8; j++)
        {
            temp = 0;
            for (int i = 0; i < 4; i++)
            {
                if (gm.ship_is_ok[(i * 8)+j] == 0)
                {
                    temp++;
                }
                else
                {
                    end = 1;
                    break;
                }
            }
            if (temp==4)
            {
                left_number++;
            }
            if (end==1)
            {
                break;
            }
        }

        return left_number;
    }
    int number_of_free_right_columns()
    {
        int right_number = 0;
        int temp = 0;
        int end = 0;
        for (int j = 7; j > -1; j--)
        {
            temp = 0;
            for (int i = 0; i < 4; i++)
            {
                if (gm.ship_is_ok[(i * 8) + j] == 0)
                {
                    temp++;
                }
                else
                {
                    end = 1;
                    break;
                }
            }
            if (temp == 4)
            {
                right_number++;
            }
            if (end == 1)
            {
                break;
            }
        }
        return right_number;
    }
    void move()
    {
        if (gm.game_over==1)
        {
            return;
        }

        if (Time.time - move_time < gm.gm_move_difference)
        {
            return;
        }
        if (move_ == Move_.Left)
        {
            left_number = number_of_free_left_colums();

            if ((temp_change==0) && (left_number!=0))
            {
                temp_left_number = left_number * 2;

            }

            if ((this.transform.position.x > (-25 + column_number * 5)))
            {
                this.transform.position = new Vector3(this.transform.position.x - 2.5F, this.transform.position.y, this.transform.position.z);
                move_time = Time.time;
            }
            else if (temp_left_number>0) 
            {
                

                temp_change = 1;
                this.transform.position = new Vector3(this.transform.position.x - 2.5F, this.transform.position.y, this.transform.position.z);
                move_time = Time.time;
  
                temp_left_number--;
                if (temp_left_number==0)
                {
                    temp_change = 0;
                    move_ = Move_.Right;
                    return;
                }
                return;
            }
            else
            {
                move_ = Move_.Right;
                return;
            }
        }
        if (move_ == Move_.Right)
        {
            right_number_ = number_of_free_right_columns();


            if ((temp_change == 0) && (right_number_ != 0))
            {
                temp_right_number = right_number_ * 2;

            }

            if ((this.transform.position.x < (25 - (7-column_number) * 5)))
            {
                this.transform.position = new Vector3(this.transform.position.x + 2.5F, this.transform.position.y, this.transform.position.z);
                move_time = Time.time;
            }
            else if (temp_right_number > 0)
            {
                temp_change = 1;
                this.transform.position = new Vector3(this.transform.position.x + 2.5F, this.transform.position.y, this.transform.position.z);
                move_time = Time.time;

                temp_right_number--;
                if (temp_right_number == 0)
                {
                    temp_change = 0;
                    move_ = Move_.Down;
                    return;
                }
                return;
            }
            else
            {
                move_ = Move_.Down; 
                return;
            }
        }
        if (move_ == Move_.Down)
        {

                this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y-5, this.transform.position.z);
                move_time = Time.time;
                move_ = Move_.Left; 
                return;

        }
        if (this.transform.position.y<=-30)
        {
            move_ = Move_.Default;
            gm.game_over = 1;
            gm.game_over_function();
        }
    }
    // Update is called once per frame
    void Update()
    {
        move();
        fire();
    }
    void level_up()
    {
        float lvl;
        lvl = (float)gm.enemy_lvl;
        gm.gm_move_difference = gm.gm_move_difference - lvl * 0.25f;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player_projectile(Clone)")
        {
            int value;
            value = Int32.Parse(gm.points.text);
            value = value + 1;
            gm.points.text = value.ToString();

            gm.ship_is_ok[(row_number * 8) + column_number] = 0;
            gm.number_of_destroyed_ships++;

            if ((gm.number_of_destroyed_ships > 0) && ((gm.number_of_destroyed_ships % 6) == 0) )
            {
                gm.enemy_lvl++;
                level_up();
            }

            if (gm.number_of_destroyed_ships>=32)
            {
                move_ = Move_.Default; 
                gm.game_over = 1;
                gm.game_over_function();
            }

            Destroy(this.gameObject);
            GameObject destroy_it = col.gameObject;
            Destroy(destroy_it);

        }
    }
}
