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


        if (Input.GetKey(KeyCode.BackQuote) && Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            leftIndexTip = rigidIndexL.GetTipPosition();
            rightIndexTip = rigidIndexR.GetTipPosition();
            Vector3 middlePos = (rightIndexTip + leftIndexTip) / 2;
            middlePos += new Vector3(0.42f, 0.19f,- 0.03600f);
            print(leftIndexTip + " : " + rightIndexTip + " ; " + middlePos);

            float fingerDist = Vector3.Distance(leftIndexTip, rightIndexTip);
            fingerDist -= .15f;
            fingerDist = .226f;
            keyboard.transform.localScale = new Vector3(fingerDist+.02f, fingerDist, fingerDist);

            Vector3 targetDir = leftIndexTip - rightIndexTip;
            float keyboardAngle = Vector3.Angle(targetDir, transform.forward);

           //middlePos.y = -0.23f;
            keyboard.transform.position = middlePos;
            
            keyboard.transform.eulerAngles = new Vector3(0, -(keyboardAngle - 90), 0);
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.localScale = new Vector3(.05f, .05f, .05f);
            //cube.transform.position = middlePos;
        }
        // leftIndexTip = finger.TipPosition;

    }
}
