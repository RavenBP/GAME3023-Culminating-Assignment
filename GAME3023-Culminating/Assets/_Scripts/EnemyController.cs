using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    AnimationClip GolemAnimation;

    [SerializeField]
    AnimationClip DemonAnimation;

    [SerializeField]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0.0f, 10.0f) > 5.0f)
        {
            animator.Play("Demon");
            Debug.Log("Playing Demon Animation Clip");
        }
        else
        {
            animator.Play("Golem");
            Debug.Log("Playing Golem Animation Clip");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
