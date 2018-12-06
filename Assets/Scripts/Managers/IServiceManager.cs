using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServiceManager
{
  bool RequestService<T>(out T service) where T : class;
}
