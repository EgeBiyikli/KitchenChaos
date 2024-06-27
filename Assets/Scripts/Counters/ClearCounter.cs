using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(Player player) {
        if (!IsKitchenObject()) {
            //There is no KitchenObject here.
            if (player.IsKitchenObject()) {
                //Player is carrying something.
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                //Player not carrying anything.
            }
        }
        else {
            //There is a KitchenObject here.
            if(player.IsKitchenObject()) {
                //Player has carrying something.
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    //Player is not carrying plate but something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();

                        }
                    }
                }
            }
            else {
                //Player not carrying anything.
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
}
