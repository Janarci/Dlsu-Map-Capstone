using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class catAgent : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

	private void Awake()
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
            navMeshAgent.destination = movePositionTransform.position;
		}
    }
}
