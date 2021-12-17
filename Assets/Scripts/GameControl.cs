using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class GameControl : MonoBehaviour {

    public GameObject statek1;
    public GameObject statek2;
    public GameObject player;
    public Text points;
    public GameObject[] tablica_statkow1 = new GameObject[8];
    public GameObject[] tablica_statkow2= new GameObject[24];
    public int[] ship_is_ok = new int[32];
    private int[] results = new int[10];
    public int number_of_destroyed_ships = 0;
    public int enemy_lvl = 0;
    public int game_over = 0;
    public float gm_move_difference=3.0f;
    public float gm_fire_difference = 3.0f;


    void Start () {

        float pozycja_x,pozycja_y,pozycja_z;
        pozycja_z = 0;
        pozycja_x = -17.0f;
        pozycja_y = 33.0f;
        for (int i = 0; i < 8; i++)
        {
            tablica_statkow1[i] = Instantiate(statek1, new Vector3(pozycja_x+i*5, pozycja_y, pozycja_z), Quaternion.identity) as GameObject;
            tablica_statkow1[i].GetComponent<Enemy>().column_number = i;
            tablica_statkow1[i].GetComponent<Enemy>().row_number = 0;
            ship_is_ok[i] = 1;
        }
        /*first row*/
        for (int i = 0; i < 8; i++)
        {
            tablica_statkow2[i] = Instantiate(statek2, new Vector3(pozycja_x + i * 5, pozycja_y-5, pozycja_z), Quaternion.identity) as GameObject;
            tablica_statkow2[i].GetComponent<Enemy>().column_number = i;
            tablica_statkow2[i].GetComponent<Enemy>().row_number = 1;
            ship_is_ok[i+8] = 1;
        }

        /*second row*/
        for (int i = 0; i < 8; i++)
        {
            tablica_statkow2[i+8] = Instantiate(statek2, new Vector3(pozycja_x + i * 5, pozycja_y - 10, pozycja_z), Quaternion.identity) as GameObject;
            tablica_statkow2[i+8].GetComponent<Enemy>().column_number = i;
            tablica_statkow2[i+8].GetComponent<Enemy>().row_number = 2;
            ship_is_ok[i + 16] = 1;
        }

        /*third row*/
        for (int i = 0; i < 8; i++)
        {
            tablica_statkow2[i + 16] = Instantiate(statek2, new Vector3(pozycja_x + i * 5, pozycja_y - 15, pozycja_z), Quaternion.identity) as GameObject;
            tablica_statkow2[i + 16].GetComponent<Enemy>().column_number = i;
            tablica_statkow2[i + 16].GetComponent<Enemy>().row_number = 3;
            ship_is_ok[i + 24] = 1;
        }
        
    }
	public void game_over_function()
    {
        game_over = 1;
        string path= Directory.GetCurrentDirectory();
        path = path + "\\records.txt";
        string target=@path;
        int new_record = 0;
        int index;
        index = 0;
        if (File.Exists(target))
        {
            StreamReader sr = File.OpenText("records.txt");
            string input;
            input = sr.ReadLine();
            for (int i = 0; i < 10; i++)
            {
                results[i] = -1000;
            }
            int liczba_wynikow;
            liczba_wynikow = Int32.Parse(input);
            int liczba_gier = liczba_wynikow;
            if (liczba_wynikow > 10)
                liczba_wynikow = 10;
            for (int i=0;i<liczba_wynikow;i++)
            {
                input = sr.ReadLine();

                if (input.Equals(""))
                {
                    input = sr.ReadLine();
                    results[i] = Int32.Parse(input);
                }

            }
            string points_;
            points_ = points.text;
            int actual_points = Int32.Parse(points_);
            int min;
            int temp,temp2;
            temp = 0;
            min = results[liczba_wynikow-1];
           
           
            if (liczba_wynikow<10)
            {
                liczba_wynikow++;

                for (int i = 0; i < liczba_wynikow; i++)
                {
                    if (actual_points > results[i])
                    {
                        temp = results[i];
                        results[i] = actual_points;
                        index = i;
                        break;
                    }
                }
                for (int i = index + 1; i < liczba_wynikow; i++)
                {
                    temp2 = results[i];
                    results[i] = temp;
                    temp = temp2;
                }
                sr.Close();

                liczba_gier++;
                FileInfo f = new FileInfo("records.txt");
                StreamWriter writer = f.CreateText();
                writer.WriteLine(liczba_gier + "\n");
                for (int i = 0; i < liczba_wynikow; i++)
                {
                    writer.WriteLine("" + results[i] + "\n");
                }

                writer.Close();
            }
            if (actual_points > min)
            {

                new_record = 1;
                if (liczba_wynikow == 10)
                {
                    for (int i = 0; i < liczba_wynikow; i++)
                    {
                        if (actual_points>results[i])
                        {
                            temp = results[i];
                            results[i] = actual_points;
                            index = i;
                            break;
                        }
                    }

                    for (int i=index+1;i<liczba_wynikow;i++)
                    {
                        temp2 = results[i];
                        results[i] = temp;
                        temp = temp2;
                    }
                    sr.Close();

                    liczba_gier++;
                    FileInfo f = new FileInfo("records.txt");
                    StreamWriter writer = f.CreateText();
                    writer.WriteLine(liczba_gier + "\n");
                    for (int i = 0; i < liczba_wynikow; i++)
                    {
                        writer.WriteLine("" + results[i] + "\n");
                    }

                    writer.Close();
                }

            }
            else if ((actual_points<=min) && (liczba_wynikow==10))
            {
                sr.Close();
                liczba_gier++;
                FileInfo f = new FileInfo("records.txt");
                StreamWriter writer = f.CreateText();
                writer.WriteLine(liczba_gier + "\n");
                for (int i = 0; i < liczba_wynikow; i++)
                {
                    writer.WriteLine("" + results[i] + "\n");
                }

                writer.Close();
            }

            sr.Close();
        }
        else
        {
            FileInfo f = new FileInfo("records.txt");
            StreamWriter writer = f.CreateText();
            writer.WriteLine("1 \n");
            string points_;
            points_ = points.text;
            writer.WriteLine("" + points_+ "\n");
            writer.Close();
        }

        FileInfo f2 = new FileInfo("temp.txt");
        StreamWriter writer2 = f2.CreateText();
        string points_2;
        points_2 = points.text;
        writer2.WriteLine(points_2 + "\n");
        if (new_record ==1)
        {
            writer2.WriteLine(index+1);
        }
        writer2.Close();
        Application.LoadLevel("game_over_scene");

    }


}
