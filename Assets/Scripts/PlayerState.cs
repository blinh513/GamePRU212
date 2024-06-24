using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("State", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetInteger("State", 2);
            }
        }
    }
}
