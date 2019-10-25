using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cam : MonoBehaviour
{
    public float speed;
    public Text camSpeed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        camSpeed.text = "LOADING? BUG?";
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > -0.1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        }
        if (transform.position.z < -2000)
        {
            transform.position = new Vector3 (transform.position.x, transform.position.y, -2000);
        }
        if (speed > 200)
        {
            speed = 200;
        } else if (speed < 0.5f) {
            speed = 0.5f;
        }
        if (Input.GetKey("z")) {

            transform.Translate(0, 0, speed * Time.deltaTime);
        }else if (Input.GetKey("x")) 
        {

            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey("up"))
        {

            speed += (speed * 3) * Time.deltaTime;
        }
        else if (Input.GetKey("down"))
        {

            speed -= (speed * 3) * Time.deltaTime;
        }

        if (Input.GetKey("a")) {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("s"))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("w"))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        float visualSpeed = speed * 10;
        visualSpeed = (int)visualSpeed;
        camSpeed.text = "CAM SPEED: " + (visualSpeed / 10); 
    }
}
