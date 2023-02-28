using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Handler : MonoBehaviour
{
    
    public Transform hand;

    public float handRotation;
    public float currentRotation;

    bool start = false;

    Vector3 rotation = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        Invoke("startNow", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (start)
        {
            handRotation = hand.rotation.x;

            float dif;

            if (currentRotation > handRotation)
            {
                //Debug.Log("ONE RAN YIPEE");
                dif = (currentRotation - handRotation);
                dif = dif * 150;
            }
            else
            {
                //Debug.Log("TWO RAN YAHOO");

                dif = handRotation - currentRotation;
                dif = dif * -150;
            }

            Debug.Log(dif);

            currentRotation = handRotation;


            transform.Rotate(dif, 0, 0);
        }
    }

    public void startNow()
    {
        currentRotation = hand.rotation.x;
        handRotation = hand.rotation.x;
        rotation.z = transform.rotation.z;
        rotation.y = transform.rotation.y;
        rotation.x = transform.rotation.x;
        start = true;
    }
}
