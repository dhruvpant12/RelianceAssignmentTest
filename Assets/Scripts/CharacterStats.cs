using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
   public int healthOfPlayer ; 
    // Start is called before the first frame update
    void Start()
    {
        healthOfPlayer = 3;//If it becomes zero , game is over.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
