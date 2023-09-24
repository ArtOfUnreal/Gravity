using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody componentRigidbody;
    AudioSource componentAudioSource;

    [SerializeField] float thrustForce = 1000f; 
    [SerializeField] float rotationPerSecond = 90f;

    [SerializeField] AudioClip mainEngineSFX;

    [SerializeField] ParticleSystem mainBoosterParticleSystem;
    [SerializeField] ParticleSystem leftBoosterParticleSystem;
    [SerializeField] ParticleSystem rightBoosterParticleSystem;

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
            StartMainThrusting();

        }
        else
        {
            StopMainThrusting();
        }
    }
    void StopMainThrusting()
    {
        componentAudioSource.Stop();
        mainBoosterParticleSystem.Stop();
    }

    void StartMainThrusting()
    {
        componentRigidbody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!componentAudioSource.isPlaying)
        {
            componentAudioSource.PlayOneShot(mainEngineSFX);
        }
        if (!mainBoosterParticleSystem.isPlaying)
        {
            mainBoosterParticleSystem.Play();
        }
    }

    public void StopAllBoosters()
    {
        StopMainThrusting();
        StopLeftBooster();
        StopRightBooster();
    }

    void ProcessRotation()
    {
        if ((Input.GetKey(KeyCode.RightArrow)) && (Input.GetKey(KeyCode.LeftArrow)))
        {
            StopRightBooster();
            StopLeftBooster();
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ActiveteRightBooster();
            }
            else
            {
                StopRightBooster();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ActivateLeftBooster();
            }
            else
            {
                StopLeftBooster();
            }
        }
    }

    void StopLeftBooster()
    {
        leftBoosterParticleSystem.Stop();
    }

    void ActivateLeftBooster()
    {
        ApplyRotation(rotationPerSecond);
        if (!leftBoosterParticleSystem.isPlaying)
        {
            leftBoosterParticleSystem.Play();
        }
    }

    void StopRightBooster()
    {
        rightBoosterParticleSystem.Stop();
    }

    void ActiveteRightBooster()
    {
        ApplyRotation(-rotationPerSecond);
        if (!rightBoosterParticleSystem.isPlaying)
        {
            rightBoosterParticleSystem.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        componentRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;     //   .freezeRotation = true; //freezing rigidbogy rotation so we can rotate only MANNUALY
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        componentRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ; //unfreezing rigidbogy rotation
    }
}
