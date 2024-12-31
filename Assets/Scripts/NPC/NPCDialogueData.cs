using UnityEngine;

public class NPCDialogueData : MonoBehaviour
{
    public enum NPC
    {
        Gilbert,
        Dennis,
        Mo, 
        Archibald,
        Barney,
        Bartholomew
    }
    public static string[][] GetDialogue(NPC npc)
    {
        switch (npc)
        {
            case NPC.Barney:
                return new string[][]
                    {
                        new string[] { "Y'know, the Earth didn't always look like this.",
                            "Most of the surface was solid ground at one point." },
                        new string[] { "But one day the rain just kept coming down...  and it never stopped." },
                        new string[] { "If I could do it all over again I'd take better care of my knees." }
                    };
            case NPC.Gilbert:
                return new string[][]
                    {
                        new string[] { "Y'know, the Earth didn't always look like this.",
                            "Most of the surface was solid ground at one point." },
                        new string[] { "But one day the rain just kept coming down...  and it never stopped." },
                        new string[] { "If I could do it all over again I'd take better care of my knees." }
                    };
            case NPC.Dennis:
                return new string[][]
                    {
                        new string[] { "You wouldn't happen to have a cigarette...?" }
                    };
            case NPC.Bartholomew:
                return new string[][]
                    {
                        new string[] { "Logic must just be an apprehension of our material observations. " },
                        new string[] { "\"Our minds can create new ideas from the components which experience has already given us, by combining together our existing ideas in new ways or by shuffling the components of our existing ideas,", "but we are quite unable to form any completely new ideas beyond those that have already been given to us by sensation or feeling.\"" },
                        new string[] { "Isn't the statement 'everything is meaningless' an oxymoron?" },
                        new string[] { "A purely subjective foundation for meaning is no less fulfilling." }
                    };
            case NPC.Mo:
                return new string[][]
                    {
                        new string[] { "Mo" }
                    };
            case NPC.Archibald:
                return new string[][]
                    {
                        new string[] { "Keep an eye on Levi over there, would you? Poor kid always insists on more alchohol than he can take." }
                    };
            default:
                return new string[][]
                    {
                        new string[] { "(Dialogue was not found. If you're seeing this something has gone horribly wrong)" }
                    };

        }
    }
}
