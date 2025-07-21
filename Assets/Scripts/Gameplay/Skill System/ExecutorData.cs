using UnityEngine;

namespace Assets.Scripts.Gameplay.Skill_System
{
    public struct ExecutorData
    {
        public readonly int Id;
        public readonly IDamageable ExecutorDamageable;
        public readonly IReplenishable ExecutorReplenishable;
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public ExecutorData(GameObject executor, IDamageable damageable = null, IReplenishable replenishable = null)
        {
            Id = executor.GetInstanceID();
            ExecutorDamageable = damageable;
            ExecutorReplenishable = replenishable;
            Position = executor.transform.position;
            Rotation = executor.transform.rotation;
        }

        public static bool operator ==(ExecutorData left, ExecutorData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ExecutorData left, ExecutorData right)
        {
            return left.Equals(right) == false;
        }

        public override readonly bool Equals(object obj)
        {
            if (obj == null || obj is not ExecutorData data)
                return false;

            return Equals(data);
        }

        public readonly bool Equals(ExecutorData data)
        {
            return Id == data.Id;
        }

        public override readonly int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}