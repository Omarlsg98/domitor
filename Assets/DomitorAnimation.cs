using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomitorAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator domAnimator;

    void Start()
    {
     domAnimator = gameObject.transform.gameObject.GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Attack1")||Input.GetButtonDown("Attack2")||Input.GetButtonDown("Attack3")||Input.GetButtonDown("Attack4")) { 
           // domAnimator.SetTrigger("Attack01");
        //}
  
    }
}

