using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class guardAI : MonoBehaviour
{
    public Transform[] targets;
    private AICharacterControl aiCharControl;
    private float minDist = 3;
    int i = 0;

    // Line of sight variables
    private float fov = 360.0f;
    private float sightDist = 20.0f;
    private RaycastHit hit;
    private CommandReader cmdReader;
    public GameObject cube;


    // Use this for initialization
    void Start()
    {
        cmdReader = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<CommandReader>();
        aiCharControl = transform.GetComponent<AICharacterControl>();

        if (targets.Length > 0)
        {
            i = 0;
            aiCharControl.target = targets[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(aiCharControl.transform.position, aiCharControl.target.position) < minDist)
        {
           if (i < (targets.Length - 1))
           {
               i++;
               aiCharControl.target = targets[i];
           }
           else
           {
               i = 0;
               aiCharControl.target = targets[i];
           }
        }

        if (LOS(cmdReader.slaveCam.transform) == true)
        {
           print("Game Over\n");
            Time.timeScale = 0;
        }

        //else
          // print("Not Over\n");

    }

    // Line of sight script
    //bool LOS(Transform target)
    //{
    //    if (Vector3.Angle(target.position - transform.position, transform.forward) <= fov &&
    //        Physics.Linecast(transform.position, target.position, out hit) &&
    //        hit.collider.transform == target && Vector3.Distance(transform.position, target.position) < sightDist)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    bool LOS(Transform target)
    {
        if (Vector3.Angle(target.position - transform.position, transform.forward) <= fov && Physics.Linecast(transform.position, target.position, out hit) &&  hit.collider.transform == target)
        {
            return true;
        }

        print(Vector3.Angle(target.position - transform.position, transform.forward) + " : " +  fov + " | " + Physics.Linecast(transform.position, target.position, out hit) + " | " + hit.collider.transform + " : " + target);
        return false;
    }

}