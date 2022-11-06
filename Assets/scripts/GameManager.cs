using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Slider timeSlider;

    public Transform folderByte;
    public Transform folderRov;
    public Transform folderPlant;

    [SerializeField]
    private Text textRov;
    [SerializeField]
    private Text textByte;
    [SerializeField] //g�r s� att vi ser variablen i inspectorn men variabeln �r fortfarande privat
    private Text textPlant;
    [SerializeField]
    private Text timeMultiplier;

    private float fixedDeltaTimeStart;

    private void Start()
    {
        fixedDeltaTimeStart = Time.fixedDeltaTime;
    }



    private void FixedUpdate()
    {
        //set the time according to the timeSlider
        Time.timeScale = Mathf.Round(timeSlider.value);
        Time.fixedDeltaTime = fixedDeltaTimeStart / Time.timeScale;

        Debug.Log("timeScale: " + Time.timeScale);
        Debug.Log("fixedDeltaTime: " + Time.fixedDeltaTime);
        timeMultiplier.text = (int)Time.timeScale + "x";

        textRov.text = "Antal rovdjur: " + folderRov.childCount;
        textByte.text = "Antal bytesdjur: " + folderByte.childCount;
        textPlant.text = "Antal plantor: " + folderPlant.childCount;
    }
}
