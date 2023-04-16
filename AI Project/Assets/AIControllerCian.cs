using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIControllerCian : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public float timer, wanderTime;
    public Transform otherPlayer;
    public bool isFollowing;

    private enum State
    {
        FindCharacter,
        Wander,
        Idle
    }

    private State _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = State.FindCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                SetAITargetLocation(hit.point);
            }
            
        }

        

        switch (_currentState)
        {
            case State.FindCharacter:
                
                SetAITargetLocation(otherPlayer.position);

                if (_navMeshAgent.remainingDistance < 100.0f && _navMeshAgent.remainingDistance > 5.0f)
                {
                    _currentState = State.Wander;
                    isFollowing = true;
                }

                break;
            
            case State.Wander:

                timer += Time.deltaTime;
                Wander();

                break;
            
            case State.Idle:
                
                SetAITargetLocation(transform.position);

                break;
        }
    }

    private void SetAITargetLocation(Vector3 targetLocation)
    {
        _navMeshAgent.SetDestination(targetLocation);

    }

    private void Wander()
    {
        if (timer >= wanderTime)
        {
            Vector2 wanderTarget = Random.insideUnitCircle * wanderTime;
            Vector3 wanderPos3D = new Vector3(transform.position.x + wanderTarget.x, transform.position.y, transform.position.z + wanderTarget.y);
            SetAITargetLocation(wanderPos3D);
            timer = 0;
        }
        
    }

    public void SetState(string newState)
    {
        _currentState = (State) Enum.Parse(typeof(State), newState);
    }
}

