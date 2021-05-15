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
            
            for (int i = 0; i < count; i++)
            {
                T bullet = GameObject.Instantiate<T>(_prefab);
                _bulletsStack.Push(bullet);
                bullet.gameObject.SetActive(false);
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
                    return GameObject.Instantiate<T>(_prefab);
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