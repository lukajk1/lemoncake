using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCData", menuName = "ScriptableObjects/NPCData")]
public class NPCData : ScriptableObject
{
    public string NPCName;
    public string[] Dialogues;

    [Range(0.5f, 1.5f)]
    public float VoicePitch = 1f;

    [Range(0.04f, 0.14f)]
    public float WordInterval = 0.08f;

    public Color NameColor = Color.white;

}
