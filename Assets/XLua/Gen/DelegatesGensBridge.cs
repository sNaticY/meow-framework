#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;


namespace XLua
{
    public partial class DelegateBridge : DelegateBridgeBase
    {
		
		public void __Gen_Delegate_Imp0()
		{
#if THREAD_SAFT || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
                RealStatePtr L = luaEnv.rawL;
                int err_func =LuaAPI.load_error_func(L, errorFuncRef);
                
                
                LuaAPI.lua_getref(L, luaReference);
                
                
                int __gen_error = LuaAPI.lua_pcall(L, 0, 0, err_func);
                if (__gen_error != 0)
                    luaEnv.ThrowExceptionFromError(err_func - 1);
                
                
                
                LuaAPI.lua_settop(L, err_func - 1);
                
#if THREAD_SAFT || HOTFIX_ENABLE
            }
#endif
		}
        
        
		static DelegateBridge()
		{
		    Gen_Flag = true;
		}
		
		public override Delegate GetDelegateByType(Type type)
		{
		
		    if (type == typeof(Meow.Framework.AwakeFunc))
			{
			    return new Meow.Framework.AwakeFunc(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(Meow.Framework.OnEnableFunc))
			{
			    return new Meow.Framework.OnEnableFunc(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(Meow.Framework.StartFunc))
			{
			    return new Meow.Framework.StartFunc(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(Meow.Framework.UpdateFunc))
			{
			    return new Meow.Framework.UpdateFunc(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(Meow.Framework.OnDisableFunc))
			{
			    return new Meow.Framework.OnDisableFunc(__Gen_Delegate_Imp0);
			}
		
		    if (type == typeof(Meow.Framework.OnDestroyFunc))
			{
			    return new Meow.Framework.OnDestroyFunc(__Gen_Delegate_Imp0);
			}
		
		    throw new InvalidCastException("This delegate must add to CSharpCallLua: " + type);
		}
	}
    
}