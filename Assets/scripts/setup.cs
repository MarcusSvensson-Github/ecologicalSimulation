/*
Tänkte vi behöver en script för 
en som slummässigt utplacerar alla entiteter vid start så dem inte 
generas rakt på varandra och det blir katastrof när alla är i collission med varandra
Tänker det är även en bra plats att skapa nya planter/växter som mat så bytesdjur inte svälter dirr
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setup : MonoBehaviour
{
    //startvärden att ange 
    public GameObject bytePrefab;
    public GameObject rovPrefab;
    public GameObject plantPrefab;
    
    public Transform folderRov;
    public Transform folderByte;
    public Transform folderPlant;

    public int antalBytesdjur;
    public int antalRovdjur;
    public int antalPlanter;

    public int xPos;
    public int zPos;


    void Start()
    {
        plantSpawn(antalPlanter);
        rovdjurSpawn(antalRovdjur);
        bytesdjurSpawn(antalBytesdjur);  
    }

    void FixedUpdate(){
        plantGenerator();
    }



    public void plantGenerator()
    {
        //Om antal plantor faller under värdet antalPlanter
        //Så kör vi funktionen för att skapa plantor.
        if (folderPlant.childCount < antalPlanter)
        {
            plantSpawn(1);
        }
    }


    //såg någon snubbe på youtube använda dessa till spawna AI agents
    //tillfälligt statiska värden, behöver igen lyckas generar mesh max och min i xPos och zPos
    
    //Ändrade om funktionerna för att skapa djur och växter
    //tar emot en parameter så att vi kan återanvända funktionerna och skapa mer när det behövs
    //ex plantSpawn(22) spawnar 22 plantor
    
    public void bytesdjurSpawn(int bytesdjur){
        while(bytesdjur > 0){
            xPos = Random.Range(-100, 100);
            zPos = Random.Range(-100, 100);
            Instantiate(bytePrefab, new Vector3(xPos, 0f, zPos), Quaternion.identity, folderByte);
            bytesdjur -= 1;
        }
    }


    public void rovdjurSpawn(int rovdjur){
        while(rovdjur > 0){
            xPos = Random.Range(-100, 100);
            zPos = Random.Range(-100, 100);
            Instantiate(rovPrefab, new Vector3(xPos, 0f, zPos), Quaternion.identity, folderRov);
            rovdjur -= 1;
        }
    }



    public void plantSpawn(int plants){
        while(plants > 0){
            xPos = Random.Range(-65, 65);
            zPos = Random.Range(-65, 65);
            Instantiate(plantPrefab, new Vector3(xPos, 0f, zPos), Quaternion.identity, folderPlant);
            plants -= 1;    
        }   
    }


}
