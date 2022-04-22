
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class RespawnAnchor : UdonSharpBehaviour
{
    [Header("リスポーン地点")]
    [SerializeField] private Transform respawnPosition = null;
    
    bool respawn = false;
    VRCPlayerApi spawnp;

    void OnPlayerTriggerExit(VRCPlayerApi player){
        if (respawn && spawnp == player){
            Vector3 respawnVec = respawnPosition.transform.position;
            var target = Networking.LocalPlayer;
            target.TeleportTo(respawnVec, Quaternion.Euler(respawnVec));
        }
    }
    void OnPlayerRespawn(VRCPlayerApi player){
        respawn = true;
        spawnp = player;
        SendCustomEventDelayedFrames(nameof(RAend), 3);
    }   

    public void RAend(){
        respawn = false;
    }
}
