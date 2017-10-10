using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour {


    public AudioClip JumpSound;

    public float JumpMagnitude = 20f;

    public void ControllerEnter2D(CharacterController2D controller)
    {
        AudioSource.PlayClipAtPoint(JumpSound, gameObject.transform.position);

        controller.SetVerticalForce(JumpMagnitude);
    }

}
