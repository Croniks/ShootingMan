using System.Collections.Generic;
using UnityEngine;


namespace Providers
{
    public class BulletsPool<T> : IProviderContent where T : MonoBehaviour
    {
        public int Count { get { return _bulletsStack.Count; } }
        private Stack<T> _bulletsStack;
        private T _prefab;
        private bool _isFixedSize = false;

        
        public void Setup(T prefab, int count, bool isFixedSize = false)
        {
            _isFixedSize = isFixedSize;
            _prefab = prefab;
            _bulletsStack = new Stack<T>(count);
            GameObject gameObj = prefab.gameObject;

            for (int i = 0; i < count; i++)
            {
                GameObject bulletObject = GameObject.Instantiate(gameObj);
                _bulletsStack.Push(bulletObject.GetComponent<T>());
                bulletObject.SetActive(false);
            }
        }
        
        public T GetFromPool()
        {
            if(_bulletsStack.Count > 0)
            {
                return _bulletsStack.Pop();
            }
            else
            {
                if(!_isFixedSize)
                {
                    return GameObject.Instantiate(_prefab).GetComponent<T>();
                }
                
                return null;
            }
        }
        
        public void ReturnToPool(T poolObject)
        {
            _bulletsStack.Push(poolObject);
        }
    }
}