using Assets.Scripts.Entities.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Cream/Database/Descriptions/New character description")]
public class CharacterDescription : ObjectDescription
{
    [HideInInspector] public PlayerSkill[] Skills;
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }
    [field: SerializeField] public Sprite CharacterButtonIcon { get; private set; }
    public void Init(PlayerSkill[] skills)
    {
        Skills = skills;
    }
}
