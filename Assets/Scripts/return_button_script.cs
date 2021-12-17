using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;

public class return_button_script : MonoBehaviour {


    public Text points_;
    public Text record;
    void Start () {

        StreamReader sr = File.OpenText("temp.txt");
        string input;
        input = sr.ReadLine();
        points_.text = input;
        input = sr.ReadLine();

        input = sr.ReadLine();
        record.text = input;

        sr.Close();
    }
	
    public void start_click_load()
    {
        Application.LoadLevel("menu");
    }
}
