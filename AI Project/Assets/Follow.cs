using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    
    private NavMeshAgent _navMeshAgent;

    public bool isFollowing = false;
    
    public Transform followTarget;
    
    private enum State
    {
        Follow,
        Idle
    }
    
    private State _currentState = State.Follow;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        isFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case State.Follow:
                SetAITargetLocation(followTarget.position);
                
                break;
            
            case State.Idle:
                SetAITargetLocation(transform.position);
                
                break;
            
            default:
                throw new System.NotImplementedException();
        }
        
        if (_navMeshAgent.remainingDistance < 100.0f)
        {
            _currentState = State.Follow;
        }
        else
        {
            _currentState = State.Idle;
        }
    }
    
    
    private void SetAITargetLocation(Vector3 targetLocation)
    {
        _navMeshAgent.SetDestination(targetLocation);

    }
}
