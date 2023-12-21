using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyholder : MonoBehaviour
{
    private List<key.Keytype> keyList;

    private void Awake()
    {
        keyList = new List<key.Keytype>();
    }

    public void AddKey(key.Keytype keytype)
    {
        keyList.Add(keytype);
    }

    public void RemoveKey(key.Keytype keytype)
    {
        keyList.Remove(keytype);
    }

    public bool ContainsKey(key.Keytype keytype)
    {
        return keyList.Contains(keytype);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        key key = GetComponent<Collider>().GetComponent<key>();
        if (key != null)
        {
            AddKey(key.GetKeytype());
        }
    }
}
