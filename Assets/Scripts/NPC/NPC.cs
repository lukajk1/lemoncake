using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private AudioSource voice;
    [SerializeField] private NPCData d;
    public override void OnInteract()
    {
        DialogueManager.Instance.Open(d.NPCName, d.Dialogues, voice, d.VoicePitch, d.WordInterval, d.NameColor);
    }
}
