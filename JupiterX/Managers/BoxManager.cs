using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System;
using UnityEngine;

// JupiterX copyright 2026
/*
 - Please do not skid or say you made this file
 - This file is originally made by nova/silent
 - If this file goes into your mod menu you will be counted as a skidder
 - I have had this file private for 2-3 ish months because of this reason
 - If you want to use this give me credits somewhere
 */

namespace JupiterX.Managers
{
    internal class BoxManager
    {
        public unsafe static Il2CppSystem.Object BoxAny(object obj)
        {
            if (obj == null) return null;
            if (obj is object[] oa)
            {
                var arr = new Il2CppReferenceArray<Il2CppSystem.Object>(oa.Length);
                for (int index = 0; index < oa.Length; index++)
                    arr[index] = BoxAny(oa[index]);
                return arr.Cast<Il2CppSystem.Object>();
            }
            if (obj is Il2CppSystem.Object il2cppObj) return il2cppObj;
            if (obj is UnityEngine.Object unityObj) return unityObj.Cast<Il2CppSystem.Object>();
            if (obj is string str) return (Il2CppSystem.Object)str;
            if (obj is int[] ia) return BoxArray(ia);
            if (obj is bool[] ba) return BoxArray(ba);
            if (obj is float[] fa) return BoxArray(fa);
            if (obj is short[] sa) return BoxArray(sa);
            if (obj is long[] la) return BoxArray(la);
            if (obj is ulong[] ula) return BoxArray(ula);
            if (obj is double[] da) return BoxArray(da);
            if (obj is byte[] bya) return BoxArray(bya);
            if (obj is uint[] uia) return BoxArray(uia);
            if (obj is int i) return Box(i);
            if (obj is bool b) return Box(b);
            if (obj is float f) return Box(f);
            if (obj is short s) return Box(s);
            if (obj is long l) return Box(l);
            if (obj is ulong u) return Box(u);
            if (obj is double d) return Box(d);
            if (obj is byte by) return Box(by);
            if (obj is uint ui) return Box(ui);
            if (obj is sbyte sb) return Box(sb);
            if (obj is ushort us) return Box(us);
            if (obj is Vector2 v2) return Box(v2);
            if (obj is Vector3 v3) return Box(v3);
            if (obj is Vector4 v4) return Box(v4);
            if (obj is Quaternion q) return Box(q);
            if (obj is Color c) return Box(c);
            if (obj is Color32 c32) return Box(c32);
            if (obj is Matrix4x4 m) return Box(m);
            if (obj is Bounds bo) return Box(bo);
            if (obj is Rect r) return Box(r);
            return null;
        }
        public static Il2CppSystem.Object BoxArray<T>(T[] arr) where T : struct
        {
            var il2cppArray = Array.CreateInstance(typeof(T), arr.Length);
            for (int i = 0; i < arr.Length; i++)
                il2cppArray.SetValue(arr[i], i);
            return (Il2CppSystem.Object)(object)il2cppArray;
        }
        public static unsafe Il2CppSystem.Object Box<T>(T v) where T : struct
        {
            IntPtr ptr = IL2CPP.il2cpp_object_new(Il2CppClassPointerStore<T>.NativeClassPtr);
            *(T*)IL2CPP.il2cpp_object_unbox(ptr) = v;
            return new Il2CppSystem.Object(ptr);
        }
    }
}