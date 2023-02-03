using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Pool
{
    public class GenericPool<T> where T : Component
    {
        protected List<T> _pool;
        protected T _prefab;
        protected Transform _parent;
        protected int _amount;

        public GenericPool(T prefab, int amount, Transform parent = null)
        {
            _pool = new List<T>();
            _parent = parent;
            _amount = amount;

            if (!prefab) return;
            this._prefab = prefab;

            for (int i = 0; i < _amount; i++)
            {
                GetPoolObject();
            }
        }

        public T GetPoolObject()
        {
            foreach (var o in _pool)
            {
                if (!o.gameObject.activeInHierarchy &&
                _pool.Count >= _amount)
                    return o;
            }
            if (!_parent)
                _parent = new GameObject($"Pool - {_prefab.name}").transform;

            var newObject = GameObject.Instantiate(_prefab, _parent);
            newObject.gameObject.SetActive(false);
            _pool.Add(newObject);
            return newObject;
        }

        public int EnabledAmount()
        {
            var amount = 0;
            foreach (var o in _pool)
            {
                if (o.gameObject.activeInHierarchy)
                    amount++;
            }
            return amount;
        }

        public bool PoolInitialized()
        {
            return _pool != null;
        }

        public void Destroy(T o)
        {
            if (o.gameObject.activeInHierarchy)
                o.gameObject.SetActive(false);
        }

        public void DestroyAll()
        {
            _pool.ForEach(o => Destroy(o));
        }
    }
}