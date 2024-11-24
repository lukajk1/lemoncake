using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private AudioSource voice;
    public override void OnInteract()
    {
        string[] dialogues = new string[] 
        {
            "Things come, things go, but not much changes.",
            "Such is life..."
        };

        DialogueManager.Instance.Open("Gabe", dialogues, voice);
    }
}
