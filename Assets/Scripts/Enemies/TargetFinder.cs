using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy), typeof(NavMeshAgent))]
public class TargetFinder : MonoBehaviour
{

    public GameObject Target { get; protected set; }
    public float MaxSearchDistance = 50.0f;
    public float TargetDistanceFromTarget = 20.0f;

    bool Awake = true;

    Enemy enemy;
    NavMeshAgent agent;

    public Action MoveFunc;
    public Func<float,GameObject> SearchFunc;


    public void SetWake(bool wake)
    {
        Awake = wake;
    }

    float GetDistToTarget()
    {
        if (Target == null)
            return float.MaxValue;
        return Vector3.Distance(this.transform.position, Target.transform.position);
    }

    private GameObject FindTarget(float MaxDistance)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float closestObjectDistance = float.MaxValue;
        GameObject closestObject = null;


        foreach (GameObject g in players)
        {
            float distance = Vector3.Distance(this.transform.position, g.transform.position);
            if (distance <= MaxDistance)
            {
                if (distance < closestObjectDistance)
                {
                    closestObjectDistance = distance;
                    closestObject = g;
                }
            }
        }
        return closestObject;
    }

    void Update()
    {
        if (Awake)
        {
            if (Target == null)
                Target = SearchFunc(MaxSearchDistance);
            MoveFunc();
        }

    }
    private void MoveToEnemy()
    {
        if (Target != null)
        {
            if (TargetDistanceFromTarget < GetDistToTarget())
            {
                agent.SetDestination(Target.transform.position);
                agent.isStopped = false;
            }
            else
            {
                agent.isStopped = true;
            }
        }

    }
    void Start()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();

        MoveFunc = MoveToEnemy;
        SearchFunc = FindTarget;
    }
}
