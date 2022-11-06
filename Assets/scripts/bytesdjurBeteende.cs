using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bytesdjurBeteende : MonoBehaviour
{
    public float eatRadie;
    public float parningsRadie;

    private float energy;
    public float energyStart;
    public float _energyLoss;
    private float energyLoss;
    private float speed;

    public float mutationSpeedChange;
    public float mutationVisionChange;

    public int matingThreshold;

    public Transform folderByte;

   

    private void Awake()
    {
        folderByte = GameObject.Find("Bytesdjur").transform;

        speed = GetComponent<agentController>().speed;
        energyLoss = calcEnergyLoss();

        energy = energyStart;
    }

    private void FixedUpdate()
    {
        eatPlant();


        energy -= energyLoss;
        
        if (energy < 0)
        {
            Destroy(gameObject);
        }


        if (energy > matingThreshold)
        {
            checkForMate();
        }
    }

    private float calcEnergyLoss()
    {
        return speed * _energyLoss;
    }

    private void checkForMate()
    {
        /*
         * parningsRadie �r radien de m�ste befinna sig inom f�r att kunna para sig
         * kollar om detta objektet och det andra objektet b�da har �tit tillr�ckligt mycket f�r att para sig
         * d� k�rs parningsfunktionen mate()
         */
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, parningsRadie);

        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("bytesdjur")) 
            {
                if (hitCollider.GetComponent<bytesdjurBeteende>().energy >= matingThreshold)
                {
                    if(hitCollider.gameObject != gameObject){
                        mate(hitCollider.gameObject);                       
                        return; 
                    }                   
                }
            }
        }
    }

    public void mate(GameObject otherParent)
    {

        //nollst�ller eatcount eftersom dom ska para sig
        energy = energyStart;
        otherParent.GetComponent<bytesdjurBeteende>().energy = energyStart;

        //skapar barnet som en exakt kopia av detta objektet
        GameObject child = Instantiate(gameObject, transform.position, Quaternion.identity, folderByte);


        //vi kan �ndra egenskaper i child objektet om vi vill
        //ex f�r att �ndra hastighet p� barnet
        float parentsAvgSpeed = (speed + otherParent.GetComponent<NavMeshAgent>().speed) / 2;
        float childSpeed = Random.Range(-mutationSpeedChange, mutationSpeedChange) + parentsAvgSpeed;

        child.GetComponent<agentController>().speed = childSpeed;


        float parentsAvgVision = (GetComponent<agentController>().synRadie + otherParent.GetComponent<agentController>().synRadie)/ 2;
        float childVision = Random.Range(-mutationVisionChange, mutationVisionChange) + parentsAvgVision;
        //vi kan ocks� komma �t synradie och liknande p� barnet
        //ex synradie
        child.GetComponent<agentController>().synRadie = childVision;  // h�mtar scriptet agentController och d�r kommer
                                                                // vi �t medlemsvariablerna ex synradie
        
    }


    public void eatPlant()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, eatRadie);
        foreach(var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("plant"))
            {
                Destroy(hitCollider.gameObject);
                energy++;
            }
        }
    }
}
