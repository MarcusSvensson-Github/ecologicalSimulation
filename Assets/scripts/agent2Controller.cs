using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class agent2Controller : MonoBehaviour
{

    public NavMeshAgent agent2;
    
    public float synRadie;

    
    [Range(0, 100)] public float speed;
    [Range(1, 500)] public float walkRadius;

    public void Start(){

        
        agent2 = GetComponent<NavMeshAgent>();
        if(agent2 != null){
            agent2.speed = speed;
            agent2.SetDestination(RandomMeshLocation());
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

    public void seByte(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, synRadie);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.CompareTag("bytesdjur")){
                agent2.SetDestination(hitCollider.gameObject.transform.position);
            }
            
        }
        return;
    } 
    

    void FixedUpdate(){

        if( agent2 != null && agent2.remainingDistance <= agent2.stoppingDistance){
            agent2.SetDestination(RandomMeshLocation());
        }
        
        seByte();

        if(agent2.velocity.magnitude <= speed * 0.2f)
        {
            agent2.SetDestination(RandomMeshLocation());
        }

    }
    

}