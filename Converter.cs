using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Converter : MonoBehaviour
{
    string str = "";
    public TextAsset text;
    public GameObject node;
    GameObject[] nodes;
    static int count; //when 0 - calculates - should be 0 only after loading a new map and clearning all of the Gameobjects and lines
    const int maxNodes = 10000;
    LineRenderer lineRND;
    // Start is called before the first frame update
    void Start()
    {
        lineRND = gameObject.GetComponent<LineRenderer>();
        nodes = new GameObject[maxNodes];
        count = 0;
        str = text.text;
        print(str.Length);

    }

    private void drawLines() {
        lineRND.positionCount = count;
        for (int i = 0; i < count; i++) {
            lineRND.SetPosition(i, nodes[i].transform.position);
        }
    }

    private void placeNodes()
    {
        int startingIndex = 0;
        int tmpIndex = 0;
        // search where the line starts - first find the index of "path"
        for (int i = 20; i<200; i++)
        {
            if (str[i] == 'p' && str[i + 1] == 'a' && str[i] == 't' && str[i + 1] == 'h') ; // if the vehicle has a very long name with "path" in its name then the line will be *broken* if the vehicle's name also has "z" after the "path"
            tmpIndex = i + 5; //after tmpIndex the letter z should not apear - only as cordinate z
            break;
        }
        //count 3 apearences of the letter z, and set the starting index to the index of the 3nd appearance +4

        for (int i = tmpIndex, zCounter=0; i < 400 && zCounter<3; i++) {
            if (str[i] == 'z') {
                zCounter++;
            }
            if (zCounter == 3) {
                startingIndex = i + 4;
            }
        }
        //initializing function's stuff
        count = 0;
        int index = startingIndex;  // setting the first index for reading the the index found above
        //if the index is 0, the line file is not a line file
        if (index == 0) {
            Debug.Log("ERROR: OREN FOUND A PROBLEM WITH YOUR LINE");
            return;
        }
        int last = str.Length - 1;
        float y = 0;
        float x = 0;
        float z = 0;

        //old code, spawns the nodes for the line
        while(index < last)
        {
            if (str[index] == ':') {
                if (str[index - 2] == 't') {
                    index++;
                    continue;
                }
                if (str[index + 1] == '0' || str[index + 1] == '1' || str[index + 1] == '2' || str[index + 1] == '3' || str[index + 1] == '4' || str[index + 1] == '5' || str[index + 1] == '6' || str[index + 1] == '7' || str[index + 1] == '8' || str[index + 1] == '9' || str[index + 1] == '-') {
                    if (!(y == 0))
                    {
                        if (!(x == 0))
                        {
                            string tmp = str[index + 1].ToString() + str[index + 2].ToString() + str[index + 3].ToString() + str[index + 4].ToString() + str[index + 5].ToString() + str[index + 6].ToString();
                            if (str[index + 6] == ',')
                                tmp = tmp.Substring(0,tmp.Length-1);
                            z = (float)Convert.ToDouble(tmp);
                            Vector3 tmpv = new Vector3(y,x,0.1f);

                            if (count < maxNodes)
                            {
                                nodes[count] = Instantiate(node, tmpv, Quaternion.identity);
                                nodes[count].GetComponent<Value>().setValues(x, y, z);  //gives the node the x,y,z cordinates, so they can be used when exporting
                                count++;
                            }
                            else
                            {
                                Debug.Log("Line is too long, oren didn't expect lines that so long");
                            }
                            //debug
                            // print("x = " + x + "y = " + y + "z = " + z);
                            x = 0; y = 0; z = 0;
                        }
                        else
                        {
                            string tmp = str[index + 1].ToString() + str[index + 2].ToString() + str[index + 3].ToString() + str[index + 4].ToString() + str[index + 5].ToString() + str[index + 6].ToString();
                            if (str[index + 6] == ',')
                                tmp = tmp.Substring(0, tmp.Length - 1);
                            x = (float)Convert.ToDouble(tmp);

                        }
                    }
                    else {

                        string tmp = str[index + 1].ToString() + str[index + 2].ToString() + str[index + 3].ToString() + str[index + 4].ToString() + str[index + 5].ToString() + str[index + 6].ToString();
                        if (str[index + 6] == ',')
                            tmp = tmp.Substring(0, tmp.Length - 1);
                        y = (float)Convert.ToDouble(tmp);

                    }

                }
            }
            index++;
        }

    }

    void FixedUpdate()
    {
        //no node has been spawned - spawn nodes
        if (count == 0)
        {
            placeNodes();
            drawLines();
        }
        
    }
}
