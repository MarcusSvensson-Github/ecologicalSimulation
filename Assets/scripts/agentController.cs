using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class agentController : MonoBehaviour
{

    public NavMeshAgent agent;
    public float synRadie;
    public bool rovPresent;
    
    [Range(0, 100)] public float speed;
    [Range(1, 500)] public float walkRadius;

    public void Start(){
        
        agent = GetComponent<NavMeshAgent>();
        if(agent != null){
            agent.speed = speed;
            agent.SetDestination(RandomMeshLocation());
        }
    }

    public Vector3 RandomMeshLocation() {
        Vector3 finaldest = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * walkRadius;
        randomPos += transform.position;

        if(NavMesh.SamplePosition(randomPos, out NavMeshHit hit, walkRadius, 1)) {
            finaldest = hit.position;
        }

        return finaldest;
    }

    public void seRovdjur(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, synRadie);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.CompareTag("rovdjur")){
                Vector3 dirr = transform.position - hitCollider.transform.position;
                dirr += transform.position;
                agent.SetDestination(dirr);
                rovPresent = true; 
            }
            
        }
        return;
    }

    public void sePlanta(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, synRadie);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.CompareTag("plant")){ 
                Vector3 food = hitCollider.transform.position; 
                agent.SetDestination(food);
            }
        }
    }

    void FixedUpdate(){

        rovPresent = false;
        if( agent != null && agent.remainingDistance <= agent.stoppingDistance){
            agent.SetDestination(RandomMeshLocation());
        }
        
        seRovdjur();

        if (rovPresent == false){
            sePlanta();
        }

        //Set a new destination if im stuck
        if (agent.velocity.magnitude <= speed * 0.2f)
        {
            agent.SetDestination(RandomMeshLocation());
        }

    }
    
    
}