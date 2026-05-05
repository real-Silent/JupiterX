using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace JupiterX
{
    public static class CosmeticsWrapper
    {
        private static Type controllerType;
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
                    .FirstOrDefault(t =>
                        t.FullName != null &&
                        t.FullName.Contains("CosmeticsController"));
                if (controllerType == null)
                {
                    Notifications.NotifiLib.SendNotification("CosmeticsController not found");
                    return;
                }
                Notifications.NotifiLib.SendNotification($"[CosmeticsWrapper] Found: {controllerType.FullName}");
            }
            catch (Exception ex)
            {
                Utility.Log($"[CosmeticsWrapper Init Error] {ex}");
            }
        }

        private static object GetInstance()
        {
            if (controllerType == null) return null;
            try
            {
                var instanceField = controllerType.GetField("instance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                var inst = instanceField?.GetValue(null);
                if (inst != null)
                    return inst;
                var all = UnityEngine.Resources.FindObjectsOfTypeAll<UnityEngine.Object>();
                foreach (var obj in all)
                {
                    if (obj == null) continue;
                    var objType = obj.GetType();
                    if (objType == controllerType || objType.FullName == controllerType.FullName)
                    {
                        return obj;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Utility.Log($"[GetInstance Error] {ex}");
                return null;
            }
        }

        public static void AddCurrency(int amount)
        {
            if (controllerType == null) return;
            var instance = GetInstance();
            if (instance == null)
            {
                Notifications.NotifiLib.SendNotification("CosmeticsController not ready");
                return;
            }
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
                Notifications.NotifiLib.SendNotification("<color=cyan>[INFO]</color> Error adding currency: " + ex, 15f);
                Utility.Log($"[AddCurrency Error] {ex}");
            }
        }

        public static void PurchaseAll()
        {
            if (controllerType == null) return;
            var instance = GetInstance();
            if (instance == null)
            {
                Notifications.NotifiLib.SendNotification("CosmeticsController not ready");
                return;
            }
            try
            {
                var allCosmeticsField = controllerType.GetField("allCosmetics", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var cartField = controllerType.GetField("currentCart", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                var allCosmetics = allCosmeticsField?.GetValue(instance) as IEnumerable;
                var cart = cartField?.GetValue(instance) as IList;
                if (allCosmetics == null || cart == null)
                {
                    Utility.Log("[CosmeticsWrapper] Cosmetics or cart is null");
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
                Notifications.NotifiLib.SendNotification("<color=cyan>[INFO]</color> Error: " + ex, 15f);
                Utility.Log($"[PurchaseAll Error] {ex}");
            }
        }
        public static void DebugFields()
        {
            if (controllerType == null) return;
            foreach (var f in controllerType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                Utility.Log($"Field: {f.Name}");
            }
            foreach (var m in controllerType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                Utility.Log($"Method: {m.Name}");
            }
        }
    }
}