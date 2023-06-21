using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class CatAnimsTest : MonoBehaviour
{
    [SerializeField] Animator[] animators;
    [SerializeField] AnimatorController controller;
    Vector3 jumpTarget = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        foreach (Animator animator in animators)
        {
            Debug.Log(animator.avatar);
            animator.runtimeAnimatorController = controller;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(isJumping)
        //{
        //    animators[0].MatchTarget(jumpTarget, transform.rotation, AvatarTarget.LeftFoot, new MatchTargetWeightMask(Vector3.one, 1f), 0.435f, 0.86f);
        //}
    }

    public void ResetAnimator()
    {
        foreach (Animator animator in animators)
        {
            animator.Play("Idle");
        }
    }
    public void cat_feed_eat()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_feed_eat");

        }
    }

    public void cat_jump()
    {
        foreach(Animator animator in animators)
        {
            animator.SetTrigger("cat_jump");
        }

    }

    public void cat_laydown()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_laydown");
        }
    }

    public void cat_laydown_static()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_laydown_static");
        }
    }

    public void cat_laydown_static_breathe()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_laydown_static_breathe");
        }
    }

    public void cat_laydown_static_BT()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_laydown_static_BT");
        }
    }

    public void cat_laydown_static_BTS()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_laydown_static_BTS");
        }
    }

    public void cat_play()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_play");
        }
    }

    public void cat_walk_tail_start()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_walk_tail");
            animator.SetBool("isWalking", true);
        }
    }

    public void cat_walk_tail_end()
    {
        foreach (Animator animator in animators)
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void cat_play_down_static()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_play_down_static");
        }
    }

    public void cat_play_down_static_breathe()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_play_down_static_breathe");
        }
    }

    public void cat_play_down_static_BT()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("cat_play_down_static_BT");
        }
    }

    public void cat_react_bad()
    {
        int _rndIndex = UnityEngine.Random.Range(0, 2);
        foreach (Animator animator in animators)
        {
            if(_rndIndex == 0)
            {
                animator.SetTrigger("cat_react_bad_01");

            }

            else
            {
                animator.SetTrigger("cat_react_bad_02");
            }
        }
    }

    public void cat_react_good()
    {
        int _rndIndex = UnityEngine.Random.Range(0, 2);
        foreach (Animator animator in animators)
        {
            if (_rndIndex == 0)
            {
                animator.SetTrigger("cat_react_good_01");

            }

            else
            {
                animator.SetTrigger("cat_react_good_02");
            }
        }
    }

    public void cat_react_great()
    {
        int _rndIndex = UnityEngine.Random.Range(0, 2);
        foreach (Animator animator in animators)
        {
            if (_rndIndex == 0)
            {
                animator.SetTrigger("cat_react_great_01");

            }

            else
            {
                animator.SetTrigger("cat_react_great_02");
            }
        }
    }

    //public void cat_play()
    //{
    //    foreach (Animator animator in animators)
    //    {
    //        animator.SetTrigger("cat_play");
    //    }
    //}

    //public void cat_play()
    //{
    //    foreach (Animator animator in animators)
    //    {
    //        animator.SetTrigger("cat_play");
    //    }
    //}

    //public void cat_play()
    //{
    //    foreach (Animator animator in animators)
    //    {
    //        animator.SetTrigger("cat_play");
    //    }
    //}

    //public void cat_play()
    //{
    //    foreach (Animator animator in animators)
    //    {
    //        animator.SetTrigger("cat_play");
    //    }
    //}

    public bool isJumping()
    {
        return animators[0].GetBool("isJumping");
    }

    public float GetAnimDuration(string _anim)
    {
        AnimationClip[] clips = animators[0].runtimeAnimatorController.animationClips;
        float _duration = 0.0f;

        AnimatorController animatorController = animators[0].runtimeAnimatorController as AnimatorController;

        foreach (AnimatorControllerLayer layer in animatorController.layers)
        {
            foreach (ChildAnimatorState childAnimatorState in layer.stateMachine.states)
            {
                if (childAnimatorState.state.name == _anim)
                {
                    _duration = childAnimatorState.state.motion.averageDuration;
                    break;
                }
            }
        }
        //foreach (AnimationClip _clip in clips)
        //{

            
        //}
                
                    

        Debug.Log(_duration);
        return _duration;
    }

    public bool isWalking()
    {
        return animators[0].GetBool("isWalking");
    }

    public void TestJump(Vector3 _jumpTarget)
    {
        jumpTarget= _jumpTarget;

        foreach (Animator animator in animators)
        {
            Debug.Log(animator.avatar);
            animator.SetBool("isJumping", true);
            animator.SetFloat("jumpTargetX", _jumpTarget.x);
            animator.SetFloat("jumpTargetY", _jumpTarget.y);
            animator.SetFloat("jumpTargetZ", _jumpTarget.z);
        }
        cat_jump();

    }

}
