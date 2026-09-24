using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    [CreateAssetMenu(fileName = "SkillConfig", menuName = "Skills/Skill Config")]
    public class SkillConfig : ScriptableObject
    {
        [SerializeField, Min(0f)] private float _durationInSeconds;
        [SerializeField, Min(0f)] private float _cooldownInSeconds;

        public float DurationInSeconds => _durationInSeconds;

        public float CooldownInSeconds => _cooldownInSeconds;
    }
}
