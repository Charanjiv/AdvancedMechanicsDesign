using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;
using Unity.Physics.Extensions;

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
            LookupPlayerTag = state.GetComponentLookup<PlayerTag>();
            LookupPlayerTrigger = state.GetComponentLookup<TriggerTag>();
            deltaTime = SystemAPI.Time.DeltaTime
        }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);   

    }


    private partial struct TriggerDebugJob : ITriggerEventJob
    {
        public float deltaTime;
        [ReadOnly] public ComponentLookup<PlayerTag> LookupPlayerTag;
        [ReadOnly] public ComponentLookup<TriggerTag> LookupTriggerTag;

    public void Execute(TriggerEvent triggerEvent)
        {
            bool isBodyAPlayer = LookupPlayerTag.HasComponent(triggerEvent.EntityA);
            bool isBodyBPlayer = LookupPlayerTag.HasComponent(triggerEvent.EntityB);

            if(!isBodyAPlayer && !isBodyBPlayer) { return; }

            bool isBodyATrigger = LookupPlayerTag.HasComponent(triggerEvent.EntityA);
            bool isBodyBTrigger = LookupPlayerTag.HasComponent(triggerEvent.EntityB);

            if (!isBodyATrigger && !isBodyBTrigger) { return; }

            Entity playerEntity = isBodyAPlayer ? triggerEvent.EntityA : triggerEvent.EntityB;
            PhysicsVelocity playerVel = LookupPhysicsVelocity[playerEntity];
            playerVel.ApplyLinearImpulse(LookupPhysicsVelocity)
            UnityEngine.Debug.Log(triggerEvent.EntityA);
        }
    }
}
