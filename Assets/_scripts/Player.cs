using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private NavMeshAgent navAgent;
	private Animator animator;

	private void Awake ()
	{
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	private void Start ()
	{
		navAgent.SetDestination(GameObject.FindGameObjectWithTag("DestinationPoint").transform.position);
		GetComponentInChildren<TrailRenderer>().enabled = true;
	}

	private void Update () 
	{
		animator.SetFloat("Forward", navAgent.desiredVelocity.sqrMagnitude / 20, .2f, Time.deltaTime);
	}
}