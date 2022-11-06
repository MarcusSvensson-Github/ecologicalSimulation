using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class dataCollection : MonoBehaviour
{
    public Transform folderByte;
    public Transform folderRov;

    public float samplingsIntervall = 10;
    private float nextUpdate;

    [SerializeField]
    private bool Marcus;
    [SerializeField]
    private bool Albin;
    
    private string path;

    private void Start()
    {
        nextUpdate = samplingsIntervall;



        if (Marcus)
        {
            path = "/Users/marcussvensson/AI-mesh test/Assets/Data/";
        }
        if (Albin)
        {
            path = "C:\\Users\\Albin\\wkspaces\\simuCrewTest\\Assets\\Data\\";
        }

        WriteString("bytePop.txt", "Pop Tid", path);
        WriteString("rovPop.txt", "Pop Tid", path);

        WriteString("byteVision.txt", "Range Tid", path);
        WriteString("rovVision.txt", "Range Tid", path);

        WriteString("byteSpeed.txt", "Speed Tid", path);
        WriteString("rovSpeed.txt", "Speed Tid", path);
    
    }

    private void FixedUpdate()
    {
        //Debug.Log(Time.time);
        if(Time.time >= nextUpdate)
        {
            popCount();
            avgSpeed();
            avgVision();
            nextUpdate = Time.time + samplingsIntervall;
        }
    }

    private void avgVision()
    {
        //rovdjur
        float rovAvgVision = 0;
        foreach (Transform child in folderRov)
        {
            rovAvgVision += child.GetComponent<agent2Controller>().synRadie;
        }
        rovAvgVision /= folderRov.childCount;

        //bytesdjur
        float byteAvgVision = 0;
        foreach (Transform child in folderByte)
        {
            byteAvgVision += child.GetComponent<agentController>().synRadie;
        }
        byteAvgVision /= folderByte.childCount;

        //write stuff
        WriteString("byteVision.txt", byteAvgVision.ToString(), path);
        WriteString("rovVision.txt", rovAvgVision.ToString(), path);
    }

    private void avgSpeed()
    {
        //rovdjur
        float rovAvgSpeed = 0;
        foreach (Transform child in folderRov)
        {
            rovAvgSpeed += child.GetComponent<agent2Controller>().speed;
        }
        rovAvgSpeed /= folderRov.childCount;

        //bytesdjur
        float byteAvgSpeed = 0;
        foreach(Transform child in folderByte)
        {
            byteAvgSpeed += child.GetComponent<agentController>().speed;
        }
        byteAvgSpeed /= folderByte.childCount;

        //write stuff
        WriteString("byteSpeed.txt", byteAvgSpeed.ToString(), path);
        WriteString("rovSpeed.txt", rovAvgSpeed.ToString(), path);

    }

    private void popCount()
    {
        int byteCount = folderByte.childCount;
        int rovCount = folderRov.childCount;

        WriteString("bytePop.txt", byteCount.ToString(), path);
        WriteString("rovPop.txt", rovCount.ToString(), path);
    }

    


    public static void WriteString(string filename, string data, string path)
    {
        Debug.Log("I AM WRITING TO FILE");

        data = data + " " + Time.time.ToString();
        
        path = path + filename;


        //Write some text to the test.txt file

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(data);

        writer.Close();
    }

}
