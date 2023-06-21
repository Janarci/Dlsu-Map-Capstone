using System;
using System.Collections;
using System.Collections.Generic;
    using Unity.AI.Navigation;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;

public class catAgent : MonoBehaviour
{
    // Start is called before the first frame update
    enum AgentState
    {
        idle,
        walking,
        jumping,
        resting
    }


    [Serializable]
    class AreaData
    {
        public HQArea area;
        public Transform pos;  
    }

    float lerpTValue = 0f;
    AgentState state = AgentState.idle;

    [SerializeField] HQArea[] areas;
    Dictionary<HQArea.Area, List<HQArea>> mappedDatabase;
    HQArea area = null;
    [SerializeField] private Transform movePositionTransform;
    public NavMeshAgent navMeshAgent { get; private set; }
    CatAnimsTest catAnim;

    Vector3 jumpStart = Vector3.zero;
    Vector3 jumpEnd = Vector3.zero;

    float idleCurrentDuration = 0.0f;
    float idleMaxDuration = 7.5f;

    public float jumpCooldown = 5.0f;
    bool isSelected = false;
	private void Awake()
	{
        navMeshAgent = GetComponent<NavMeshAgent>();
        catAnim = GetComponent<CatAnimsTest>();
        mappedDatabase= new Dictionary<HQArea.Area, List<HQArea>>();
        mappedDatabase[HQArea.Area.floor] = new List<HQArea>();
        mappedDatabase[HQArea.Area.rug] = new List<HQArea>();
        mappedDatabase[HQArea.Area.couch] = new List<HQArea>();


        //foreach(AreaData _data in data)
        //{
        //    mappedDatabase[_data.area] = _data.pos.position; 
        //}
    }
    void Start()
    {
        areas = FindObjectsOfType<HQArea>();

        foreach(HQArea _area in areas)
        {
            if(mappedDatabase.ContainsKey(_area.area))
            {
                mappedDatabase[_area.area].Add(_area);
            }
        }
        RandomizeArea();
    }

    private void RandomizeArea()
    {
        state = AgentState.idle;
        int _areaIndex1 = UnityEngine.Random.Range(1, 4);
        float _idleMaxDuration = UnityEngine.Random.Range(6.0f, 10.0f);

        HQArea.Area _area = (HQArea.Area)_areaIndex1;

        int _areaIndex2 = UnityEngine.Random.Range(0, mappedDatabase[_area].Count);

        if (!mappedDatabase[_area][_areaIndex2].isOccupied)
        {
            SetDestinationArea(mappedDatabase[_area][_areaIndex2]);
            Debug.Log("setting random target location");
        }

        else
        {
            Debug.Log("area is occupised");

        }

        idleMaxDuration = _idleMaxDuration;
        idleCurrentDuration = 0.0f;
    }

