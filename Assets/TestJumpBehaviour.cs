using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestJumpBehaviour : StateMachineBehaviour
{
    float normalizedTime = 0.0f;
    public Vector3 startPoint = Vector3.zero;
    public Vector3 endPoint = Vector3.zero;
    float duration = 0.0f;
    public AnimationCurve m_Curve = new AnimationCurve();


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        endPoint = new Vector3(animator.GetFloat("jumpTargetX"), animator.GetFloat("jumpTargetY"), animator.GetFloat("jumpTargetZ"));
        startPoint = animator.transform.parent.position;
        duration = 0.0f;

        animator.SetBool("isJumping", true);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //OffMeshLinkData data = agent.currentOffMeshLinkData;

        if(stateInfo.normalizedTime >= 0.26f)
        if (normalizedTime < 1.0f)
        {
            if (duration == 0.0f)
                duration = 1.125f - stateInfo.normalizedTime;


            float yOffset = m_Curve.Evaluate(normalizedTime);
            //animator.transform.parent.LookAt(endPoint);
            animator.transform.parent.parent.position = Vector3.Lerp(startPoint, endPoint, normalizedTime) + yOffset * Vector3.up;

            normalizedTime += Time.deltaTime / duration;
        }

        //Debug.Log(animator.GetBool("isJumping"));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        normalizedTime = 0.0f;
        endPoint = Vector3.zero;
        startPoint = Vector3.zero;

        animator.SetBool("isJumping", false);
        Debug.Log("exit jump state");

        animator.transform.parent.parent.GetComponent<NavMeshAgent>().CompleteOffMeshLink();
        //animator.transform.parent.GetComponent<catAgent>().navMeshAgent.autoTraverseOffMeshLink = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
