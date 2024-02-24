using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Repository<T> : MonoBehaviour  where T : IIndentity
{
    [SerializeField] protected List<T> RepositoryObjects;
    [SerializeField] protected string KEY_REMOTE;
    protected Dictionary<string, Dictionary<string, string>> DictionaryRemoteDto = new();

    protected virtual void Setup()
    {
        DictionaryRemoteDto = SheetAdapter.Read(KEY_REMOTE);
    }
    protected virtual void Awake()
    {
        Setup();
    }
    public T GetById(Identificator identificator) 
    {
       return RepositoryObjects.FirstOrDefault(x => x.Id == identificator);
    }
    public T GetById(string identificator)
    {
        return RepositoryObjects.FirstOrDefault(x => x.Id == identificator);
    }
}
