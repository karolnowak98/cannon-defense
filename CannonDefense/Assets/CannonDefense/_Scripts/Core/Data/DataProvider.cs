using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Data
{
    public class DataProvider : IDataProvider
    {
        private readonly Dictionary<Type, ConfigData> _configs = new();
        private readonly Dictionary<Type, EntityData[]> _entities = new();

        [Inject]
        private void Construct(DataHolder dataHolder)
        {
            dataHolder.RegisterAll(this);
        } 

        public T GetConfig<T>() where T : ConfigData
        { 
            var type = typeof(T);
            
            if(_configs.TryGetValue(type, out var result)) 
                return (T)result;
            
            Debug.LogWarning("Didn't find config with type: " + type);
            return null;
        }
        
        public void SetConfig<T>(ConfigData config) where T : ConfigData
        { 
            var type = typeof(T);
            SetConfig(type, config);
        }
        
        public void SetConfig(Type type, ConfigData config)
        {
            if (_configs.TryAdd(type, config)) 
                return;
            
            Debug.LogWarning("Data provider already has config with type: " + type);
        }

        public T GetEntity<T>(int id) where T : EntityData
        {
            var entities = GetEntities<T>();
            
            if(entities != null) return entities[id];
            
            Debug.LogWarning("DataProvider.GetEntity null id=" + id + " " + typeof(T));
            return null;
        }

        public T[] GetEntities<T>() where T : EntityData
        {
            var type = typeof(T);
            return Array.ConvertAll(_entities[type], x => (T)x);
        }

        public void SetEntities<T>(T[] entities) where T : EntityData
        {
            var type = typeof(T);
            SetEntities(type, entities);
        }
        
        public void SetEntities<T>(Type type, T[] entities) where T : EntityData
        {
            if(entities == null || entities.Length == 0)
            {
                Debug.LogWarning("DataProvider.SetEntities is empty for " + type);
            } 
            else 
            {
                var maxID = entities.Select(t => t.ID).Prepend(0).Max();

                var all = new EntityData[maxID+1];

                foreach (var entity in entities)
                {
                    var id = entity.ID;
                    all[id] = entity;
                }

                _entities.Add(type, all);
            }
        }
    }
}