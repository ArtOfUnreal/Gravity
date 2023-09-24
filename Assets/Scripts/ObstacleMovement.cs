using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    Vector3 startPosition;
    float positionFactor;

    [SerializeField] Vector3 offsetPosition;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } //Mathf.Epsilon is eqivalent of 0
        //calculate current offset based on time and period
        float cycles = Time.time / period; //current cycle of time
        const float tau=Mathf.PI*2;  //constant value of ~6.283
        float rawSinWave = Mathf.Sin(cycles*tau); //sin vawe from -1 to 1
        positionFactor = (rawSinWave + 1f) / 2f; //recalculate vawe to go from 0 to 1
        Vector3 currentOffset = offsetPosition * positionFactor; //calculate current offset
        
        //apply offset to position
        transform.position = startPosition + currentOffset;

        transform.Rotate(Random.Range(0, 0.2f), Random.Range(0, 0.2f), Random.Range(0, 0.2f));

    }
}
