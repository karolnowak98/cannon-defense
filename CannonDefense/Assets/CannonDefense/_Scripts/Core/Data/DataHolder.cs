using System;
using System.Collections.Generic;
using System.Linq;
using GlassyCode.CannonDefense.Core.Utility.Static;
using UnityEditor;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Data
{
    [CreateAssetMenu(fileName = "DataHolder", menuName = "Data/DataHolder")]
    public class DataHolder : ScriptableObject
    {
        private const string DataPath = "Assets/CannonDefense/Data";
        
        [SerializeField] private ScriptableObject[] _allData;

#if UNITY_EDITOR
        [ContextMenu("Find all Data")]
        public void FindAll()
        {
            _allData = AssetDatabase.FindAssets($"t:{nameof(ScriptableObject)}", new[] { DataPath })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<ScriptableObject>)
                .Where(so => so != null && (TypeUtils.IsOfTypeOrDerived(so.GetType(), typeof(Config)) || TypeUtils.IsOfTypeOrDerived(so.GetType(), typeof(EntityData))))
                .ToArray();

            EditorUtility.SetDirty(this);
        }
#endif
        public void RegisterAll(DataProvider provider)
        {
            #if UNITY_EDITOR
            FindAll();
            #endif

            var entityDataType = typeof(EntityData);
            var configDataType = typeof(Config);

            var entities = new Dictionary<Type, List<EntityData>>();

            if(_allData != null)
            {
                foreach(var dataElement in _allData)
                {
                    var type = dataElement.GetType();
                    
                    if (type.BaseType == configDataType)
                    {
                        provider.SetConfig(type, (Config)dataElement);
                    } 
                    else if (type.BaseType == entityDataType)
                    {
                        if (!entities.ContainsKey(type))
                        {
                            entities.Add(type, new List<EntityData>());
                        }
                        entities[type].Add((EntityData)dataElement);
                    }
                }
            }
            
            foreach(var entitiesByType in entities)
            {
                provider.SetEntities(entitiesByType.Key, entitiesByType.Value.ToArray());
            }
        }
    }
}