using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Physics.Extensions;
using Unity.Mathematics;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(PhysicsSystemGroup))]   
[BurstCompile]
public partial struct TriggerDebugSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Dependency = new TriggerDebugJob
        {
            LookupPhysicsVelocity = state.GetComponentLookup<PhysicsVelocity>(),
            LookupPhysicsMass = state.GetComponentLookup<PhysicsMass>(),
            LookupPlayerTag = state.GetComponentLookup<PlayerTag>(),
            LookupTriggerTag = state.GetComponentLookup<TriggerTag>(),
            deltaTime = SystemAPI.Time.DeltaTime
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);   

    }


    private partial struct TriggerDebugJob : ITriggerEventsJob
    {
        public float deltaTime;
        public ComponentLookup<PhysicsVelocity> LookupPhysicsVelocity;
        [ReadOnly] public ComponentLookup<PhysicsMass> LookupPhysicsMass;
        [ReadOnly] public ComponentLookup<PlayerTag> LookupPlayerTag;
        [ReadOnly] public ComponentLookup<TriggerTag> LookupTriggerTag;
    

    public void Execute(TriggerEvent triggerEvent)
    {
        bool isBodyAPlayer = LookupPlayerTag.HasComponent(triggerEvent.EntityA);
        bool isBodyBPlayer = LookupPlayerTag.HasComponent(triggerEvent.EntityB);

        if (!isBodyAPlayer && !isBodyBPlayer) { return; }

        bool isBodyATrigger = LookupPlayerTag.HasComponent(triggerEvent.EntityA);
        bool isBodyBTrigger = LookupPlayerTag.HasComponent(triggerEvent.EntityB);

        if (!isBodyATrigger && !isBodyBTrigger) { return; }

        Entity playerEntity = isBodyAPlayer ? triggerEvent.EntityA : triggerEvent.EntityB;

        PhysicsVelocity playerVel = LookupPhysicsVelocity[playerEntity];
        playerVel.ApplyLinearImpulse(LookupPhysicsMass[playerEntity], math.up() * 100f * deltaTime);
        LookupPhysicsVelocity[playerEntity] = playerVel;
            //UnityEngine.Debug.Log(triggerEvent.EntityA);
            UnityEngine.Debug.Log(playerEntity);
        }
    }
}
