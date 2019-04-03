using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunPractice : MonoBehaviour {

    
    public List<GameObject> signsL;
    public List<GameObject> signsR;
    public GameObject manager;
    int index;
    int wait = 0;

    // Use this for initialization
    void Start () {
        index = 1;

		if (signsL[0] != null)
        {
            Instantiate(signsL[0]);
        }
        if (signsR[0] != null)
        {
            Instantiate(signsR[0]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (manager.GetComponent<GestureDetection>().both)
        {
            Destroy(GameObject.FindGameObjectWithTag("left"));
            Destroy(GameObject.FindGameObjectWithTag("right"));

            if (!(index >= signsL.Count)) {
                if (signsL[index] != null)
                {
                    Instantiate(signsL[index]);
                }
                if (signsR[index] != null)
                {
                    Instantiate(signsR[index]);
                }
                wait += 1;
                if (wait > 5)
                {
                    wait = 0;
                    index += 1;

                }
            }
        }
	}
}
