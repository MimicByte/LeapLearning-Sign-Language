using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class GestureDetection : MonoBehaviour {

    public float accuracy;
    public Material red;
    public Material green;
    LeapProvider leapController;
    public GameObject leap;
    public GameObject left;
    public GameObject right;
    public bool both = false;

    GameObject leftPalm;
    GameObject rightPalm;
    List<GameObject> leftFingers;
    List<GameObject> rightFingers;

    Frame frame;

    private void Start()
    {
        Begin();
    }

    // Use this for initialization
    public void Begin () {
        leapController = leap.GetComponent<LeapServiceProvider>();
        frame = leapController.CurrentFrame;

        if (left == null && GameObject.FindGameObjectWithTag("left") != null)
        {

            left = GameObject.FindGameObjectWithTag("left");
            leftFingers = new List<GameObject>();

            leftPalm = left.transform.GetChild(0).gameObject;
            leftPalm = leftPalm.transform.GetChild(0).gameObject;

            
            leftFingers.Add(leftPalm.transform.GetChild(0)
                   .transform.GetChild(0)
                   .transform.GetChild(0)
                   .transform.GetChild(0).gameObject);

            

            for (int i = 1; i < 5; i++)
            {
                leftFingers.Add(leftPalm.transform.GetChild(i)
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .transform.GetChild(0).gameObject);
            }
        }

        if (right == null && GameObject.FindGameObjectWithTag("right") != null)
        {

            right = GameObject.FindGameObjectWithTag("right");

            rightFingers = new List<GameObject>();

            rightPalm = right.transform.GetChild(0).gameObject;
            rightPalm = rightPalm.transform.GetChild(0).gameObject;





            rightFingers.Add(rightPalm.transform.GetChild(0)
                .transform.GetChild(0)
                .transform.GetChild(0)
                .transform.GetChild(0).gameObject);



            for (int i = 1; i < 5; i++)
            {


                rightFingers.Add(rightPalm.transform.GetChild(i)
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .transform.GetChild(0)
                    .transform.GetChild(0).gameObject);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        Begin();

        Debug.Log(left);
        Debug.Log(right);

        frame = leapController.CurrentFrame;
        List<Hand> hands = frame.Hands;

        bool leftMatch = false;
        bool rightMatch = false;

        foreach (Hand hand in hands)
        {
            if (hand.IsLeft)
            {
                if (left != null)
                {
                    leftMatch = true;
                    if (hand.WristPosition.DistanceTo(new Vector(leftPalm.transform.position.x, leftPalm.transform.position.y, leftPalm.transform.position.z)) > accuracy)
                    {
                        leftMatch = false;
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (hand.Fingers[i].TipPosition.DistanceTo(new Vector(leftFingers[i].transform.position.x, leftFingers[i].transform.position.y, leftFingers[i].transform.position.z)) > accuracy)
                            {
                                leftMatch = false;
                                break;
                            }
                        }
                    }
                }
                
            }
            if (!hand.IsLeft)
            {
                if (right != null)
                {
                    rightMatch = true;
                    if (hand.WristPosition.DistanceTo(new Vector(rightPalm.transform.position.x, rightPalm.transform.position.y, rightPalm.transform.position.z)) > accuracy)
                    {
                        rightMatch = false;
                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (hand.Fingers[i].TipPosition.DistanceTo(new Vector(rightFingers[i].transform.position.x, rightFingers[i].transform.position.y, rightFingers[i].transform.position.z)) > accuracy)
                            {
                                rightMatch = false;
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (left != null)
        {
            if (leftMatch)
            {
                left.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = green;
            }
            else
            {
                left.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = red;
            }
        }
        if (right != null)
        {
            if (rightMatch)
            {
                right.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = green;
            }
            else
            {
                right.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = red;
            }
        }
        if (leftMatch && rightMatch)
        {
            both = true;
        } else
        {
            both = false;
        }
    }
}
