using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Console : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    private PlayerLookAndMove playerController;
    private GameObject inputParent;

    private string lastCommand;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerLookAndMove>();

        inputParent = inputField.transform.parent.gameObject;
        inputParent.SetActive(false);
        inputField.onEndEdit.AddListener(OnSubmit);
    }

    void Update()
    {
        HandleKeyboardCommands();
    }

    private void HandleKeyboardCommands()
    {
        if (inputParent.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                inputParent.SetActive(false);
                Game.MenusOpen--;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                inputField.text = lastCommand;
                inputField.caretPosition = inputField.text.Length;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Slash))
        {
            Game.MenusOpen++;
            inputParent.SetActive(true);
            inputField.ActivateInputField();
        }
    }

    private void OnSubmit(string command)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (HandleCommand(command))
            {
                lastCommand = command;
            }

            inputField.text = ""; // Clear the input field
            inputParent.SetActive(false);
            Game.MenusOpen--;
        }
    }

    private string[] SplitCommand(string command)
    {
        return command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    }

    private bool HandleCommand(string commandArg)
    {
        string formatted = commandArg.Trim().ToLower();
        string[] parsedCommand = SplitCommand(formatted);

        if (parsedCommand.Length == 0)
        {
            Debug.LogWarning("Command cannot be empty.");
            return false;
        }

        if (parsedCommand.Length >= 3 && parsedCommand[0] == "player")
        {
            if (parsedCommand[1] == "speed")
            {
                if (float.TryParse(parsedCommand[2], out float speed))
                {
                    playerController.MoveSpeed = speed;
                    return true;
                }
                else
                {
                    Debug.LogWarning($"Invalid value for player speed: {parsedCommand[2]}");
                    return false;
                }
            }
            else if (parsedCommand[1] == "jump")
            {
                if (float.TryParse(parsedCommand[2], out float jumpForce))
                {
                    playerController.JumpForce = jumpForce;
                    return true;
                }
                else
                {
                    Debug.LogWarning($"Invalid value for player jump: {parsedCommand[2]}");
                    return false;
                }
            }
            else if (parsedCommand[1] == "values" && parsedCommand[2] == "reset")
            {
                playerController.ResetValues();
                return true;
            }
        }

        // otherwise
        Debug.LogWarning($"Unknown command: {formatted}");
        return false;
    }



}