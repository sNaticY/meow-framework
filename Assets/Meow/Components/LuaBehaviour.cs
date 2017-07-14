using XLua;
using System;
using UnityEngine;

namespace Meow.Framework
{
    [CSharpCallLua]
    public delegate void AwakeFunc();

    [CSharpCallLua]
    public delegate void OnEnableFunc();

    [CSharpCallLua]
    public delegate void StartFunc();

    [CSharpCallLua]
    public delegate void UpdateFunc();

    [CSharpCallLua]
    public delegate void OnDisableFunc();

    [CSharpCallLua]
    public delegate void OnDestroyFunc();

    [Serializable]
    public class Injection
    {
        public string Name;
        public GameObject Value;
    }

    [LuaCallCSharp]
    public class LuaBehaviour : MonoBehaviour
    {
        public string ProjectName;
        public string LuaPath;
        public Injection[] Injections;
        public LuaTable ScriptEnv;

        private AwakeFunc _awakeFunc;
        private OnEnableFunc _onEnableFunc;
        private StartFunc _startFunc;
        private UpdateFunc _updateFunc;
        private OnDisableFunc _onDisableFunc;
        private OnDestroyFunc _onDestroyFunc;

        private bool _isInitialized;

        private void Awake()
        {
            if (!string.IsNullOrEmpty(LuaPath))
            {
                _Initialize();
            }

            if (_awakeFunc != null)
            {
                _awakeFunc.Invoke();
            }
        }

        private void Start()
        {
            if (!_isInitialized)
            {
                _Initialize();
            }

            if (_startFunc != null)
            {
                _startFunc.Invoke();
            }
        }

        private void Update()
        {
            if (_updateFunc != null)
            {
                _updateFunc.Invoke();
            }
        }

        private void OnEnable()
        {
            if (_onEnableFunc != null)
            {
                _onEnableFunc.Invoke();
            }
        }

        private void OnDisable()
        {
            if (_onDisableFunc != null)
                _onDisableFunc.Invoke();
        }

        private void OnDestroy()
        {
            if (_onDestroyFunc != null)
            {
                _onDestroyFunc.Invoke();
            }
            ScriptEnv.Dispose();
        }

        private void _Initialize()
        {
            // 获取ScriptEnv
            var result = MainGame.GetLuaLoader(ProjectName).DoString(LuaPath, "LuaBehaviour");
            Debug.Assert(result.Length > 0, string.Format("Lua controller file must return something! ({0}.lua)", LuaPath));
            var rf = result[0] as LuaFunction;
            var results = rf.Call();
            Debug.Assert(results.Length > 0, string.Format("Lua controller class must return something! ({0}.lua)", LuaPath));
            ScriptEnv = results[0] as LuaTable;
            Debug.Assert(ScriptEnv != null, string.Format("Lua controller class must return a table! ({0}.lua)", LuaPath));

            // 获取相应方法
            ScriptEnv.Get("Awake", out _awakeFunc);
            ScriptEnv.Get("Start", out _startFunc);
            ScriptEnv.Get("Update", out _updateFunc);
            ScriptEnv.Get("OnEnable", out _onEnableFunc);
            ScriptEnv.Get("OnDisable", out _onDisableFunc);
            ScriptEnv.Get("OnDestroy", out _onDestroyFunc);

            // 注入GameObject
            ScriptEnv.Set("gameObject", this.gameObject);
            
            // 注入MonoBehaviour本身
            ScriptEnv.Set("behaviour", this);

            if (Injections.Length >= 0)
            {
                foreach (var injection in Injections)
                {
                    ScriptEnv.Set(injection.Name, injection.Value);
                }
            }

            _isInitialized = true;
        }
    }
}