using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public static int point;
    public static int record;
    public static bool start;
    [SerializeField] private Text txt;
    [SerializeField] private Text Recordtxt;

    [SerializeField] private GameObject startpanel;
    // Start is called before the first frame update
    void Start()
    {
        point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //назначение значений текстов
        txt.text = "Points: " + point.ToString();
        Recordtxt.text = "Record: " + record.ToString();
        //рекорд
        if (point >= record)
        {
            record = point;
        }       
        //запуск игры 
        if (Input.GetMouseButtonDown(0))
        {
            start = true;
            startpanel.SetActive(false);
        }
        //течение времени игры 
        if (start)
        {
            Time.timeScale = 1;
        }
        else 
        {
            startpanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
