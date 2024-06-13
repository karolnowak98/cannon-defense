using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Data
{
    public class Entity : ScriptableObject
    {
        [SerializeField] protected int _id;

        public int ID => _id;
    }
}