using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDestroyer : MonoBehaviour
{
    public float destroyTime = 0f;
	// Use this for initialization
    //Destruimos el popup al pasar el tiempo indicado
	void Start ()
    {
        Destroy(gameObject, destroyTime);
	}
	
	
}
