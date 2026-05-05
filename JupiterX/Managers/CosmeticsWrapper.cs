using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace JupiterX
{
    public static class CosmeticsWrapper
    {
        private static Type controllerType;
        private static object instance;

        static CosmeticsWrapper()
        {
            try
            {
                controllerType = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(a =>
                    {
                        try { return a.GetTypes(); }
                        catch { return Array.Empty<Type>(); }
                    })
                    .FirstOrDefault(t => t.Name == "CosmeticsController");

                if (controllerType == null)
                {
                    Notifications.NotifiLib.SendNotification("CosmeticsController not found");
                    return;
                }

                var instanceField = controllerType.GetField("instance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                instance = instanceField?.GetValue(null);

                if (instance == null)
                    Notifications.NotifiLib.SendNotification("CosmeticsController instance is null");
            }
            catch (Exception ex)
            {
                Utility.Log($"[CosmeticsWrapper Init Error] {ex}");
            }
        }

        public static void AddCurrency(int amount)
        {
            if (controllerType == null || instance == null) return;

            try
            {
                var gotDailyField = controllerType.GetField("gotMyDaily", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var currencyField = controllerType.GetField("currencyBalance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var updateBoard = controllerType.GetMethod("UpdateCurrencyBoard", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                gotDailyField?.SetValue(instance, true);

                if (currencyField != null)
                {
                    int current = (int)(currencyField.GetValue(instance) ?? 0);
                    currencyField.SetValue(instance, current + amount);
                }

                updateBoard?.Invoke(instance, null);
            }
            catch (Exception ex)
            {
                Notifications.NotifiLib.SendNotification("<color=cyan>[INFO]</color> Error adding currecny. " + ex, 15f);
                Utility.Log($"[AddCurrency Error] {ex}");
            }
        }

        public static void PurchaseAll()
        {
            if (controllerType == null || instance == null) return;

            try
            {
                var allCosmeticsField = controllerType.GetField("allCosmetics", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var cartField = controllerType.GetField("currentCart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                var allCosmetics = allCosmeticsField?.GetValue(instance) as IEnumerable;
                var cart = cartField?.GetValue(instance) as IList;

                if (allCosmetics == null || cart == null)
                {
                    Utility.Log("Cosmetics or cart is null");
                    return;
                }

                var getItem = controllerType.GetMethod("GetItemFromDict", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var updateCart = controllerType.GetMethod("UpdateShoppingCart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var purchase = controllerType.GetMethod("PurchaseItem", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var updateWardrobe = controllerType.GetMethod("UpdateWardrobeModelsAndButtons", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var checkUpdate = controllerType.GetMethod("CheckIfMyCosmeticsUpdated", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var item in allCosmetics)
                {
                    if (item == null) continue;

                    var itemNameField = item.GetType().GetField("itemName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var itemName = itemNameField?.GetValue(item) as string;

                    if (string.IsNullOrEmpty(itemName)) continue;

                    var itemObj = getItem?.Invoke(instance, new object[] { itemName });
                    if (itemObj == null) continue;

                    cart.Insert(0, itemObj);
                    updateCart?.Invoke(instance, null);
                    purchase?.Invoke(instance, null);
                    updateWardrobe?.Invoke(instance, null);
                    checkUpdate?.Invoke(instance, new object[] { itemName });
                }
                Notifications.NotifiLib.SendNotification("<color=cyan>[INFO]</color> Unlocked all cosmetics.", 15f);
            }
            catch (Exception ex)
            {
                Notifications.NotifiLib.SendNotification("<color=cyan>[INFO]</color> Error. " + ex, 15f);
                Utility.Log($"[PurchaseAll Error] {ex}");
            }
        }
    }
}