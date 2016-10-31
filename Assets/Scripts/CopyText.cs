using UnityEngine;
using System.Collections;

public class CopyText : MonoBehaviour
{
    public TextMesh copyText;
    public TextMesh myText;
	// Use this for initialization
	void Start ()
    {
        myText = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        myText.text = copyText.text;
	}
}
