using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : Interactable
{
    [SerializeField]
    private GameObject door1;
    private bool door1Open;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function is where will design our interaction using code.
    protected override void Interact()
    {
        // เปิดปิดประตู door
        door1Open = !door1Open;
        door1.GetComponent<Animator>().SetBool("IsOpen", door1Open);
    }
}