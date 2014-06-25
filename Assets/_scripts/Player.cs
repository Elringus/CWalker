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
	}

	private void Update () 
	{
		navAgent.SetDestination(GameObject.FindGameObjectWithTag("DestinationPoint").transform.position);

		// restart if we can't reach the destination point
		if (navAgent.pathStatus == NavMeshPathStatus.PathPartial) Manager.I.Restart();

		animator.SetFloat("Speed", navAgent.desiredVelocity.sqrMagnitude / 20, .2f, Time.deltaTime);
	}

	
}