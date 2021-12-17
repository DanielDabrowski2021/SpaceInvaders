using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class back_to_menu_script : MonoBehaviour {


    public Text[] point = new Text[10];
    public Text number_of_games_;

    void Start () {

        string path = Directory.GetCurrentDirectory();
        path = path + "\\records.txt";
        string target = @path;
        
        if (File.Exists(target))
        {
            StreamReader sr = File.OpenText("records.txt");
            string input;
            input = sr.ReadLine();
            number_of_games_.text = input;
            int number_of_games= Int32.Parse(input);
            if (number_of_games > 10)
                number_of_games = 10;

            for (int i=0;i< number_of_games;i++)
            {
                input = sr.ReadLine();
               
                if (input.Equals(""))
                {
                    input = sr.ReadLine();
                    point[i].text = input;
                }
            }
 

            sr.Close();


        }
            
    }
	
    public void start_click_load()
    {
        Application.LoadLevel("menu");
    }
}
