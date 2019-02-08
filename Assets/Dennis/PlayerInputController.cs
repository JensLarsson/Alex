using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    private Dictionary<KeyCode, Command> OnKey = new Dictionary<KeyCode, Command>();

    // Use this for initialization
    void Start()
    {
        var movement = GetComponent<PlayerMovement>();

        var moveUpCommand = new ActionCommand(movement.SetVelocityUp);
        var moveDownCommand = new ActionCommand(movement.SetVelocityDown);
        var moveLeftCommand = new ActionCommand(movement.SetVelocityLeft);
        var moveRightCommand = new ActionCommand(movement.SetVelocityRight);

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
