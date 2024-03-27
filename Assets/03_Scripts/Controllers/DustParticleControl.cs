using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    [SerializeField] private bool createDustOnWalk = true; //걸을 때
    [SerializeField] private ParticleSystem dustParticleSystem;

    public void CreateDustParticles()
    {
        if (createDustOnWalk)
        {
            // 파티클시스템을 멈추고 새로 시작
            dustParticleSystem.Stop();
            dustParticleSystem.Play();
        }
    }
}