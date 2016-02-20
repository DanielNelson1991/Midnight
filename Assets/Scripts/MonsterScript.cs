using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

	//public Transform player;
	//NavMeshAgent agent;

	//public Transform[] wayPoints;
	//public GameObject raycastPoint;

	//public bool inPlayerView;

	//Animator animator;
	//AudioSource audioSource;

	//int i = 0;

	//// Use this for initialization
	//void Start () {
	//	agent = GetComponent<NavMeshAgent>();
	//	animator = GetComponent<Animator>();
	//	audioSource = GetComponent<AudioSource>();
	//	inPlayerView = false;
	//}
	
	//// Update is called once per frame
	//void Update () {

	//	RaycastHit hit;
	//	Vector3 fwd = raycastPoint.transform.TransformDirection(Vector3.forward);

	//	if(Physics.Raycast(transform.position, fwd, out hit, 50))
	//	{
	//		Debug.DrawRay(raycastPoint.transform.position, fwd, Color.green);
	//		if(hit.transform.gameObject.name == "Player")
	//		{

	//			Debug.Log("I seeeeeeee youuuuuuuuu...");
	//			inPlayerView = true;


	//		}
	//	}

	//	if(inPlayerView)
	//	{
	//		agent.SetDestination(player.transform.position);

	//		animator.SetBool("inPlayerView", true);
	//		agent.speed = 4;
	//		agent.acceleration = 4;
	//		agent.stoppingDistance = 2;

	//		if(agent.remainingDistance > 10)
	//		{
	//			Debug.Log("Lost player...");
	//			animator.SetBool("inPlayerView", false);
	//			agent.SetDestination(wayPoints[i].position);
	//			agent.speed = 2;
	//			agent.acceleration = 2;
	//		}
	//	}

	//		if(agent.transform.position != wayPoints[i].position && inPlayerView == false)
	//		{
	//			agent.SetDestination(wayPoints[i].position);
	//			GetComponent<Animator>().SetBool("isWalking", true);
	//		}
			
	//		if(Mathf.Round(agent.transform.position.x) == Mathf.Round(wayPoints[i].position.x) && Mathf.Round(agent.transform.position.z) == Mathf.Round(wayPoints[i].transform.position.z) && inPlayerView == false)
	//		{
	//			GetComponent<Animator>().SetBool("isWalking", false);
	//			StartCoroutine(RandomWaypoint());
	//			agent.autoBraking = true;
	//		}
	//}

	//IEnumerator RandomWaypoint()
	//{
	//	//agent.speed = 0;
	//	yield return new WaitForSeconds(5);
	//	i = Random.Range(0, wayPoints.Length);
	//	//agent.speed = 2;
	//	agent.autoBraking = false;
	//}

	//public void PlayFootStep(AudioClip audioClip)
	//{
	//	audioSource.clip = audioClip;
	//	audioSource.Play();
	//}

}
