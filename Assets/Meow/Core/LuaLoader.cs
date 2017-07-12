using System.Collections;
using System.Collections.Generic;
using XLua;
using Meow.AssetLoader;
using UnityEngine;

namespace Meow.Framework
{
    public class LuaLoader
    {
        private const float _gcInternal = 1f;
        private float _lastGCTime = 0;
        
        public LuaEnv LuaEnv { get; private set; }
        private Dictionary<string, TextAsset> LuaScripts { get; set; }

        public IEnumerator Initialize(MainLoader _loader, string luaScriptBundlePath, string luaScriptsRootPath)
        {
            LuaEnv = new LuaEnv();
            LuaScripts = new Dictionary<string, TextAsset>();
            var operation = _loader.LoadBundle(luaScriptBundlePath);
            yield return operation;

            var assets = operation.GetAllAssets<TextAsset>();
            foreach (var luaAssets in assets)
            {
                var firstIndex = luaAssets.Key.LastIndexOf(luaScriptsRootPath.ToLower()) + luaScriptsRootPath.Length;
                var luaPath = luaAssets.Key.Substring(firstIndex + 1, luaAssets.Key.Length - firstIndex - 1 - 8);
                LuaScripts.Add(luaPath, luaAssets.Value);
            }

            LuaEnv.AddLoader((ref string filepath) => LuaScripts[filepath.ToLower()].bytes);
            
            LuaEnv.DoString(LuaScripts["main"].bytes);
        }
        
        public string GetLuaScriptString(string luaPath)
        {
            return LuaScripts[luaPath.ToLower()].text;
        }

        public void UpdateGCTick()
        {
            if (Time.time - _lastGCTime > _gcInternal)
            {
                LuaEnv.Tick();
                _lastGCTime = Time.time;
            }
        }
    }
}