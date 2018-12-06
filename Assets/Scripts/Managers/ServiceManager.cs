using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : IServiceManager
{
  private static IServiceManager instance;
  public static IServiceManager Singleton
  {
    get
    {
      if (instance == null)
      {
        instance = new ServiceManager();
        InitializeCoreServices();
      }
      return instance;
    }
  }

  private static Dictionary<System.Type, object> serviceDict = new Dictionary<System.Type, object>();

  private static void InitializeCoreServices()
  {
    serviceDict.Add(typeof(PlayerStateManager), GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateManager>());

    // platform dependent input
#if UNITY_EDITOR || UNITY_STANDALONE
    serviceDict.Add(typeof(IInputSystem), new PS4InputSystem()); // make this platform independent
#elif UNITY_ANDROID
    serviceDict.Add(typeof(IInputSystem), new MobileInputSystem());

#endif

  }

  public bool RequestService<T>(out T service) where T : class
  {
    try
    {
      service = (T)serviceDict[typeof(T)];
      return true;
    }
    catch
    {
      service = null;
      return false;
    }
  }
}
