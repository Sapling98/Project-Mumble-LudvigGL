using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    [SerializeField] private Keytype keytype;

    public enum Keytype
    {
        Red,
        Green,
        Blue,
    }

    public Keytype GetKeytype() 
    { 
        return keytype; 
    }
}
