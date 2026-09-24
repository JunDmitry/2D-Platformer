using System.Collections;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public interface ISkillEffect
    {
        IEnumerator Run(ExecutorData executorData);
    }
}
