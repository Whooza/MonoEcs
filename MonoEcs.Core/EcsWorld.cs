global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Content;
global using Microsoft.Xna.Framework.Graphics;
global using MonoEcs.Core.Components;
global using MonoEcs.Core.Entities;
global using MonoEcs.Core.Interfaces;
global using MonoEcs.Core.Systems.Bases;
global using MonoEcs.Core.Systems.Entity;
global using MonoEcs.Core.Systems.Global;
global using System;
global using System.Collections.Generic;

namespace MonoEcs.Core;

public class EcsWorld
{
    private readonly EcsContainer _systemContainer;
    private readonly IEcsEntity[] _entities;
    private readonly int _maxEntityCount;

    public EcsWorld(Game game, int maxEntitiyCount)
    {
        _maxEntityCount = maxEntitiyCount;
        _systemContainer = new EcsContainer(game, maxEntitiyCount);
        _entities = new IEcsEntity[maxEntitiyCount];

        for (int i = 0; i < maxEntitiyCount; i++)
        {
            _entities[i] = new EcsEntity(0, "empty", false, false);
        }
    }

    #region Entities

    public TEntity CreateEntity<TEntity>(string name) where TEntity : IEcsEntity
    {
        lock (_entities)
        {
            for (int i = 0; i < _maxEntityCount; i++)
            {
                if (!_entities[i].Exists)
                {
                    _entities[i] = new EcsEntity(i, name);
                    return (TEntity)_entities[i];
                }
            }
        }

        throw new Exception("no free entity slot");
    }

    public void UpdateEntity(IEcsEntity entity)
    {
        _entities[entity.Id] = entity;
    }

    public void AddEntityToSystem<TSystem, TComponent>(IEcsEntity entity, TComponent component) where TSystem : IEcsEntitySystem where TComponent : IEcsComponent
    {
        _systemContainer.GetEntitySystem<TSystem>().AddComponent(entity, component);
    }

    public void DestroyEntity(IEcsEntity entity)
    {
        entity.Exists = false;
        _entities[entity.Id] = entity;
    }

    public void DestroyEntity(int entityId)
    {
        IEcsEntity entity = _entities[entityId];
        entity.Exists = false;
        _entities[entityId] = entity;
    }

    #endregion

    #region Systems

    public void ToggleGlobalSystem<TSystem>(bool toggle) where TSystem : IEcsGlobalSystem
    {
        _systemContainer.GetGlobalSystem<TSystem>().Enabled = toggle;
    }

    public void ToggleEntitySystem<TSystem>(bool toggle) where TSystem : IEcsEntitySystem
    {
        _systemContainer.GetEntitySystem<TSystem>().Enabled = toggle;
    }

    #endregion

    #region MonoGame

    public void Initialize()
    {
        _systemContainer.Initialize();
    }

    public void LoadContent()
    {
        _systemContainer.LoadContent();
    }

    public void Update(float deltaTime)
    {
        _systemContainer.Update(_entities, deltaTime);
    }

    public void Draw(float deltaTime)
    {
        _systemContainer.Draw(_entities, deltaTime);
    }

    #endregion
}
