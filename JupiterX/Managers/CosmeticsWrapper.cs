using System;
using System.Collections;
using System.Linq;

namespace JupiterX;
public static class CosmeticsWrapper
{
    private static Type controllerType;
    private static object instance;
    static CosmeticsWrapper()
    {
        controllerType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => { try { return a.GetTypes(); } catch { return new Type[0]; } }).FirstOrDefault(t => t.Name == "CosmeticsController");
        if (controllerType == null)
        {
            Utility.Log("CosmeticsController not found");
            return;
        }

        instance = controllerType.GetField("instance").GetValue(null);
    }
    public static void PurchaseAll()
    {
        if (controllerType == null || instance == null) return;
        var allCosmetics = (IEnumerable)controllerType.GetField("allCosmetics").GetValue(instance);
        var getItem = controllerType.GetMethod("GetItemFromDict");
        var updateCart = controllerType.GetMethod("UpdateShoppingCart");
        var purchase = controllerType.GetMethod("PurchaseItem");
        var updateWardrobe = controllerType.GetMethod("UpdateWardrobeModelsAndButtons");
        var checkUpdate = controllerType.GetMethod("CheckIfMyCosmeticsUpdated");
        var cart = (IList)controllerType.GetField("currentCart").GetValue(instance);
        foreach (var item in allCosmetics)
        {
            var itemName = (string)item.GetType().GetField("itemName").GetValue(item);
            var itemObj = getItem.Invoke(instance, new object[] { itemName });
            cart.Insert(0, itemObj);
            updateCart.Invoke(instance, null);
            purchase.Invoke(instance, null);
            updateWardrobe.Invoke(instance, null);
            checkUpdate.Invoke(instance, new object[] { itemName });
        }
    }
}