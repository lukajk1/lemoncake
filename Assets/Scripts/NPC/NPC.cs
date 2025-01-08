using UnityEngine;

public class NPC : Interactable
{
    [SerializeField] private AudioSource voice;

    [SerializeField] private NPCDialogueData.NPC npc;

    [Range(0.5f, 1.5f)]
    public float VoicePitch = 1f;

    [Range(0.04f, 0.14f)]
    public float WordInterval = 0.08f;

    public Color NameColor = Color.white;
    private int dialogueIndex = 0;
    public override void OnInteract()
    {
        int randomIndex = Random.Range(0, 2);

        DialogueManager.Instance.Open(NPCDialogueData.GetName(npc), NPCDialogueData.GetDialogue(npc)[dialogueIndex], voice, VoicePitch, WordInterval, NameColor, this);

    }

    public void OnClose()
    {
        if (dialogueIndex < NPCDialogueData.GetDialogue(npc).Length - 1)
        {
            dialogueIndex++;
        }
        else
        {
            dialogueIndex = 0;
        }
    }
}
