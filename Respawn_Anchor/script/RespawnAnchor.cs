
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;

public class RespawnAnchor : UdonSharpBehaviour
{
    [Header("リスポーン地点")]
    [SerializeField] private Transform respawnPosition = null;
    Boolean respawn = false;

    public override void OnPlayerTriggerExit(VRCPlayerApi player){
        if (respawn){
            Vector3 respawnVec = respawnPosition.transform.position;
            var target = Networking.LocalPlayer;
            target.TeleportTo(respawnVec, Quaternion.Euler(respawnVec));
        }
    }
    public override void OnPlayerRespawn(VRCPlayerApi player){
        respawn = true;
        SendCustomEventDelayedFrames(nameof(end), 3);
    }   

    public void end(){
        respawn = false;
    }
}