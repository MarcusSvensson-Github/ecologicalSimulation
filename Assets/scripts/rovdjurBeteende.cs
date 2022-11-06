using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rovdjurBeteende : MonoBehaviour
{   

    public float eatRadie;
    public float parningsRadie;


    public float energyStart;
    private float energy;
    public float energyLoss;
    private float setEnergyLoss;

    public int matingThreshold;

    public float mutationSpeedChange;
    public float mutationVisionChange;

    public Transform folderByte;

    private float speed;

    



    private void Awake()
    {
        folderByte = GameObject.Find("Rovdjur").transform;

        speed = GetComponent<agent2Controller>().speed;

        setEnergyLoss = calcEnergyLoss();

        energy = energyStart;
    }

    void FixedUpdate()
    {
        energy -= setEnergyLoss;
        eatByte();
        

        if(energy < 0)
        {
            Destroy(gameObject);
        }

        if(energy > matingThreshold)
        {
            checkForMate();
        }
    }

    private float calcEnergyLoss()
    {
        return speed * energyLoss;
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
            if (hitCollider.gameObject.CompareTag("rovdjur")) 
            {
                if (hitCollider.GetComponent<rovdjurBeteende>().energy >= matingThreshold)
                {
                    mate(hitCollider.gameObject);
                    return;
                }
            }
        }
    }


    public void mate(GameObject otherParent)
    {

        //nollst�ller eatcount eftersom dom ska para sig
        energy = energyStart;
        otherParent.GetComponent<rovdjurBeteende>().energy = energyStart;

        //skapar barnet som en exakt kopia av detta objektet
        GameObject child = Instantiate(gameObject, transform.position, Quaternion.identity, folderByte);


        //vi kan �ndra egenskaper i child objektet om vi vill
        //ex f�r att �ndra hastighet p� barnet
        float parentsAvgSpeed = (speed + otherParent.GetComponent<NavMeshAgent>().speed) / 2;
        float childSpeed = Random.Range(-mutationSpeedChange, mutationSpeedChange) + parentsAvgSpeed;

        child.GetComponent<agent2Controller>().speed = childSpeed;


        //vi kan ocks� komma �t synradie och liknande p� barnet
        //ex synradie
        float parentsAvgVision = (GetComponent<agent2Controller>().synRadie + otherParent.GetComponent<agent2Controller>().synRadie) / 2;
        float childVision = Random.Range(-mutationVisionChange, mutationVisionChange) + parentsAvgVision;

        child.GetComponent<agent2Controller>().synRadie = childVision;  // h�mtar scriptet agentController och d�r kommer
                                                                // vi �t medlemsvariablerna ex synradie
        
    }


    public void eatByte(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, eatRadie);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.CompareTag("bytesdjur")){
                Destroy(hitCollider.gameObject);
                energy++;
            }

        }
    }
}
