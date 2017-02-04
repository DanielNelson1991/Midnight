using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AnimationHandler : MonoBehaviour {

    private Animator anim;
    private AnimatorStateInfo stateInfo;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.SetBool("Act1-Scene1-Complete", true);
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}

    public void TurnOffAnim(string BooleanName)
    {
        anim.SetBool(BooleanName, false);
        anim.Stop();
    }


}
