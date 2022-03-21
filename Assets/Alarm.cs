using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    [SerializeField] private InputField _hours;
    [SerializeField] private InputField _minuts;

    [SerializeField] private Camera _mainCamera;

    private GameObject _alarmArrow;

    private int _satMin = -1;
    private int _satHour = -1;

    private int _nowHours;
    private int _minByAlarmArrow;

    [SerializeField] private bool isSet = false;

    private void Awake()
    {
        Clock.timeToAlarm.AddListener(GetNowTime);
        GetComponent<AudioSource>().Stop();

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "AlarmArrow")
            {
                _alarmArrow = hit.transform.gameObject;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            _alarmArrow = null;
        }

        if (_alarmArrow != null)
        {
            _alarmArrow.transform.parent.rotation = Quaternion.LookRotation(new Vector3(Input.mousePosition.x - Screen.width / 2, .5f, Input.mousePosition.y - Screen.height / 2));
            _minByAlarmArrow = (int)(_alarmArrow.transform.parent.localEulerAngles.y / 6);
            print(_minByAlarmArrow);
        }
    }

    public void SetByArrow()
    {
        _satHour = _nowHours;
        _satMin = _minByAlarmArrow;

        isSet = true;

        _minuts.text = _satMin.ToString();
        _hours.text = _satHour.ToString();
    }

    public void OnClickSetAlarm()
    {
        try
        {
            _satMin = Int32.Parse(_minuts.text);
            _satHour = Int32.Parse(_hours.text);

            _satMin = Mathf.Clamp(_satMin, 0, 59);
            _satHour = Mathf.Clamp(_satHour, 0, 23);
        }
        catch
        {
            return;
        }
        finally
        {
            if (_satMin != -1 && _satHour != -1)
            {
                isSet = true;
            }

            _minuts.text = _satMin.ToString();
            _hours.text = _satHour.ToString();

            print(_satMin);
            print(_satHour);
        }
        
    }

    public void OnClickDisableAlarm()
    {
        _minuts.text = null;
        _hours.text = null;

        isSet = false;

        GetComponent<AudioSource>().Stop();
    }

    private void GetNowTime(int h, int m)
    {
        _nowHours = h;

        if(h == _satHour && m == _satMin && isSet)
        {
            Ring();
        }
    }

    private void Ring()
    {
        GetComponent<AudioSource>().Play();
    }
}
