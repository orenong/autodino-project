using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    private float x, y, z;
    // Start is called before the first frame update
    public void setValues(float a, float b, float c)
    {
        x = a;
        y = b;
        z = c;
    }

    public override string ToString()
    {
        return ("x: " + x + " y: " + y + " z: " + z);
    }
}
