using UnityEngine;
using System.Collections;
using Leap;
using Leap.Unity;

public class KeyboardResetPos : MonoBehaviour
{
    Vector3 leftIndexTip;
    Vector3 rightIndexTip;
    RigidFinger rigidIndexL;
    RigidFinger rigidIndexR;

    GameObject keyboard;
    
    // Use this for initialization
    void Start ()
    {
        rigidIndexL = transform.GetChild(0).GetChild(1).GetComponent<RigidFinger>();
        rigidIndexR = transform.GetChild(1).GetChild(1).GetComponent<RigidFinger>();
        keyboard = GameObject.FindGameObjectWithTag("Keyboard");
    }
	
	// Update is called once per frame
	void Update ()
    {


        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.KeypadEnter))
        {
            leftIndexTip = rigidIndexL.GetTipPosition();
            rightIndexTip = rigidIndexR.GetTipPosition();
            Vector3 middlePos = (rightIndexTip + leftIndexTip) / 2;
            print(leftIndexTip + " : " + rightIndexTip + " ; " + middlePos);
            middlePos += new Vector3(0.5f, 0.1f, 0);

            keyboard.transform.position = middlePos;
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.localScale = new Vector3(.05f, .05f, .05f);
            //cube.transform.position = middlePos;
        }
        // leftIndexTip = finger.TipPosition;

    }
}
