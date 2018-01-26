using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class VibrateController : MonoBehaviour
{

    //Vibramos el mando 
    public void Rumble (int num, float eng1, float eng2)
    {
        GamePad.SetVibration(0, eng1, eng2);
    }
	
}
