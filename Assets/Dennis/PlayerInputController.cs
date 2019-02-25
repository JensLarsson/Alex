using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    private Dictionary<KeyCode, Command> OnKey = new Dictionary<KeyCode, Command>();
    PlayerMovement playerMovement;

    // Use this for initialization
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        var moveUpCommand = new ActionCommand(playerMovement.SetVelocityUp);
        var moveDownCommand = new ActionCommand(playerMovement.SetVelocityDown);
        var moveLeftCommand = new ActionCommand(playerMovement.SetVelocityLeft);
        var moveRightCommand = new ActionCommand(playerMovement.SetVelocityRight);

        OnKey.Add(KeyCode.W, moveUpCommand);
        OnKey.Add(KeyCode.S, moveDownCommand);
        OnKey.Add(KeyCode.A, moveLeftCommand);
        OnKey.Add(KeyCode.D, moveRightCommand);
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        foreach (KeyCode key in OnKey.Keys)
        {
            if (Input.GetKey(key))
            {
                OnKey[key].Execute();
            }
        }
    }

}
