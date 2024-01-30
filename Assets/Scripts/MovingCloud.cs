using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingCloud : MonoBehaviour
{
    public float speed;
    public GameObject cloud;

    // store the cloud movemend duration of one way
    public float duration;
    // the time of cloud movement
    private float timer = 0.0f;

    private DateTime _start;

    // Start is called before the first frame update
    void Start()
    {
        _start = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime; 
        TimeSpan _time = DateTime.Now - _start;
        timer = _time.Seconds + _time.Milliseconds / 1000f;

        if(timer >= duration){
            _start = DateTime.Now;
            speed = -1 * speed;
        }

        cloud.transform.position += new Vector3(step, 0f, 0f);
    }

}
