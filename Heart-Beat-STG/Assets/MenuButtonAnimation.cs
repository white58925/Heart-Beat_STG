using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonAnimation : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] UIButton uiButtonType;

    void Update()
    {
        if (UIController.Instance.Index == (int)uiButtonType)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {   
                animator.SetBool("pressed", true);
            }
            else if (animator.GetBool("pressed"))
            {
                UIController.Instance.UIButtonClick(uiButtonType);
                animator.SetBool("pressed", false);
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
}
