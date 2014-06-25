using UnityEngine;
using System.Collections;

public class Player : PlaceableObject
{
	private NavMeshAgent navAgent;
	private Animator animator;

	public override void Awake ()
	{
		base.Awake();

		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Start ()
	{
		GetComponentInChildren<TrailRenderer>().enabled = true;
		navAgent.SetDestination(GameObject.FindGameObjectWithTag("DestinationPoint").transform.position);
	}

	private void Update () 
	{
		// restart if we can't reach the destination point
		if (navAgent.pathStatus != NavMeshPathStatus.PathComplete) Manager.I.Restart();

		animator.SetFloat("Speed", navAgent.desiredVelocity.sqrMagnitude / 20, .2f, Time.deltaTime);
	}

	
}