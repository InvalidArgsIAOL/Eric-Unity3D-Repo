using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// Because of the nature of our InputReader, we have to manually calculate physics for our character.
/// This can be circumvented if we used the rigibody system.
/// 
/// </summary>
public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;


    private float verticalVelocity;

    [SerializeField]
    private float drag = 0.3f;

    private Vector3 dampingVelocity;

    private Vector3 impact; // Knockback, moving forward
    public Vector3 Movement => impact + Vector3.up * verticalVelocity; // 0, 1, 0 vector return


    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime; // setting it
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime; // Gravity when not on ground
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag); // SmoothDamp prevents flying off into the sunset when hit
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

}
