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
        string[] parsed = SplitCommand(formatted);

        if (parsed.Length == 0)
        {
            Debug.LogWarning("Command cannot be empty.");
            return false;
        }

        if (parsed.Length >= 3 && (parsed[0] == "player" || parsed[0] == "p"))
        {
            if (parsed[1] == "speed" || parsed[1] == "s")
            {
                if (float.TryParse(parsed[2], out float speed))
                {
                    playerController.MoveSpeed = speed;
                    return true;
                }
                else
                {
                    Debug.LogWarning($"Invalid value for player speed: {parsed[2]}");
                    return false;
                }
            }
            else if (parsed[1] == "jump" || parsed[1] == "j")
            {
                if (float.TryParse(parsed[2], out float jumpForce))
                {
                    playerController.JumpForce = jumpForce;
                    return true;
                }
                else
                {
                    Debug.LogWarning($"Invalid value for player jump: {parsed[2]}");
                    return false;
                }
            }
            else if ((parsed[1] == "values" || parsed[1] == "v") && (parsed[2] == "reset" || parsed[2] == "r"))
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