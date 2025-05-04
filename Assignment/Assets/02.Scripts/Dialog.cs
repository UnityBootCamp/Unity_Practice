using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Scriptable Objects/Dialog")]
public class Dialog : ScriptableObject
{
    [field: SerializeField] public string nameOfSong { get; set; }


    [field: SerializeField] public string correctChoice { get; set; }
    [field: SerializeField] public string[] wrongChoices { get; set; }


    [TextArea][SerializeField] public string lylicsOfSong1;
    [TextArea][SerializeField] public string lylicsOfSong2;

}