    private void SetDestinationArea(HQArea _area)
    {
        area = _area;
        navMeshAgent.SetDestination(_area.transform.position);
        //navMeshAgent.SetDestination(mappedDatabase[area]);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(navMeshAgent.isOnNavMesh + "----" + navMeshAgent.isStopped + " ----" + GetPathRemainingDistance() + "----" + state);

        if (state == AgentState.jumping)
        {
            //Debug.Log("Jumping");
            if (catAnim.isJumping() == false)
            {
                Debug.Log("Finished jumping");
                state = AgentState.idle;
                navMeshAgent.isStopped = false;
                //navMeshAgent.autoTraverseOffMeshLink = true;
                
                if(area != null)
                {
                    SetDestinationArea(area);
                }
            }
        }

        else if (state == AgentState.walking)
        {
            if (navMeshAgent.isStopped && navMeshAgent.hasPath && !isSelected)
            {
                navMeshAgent.isStopped = false;
            }

            if(catAnim.isWalking() == false)
            {
                catAnim.cat_walk_tail_start();
            }
        }

        else if (state == AgentState.idle)
        {
            if (navMeshAgent.isStopped && navMeshAgent.hasPath && !isSelected)
            {
                navMeshAgent.isStopped = false;
            }
            //Debug.Log(navMeshAgent.hasPath) ;
        }

        else if(state == AgentState.resting)
        {
            //Debug.Log(idleCurrentDuration + " || " + idleMaxDuration);
            if(idleCurrentDuration < idleMaxDuration)
            {
                Debug.Log("Resting");
                idleCurrentDuration += Time.deltaTime;
                //Debug.Log(idleCurrentDuration + " || " + idleMaxDuration);


            }

            else if (idleCurrentDuration >= idleMaxDuration)
            {
                RandomizeArea();
            }
        }


        

        if (state != AgentState.jumping && !GetComponent<CharacterController>().isGrounded)
        {
            //Debug.Log("applying downward movement");
            GetComponent<CharacterController>().Move(new Vector3(0.00f, -0.98f, 0.00f));
        }

        //Debug.Log(navMeshAgent.currentOffMeshLinkData.startPos);
        jumpCooldown = Math.Max(0, jumpCooldown - Time.deltaTime);

        if (area != null)
        {
            

            if (area.isInArea(transform.position))
            {
                //Debug.Log("in area");

                if (!(state == AgentState.resting))
                {
                    if(catAnim.isJumping() && state == AgentState.jumping)
                    {
                        //navMeshAgent.autoTraverseOffMeshLink = true;
                    }

                    state = AgentState.resting;
                    navMeshAgent.isStopped = true;
                    //Debug.Log("resting");
                    area = null;
                    idleCurrentDuration = 0.0f;
                    catAnim.cat_walk_tail_end();
                }
                //Debug.Log("001");
            }
            
            else if (navMeshAgent.isOnOffMeshLink)
            {
                //Debug.Log("in link");
                NavMeshLink meshLink = navMeshAgent.navMeshOwner as NavMeshLink;
                //Debug.Log(meshLink.area);
                //Debug.Log(meshLink.offMeshLink.area);
                if(meshLink.transform.IsChildOf(area.transform))
                {
                    if (!(state == AgentState.jumping) && meshLink.area == NavMesh.GetAreaFromName("Jump") && jumpCooldown == 0)
                    {
                        navMeshAgent.autoTraverseOffMeshLink = false;
                        state = AgentState.jumping;
                        //Debug.Log("bottom of jump");
                        navMeshAgent.isStopped = true;

                        //state = AgentState.jumping;
                        //Debug.Log("Destination: " + meshLink.transform.position + meshLink.endPoint);
                        catAnim.TestJump((meshLink.transform.position + (meshLink.endPoint)) + Vector3.up * navMeshAgent.baseOffset);

                        Timers.Instance?.StartCatJumpCooldown(this);
                        jumpCooldown = 15.0f;
                        //Debug.Log("Jumping");
                    }
                    //state = AgentState.jumping;
                    //Debug.Log("moving upwards");
                    Debug.Log("002");
                }

                else if(Vector3.Distance(meshLink.transform.position + meshLink.startPoint, transform.position) > Vector3.Distance(meshLink.transform.position + meshLink.endPoint, transform.position))
                {
                    if (!(state == AgentState.jumping) && meshLink.area == NavMesh.GetAreaFromName("Jump") && jumpCooldown == 0)
                    {
                        navMeshAgent.autoTraverseOffMeshLink = false;
                        state = AgentState.jumping;
                        //Debug.Log("top of jump");
                        navMeshAgent.isStopped = true;

                        //state = AgentState.jumping;
                        //Debug.Log("Destination: " + meshLink.transform.position + meshLink.startPoint);
                        catAnim.TestJump((meshLink.transform.position + (meshLink.startPoint)) + Vector3.up * navMeshAgent.baseOffset);

                        Timers.Instance?.StartCatJumpCooldown(this);
                        jumpCooldown = 15.0f;
                        //Debug.Log("Jumping");
                    }
                }

               


            }

            else if (navMeshAgent.velocity.magnitude >= 0.001f && !(state == AgentState.jumping))
            {
                if (!(state == AgentState.walking))
                {
                    state = AgentState.walking;
                }
                //Debug.Log(catAnim.isWalking());
                //Debug.Log("003");

                //if(navMeshAgent.isOnNavMesh == false)
                //{
                //    navMeshAgent.transform.position = navMeshAgent.nextPosition;
                //}
            }
        }

        //Debug.Log(navMeshAgent.currentOffMeshLinkData.endPos);

        

        if(navMeshAgent.velocity.magnitude == 0.00f && state != AgentState.resting)
        {

            if (!(state == AgentState.idle))
            {
                //navMeshAgent.autoTraverseOffMeshLink = true;
                state = AgentState.idle;
                catAnim.cat_walk_tail_end();
            }
            //Debug.Log("004");

        }

        else if(navMeshAgent.hasPath && !navMeshAgent.isStopped && !isSelected)
        {
            transform.LookAt(new Vector3 (navMeshAgent.nextPosition.x, transform.position.y, navMeshAgent.nextPosition.z));
        }

        //else if(navMeshAgent.currentOffMeshLinkData.endPos.y != navMeshAgent.currentOffMeshLinkData.startPos.y && navMeshAgent.transform.position.y == navMeshAgent.currentOffMeshLinkData.endPos.y)
        //{
        //    state = AgentState.idle;
        //    GetComponent<CharacterController>().Move(new Vector3(0, -0.08f, 0));
        //}
        //else if (!GetComponent<CharacterController>().isGrounded && state != AgentState.jumping)
        //{
        //    Debug.Log("moving downwards");
        //    GetComponent<CharacterController>().Move(new Vector3(0, -0.08f, 0));
        //}

        //else if(state == AgentState.jumping)
        //{
        //    if(jumpStart == Vector3.zero && jumpEnd == Vector3.zero)
        //    {
        //        jumpStart = navMeshAgent.currentOffMeshLinkData.startPos;
        //        jumpEnd = navMeshAgent.currentOffMeshLinkData.endPos;
        //    }
        //    transform.position = ( Vector3.Lerp(jumpStart, jumpEnd, lerpTValue/ GetComponent<CatAnimsTest>().GetAnimDuration("cat_jump")));
        //    lerpTValue += Time.deltaTime;
        //    Mathf.Clamp(lerpTValue, 0, GetComponent<CatAnimsTest>().GetAnimDuration("cat_jump")); 

        //    if(lerpTValue == GetComponent<CatAnimsTest>().GetAnimDuration("cat_jump"))
        //    {
        //        state = AgentState.idle;
        //        jumpStart = Vector3.zero;
        //        jumpEnd = Vector3.zero;
        //        lerpTValue = 0;
        //    }
        //}

        //transform.LookAt(((navMeshAgent.nextPosition * 100) - transform.position) * 100);


        //Debug.Log(GetComponent<CharacterController>().isGrounded);
        if (Input.GetKeyDown(KeyCode.Space))
		{
            navMeshAgent.SetDestination(movePositionTransform.position);
            //GetComponent<CatAnimsTest>().cat_jump();
        }

        //Debug.Log(navMeshAgent.isOnNavMesh + "----"+ navMeshAgent.isStopped + " ----" + GetPathRemainingDistance() + "----" + state);


    }

    public void StopRoam()
    {
        navMeshAgent.isStopped = true;
        isSelected = true;
    }

    public void ResumeRoam()
    {
        navMeshAgent.isStopped = false;
        isSelected = false;

    }


    public float GetPathRemainingDistance()
    {
        if (navMeshAgent.pathPending ||
            navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
            navMeshAgent.path.corners.Length == 0)
            return -1f;

        float distance = 0.0f;
        for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
        {
            distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
        }

        return distance;
    }
}
