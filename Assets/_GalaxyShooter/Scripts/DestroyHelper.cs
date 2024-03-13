using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHelper : MonoBehaviour
{
    public void DestroySefl()
    {
        LeanPool.Despawn(gameObject);
    }
}
