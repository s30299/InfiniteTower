using System.Data.Common;
using UnityEngine;

public class DefsDBBootstrap : MonoBehaviour
{
    [SerializeField] private DefsDbSO defsDb;
    void Awake()
    {
        defsDb.BuildCache();
    }
}
