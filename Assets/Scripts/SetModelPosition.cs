using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class SetModelPosition : MonoBehaviour
{

    LeapProvider leapController;
    public GameObject leap;
    public GameObject left;
    public GameObject right;

    int x;
    int y;
    int z;

    Frame frame;

    // Use this for initialization
    void Start()
    {
        leapController = leap.GetComponent<LeapServiceProvider>();
        frame = leapController.CurrentFrame;
        x = 0;
        y = 0;
        z = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("q")) x += 90;
        if (Input.GetKeyDown("a")) x -= 90;
        if (Input.GetKeyDown("w")) y += 90;
        if (Input.GetKeyDown("s")) y -= 90;
        if (Input.GetKeyDown("e")) z += 90;
        if (Input.GetKeyDown("d")) z -= 90;
        if (Input.GetKeyDown("p")) Debug.Log(x + "," + y + "," + z);

        if (Input.GetKeyDown("space"))
        {
            frame = leapController.CurrentFrame;
            List<Hand> hands = frame.Hands;
            

            foreach (Hand hand in hands)
            {
                if (hand.IsLeft)
                {
                    left.transform.position = hand.WristPosition.ToVector3();
                    left.transform.rotation = hand.Rotation.ToQuaternion();
                    left.transform.Rotate(-90, 180, 0, Space.Self);

                    for (int j = 0; j < 5; j++)
                    {
                        GameObject temp = left.transform.GetChild(0).GetChild(0).GetChild(j).gameObject;
                        
                        for (int i = 0; i < 3; i++)
                        {
                            if (j != 0 || i > 0)
                            {
                                temp = temp.transform.GetChild(0).gameObject;
                                temp.transform.position = hand.Fingers[j].bones[i + 1].PrevJoint.ToVector3();
                                temp.transform.rotation = hand.Fingers[j].bones[i + 1].Rotation.ToQuaternion();
                                temp.transform.Rotate(180, 90, 0, Space.Self);
                            }
                        }
                        
                        temp.transform.position = hand.Fingers[j].bones[3].NextJoint.ToVector3();
                        temp.transform.GetChild(0).Rotate(180, 90, 0, Space.Self);
                        
                    }

                }
                if (!hand.IsLeft)
                {
                    right.transform.position = hand.WristPosition.ToVector3();
                    right.transform.rotation = hand.Rotation.ToQuaternion();
                    right.transform.Rotate(-90, 180, 0, Space.Self);

                    for (int j = 0; j < 5; j++)
                    {
                        GameObject temp = right.transform.GetChild(0).GetChild(0).GetChild(j).gameObject;
                        for (int i = 0; i < 3; i++)
                        {
                            if (j != 0 || i > 0)
                            {
                                temp = temp.transform.GetChild(0).gameObject;
                                temp.transform.position = hand.Fingers[j].bones[i + 1].PrevJoint.ToVector3();
                                temp.transform.rotation = hand.Fingers[j].bones[i + 1].Rotation.ToQuaternion();
                                temp.transform.Rotate(0, -90, 0, Space.Self);
                            }
                        }
                        temp.transform.position = hand.Fingers[j].bones[3].NextJoint.ToVector3();
                        temp.transform.GetChild(0).Rotate(0, -90, 0, Space.Self);
                    }
                }
            }
        }
    }
}
