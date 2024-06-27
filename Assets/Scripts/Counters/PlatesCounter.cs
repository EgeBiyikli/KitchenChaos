using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private float plateSpawnedAmount;
    private float plateSpawnedAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax ) {
            spawnPlateTimer = 0f;
            if(KitchenGameManager.Instance.IsGamePlaying() && plateSpawnedAmount < plateSpawnedAmountMax ) {
                plateSpawnedAmount++;

                OnPlateSpawned?.Invoke( this, EventArgs.Empty );
            }
        }
    }
    public override void Interact(Player player) {
        if (!player.IsKitchenObject()) {
            //Player is empty handed
            if(plateSpawnedAmount > 0) {
                plateSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke( this, EventArgs.Empty );
            }
        }    
    }


}