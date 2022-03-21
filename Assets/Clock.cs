using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
    public static UnityEvent<int, int> timeToAlarm = new UnityEvent<int, int>();

    [SerializeField] private GameObject _secondsArrow;
    [SerializeField] private GameObject _minutsArrow;
    [SerializeField] private GameObject _hoursArrow;

    [SerializeField] private Text _digitalClock;

    private int _sec = 0;
    private int _min = 0;
    private int _hou = 0;


    private void Start()
    {
        //Can't connect to NTP Server, so I get time from PC

        _hou = DateTime.Now.Hour;
        _min = DateTime.Now.Minute;
        _sec = DateTime.Now.Second;

        for(int i =0; i<(_sec+_min*60+_hou*60*60); i++)
        {
            _secondsArrow.transform.localEulerAngles += new Vector3(0, 6f, 0);
            _minutsArrow.transform.localEulerAngles += new Vector3(0, 6f / 60f, 0);
            _hoursArrow.transform.localEulerAngles += new Vector3(0, 6f / 60f / 12f, 0);
        }

        print(_hou);
        print(_min);
        print(_sec);

        StartCoroutine(GoSec());
    }

    private void MoveArrows()
    {
        _secondsArrow.transform.localEulerAngles += new Vector3(0, 6f, 0);
        _minutsArrow.transform.localEulerAngles += new Vector3(0, 6f / 60f, 0);
        _hoursArrow.transform.localEulerAngles += new Vector3(0, 6f / 60f / 12f, 0);

        _sec++;
         
        if(_sec == 60)
        {
            _min++;
            _sec = 0;
        }

        if(_min == 60)
        {
            _hou++;
            _min = 0;
        }

        if(_hou == 24)
        {
            _hou = 0;
        }

        string hours, minuts, seconds;

        seconds = _sec < 10 ? "0" + _sec.ToString() : _sec.ToString();  
        minuts = _min < 10 ? "0" + _min.ToString() : _min.ToString();  
        hours = _hou < 10 ? "0" + _hou.ToString() : _hou.ToString();

        _digitalClock.text = $"{hours} : {minuts} : {seconds}";

        timeToAlarm.Invoke(_hou, _min);
    }

    private IEnumerator GoSec()
    {
        yield return new WaitForSeconds(1f);
        MoveArrows();
        StartCoroutine(GoSec());
    }
}
