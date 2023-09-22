using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody componentRigidbody;
    AudioSource componentAudioSource;
    [SerializeField] float thrustForce = 1000f; 
    [SerializeField] float rotationPerSecond = 90f;
    // Start is called before the first frame update
    void Start()
    {
        componentRigidbody = GetComponent<Rigidbody>();
        componentAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            componentRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if (!componentAudioSource.isPlaying)
            {
                componentAudioSource.Play();
            }
        }
        else 
        {
            componentAudioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if ((Input.GetKey(KeyCode.RightArrow)) && (Input.GetKey(KeyCode.LeftArrow)))
        {
            Debug.Log("ROTATION ERROR");
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ApplyRotation(-rotationPerSecond);
            };
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ApplyRotation(rotationPerSecond);
            };
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        componentRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;     //   .freezeRotation = true; //freezing rigidbogy rotation so we can rotate only MANNUALY
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        componentRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ; //unfreezing rigidbogy rotation
    }
}
