using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Objects
{
    public class GameObjectPool<T> where T : BaseGameObject
    {
        private LinkedList<T> _activePool = new LinkedList<T>();
        private LinkedList<T> _inactivePool = new LinkedList<T>();

        public List<T> ActiveObjects
        {
            get
            {
                var list = new List<T>();
                foreach (var gameObject in _activePool)
                {
                    list.Add(gameObject);
                }
                return list;
            }
        }
        public T GetOrCreate(Func<T> createNbObjectFn)
        {
            T activatedObject;
            if (_inactivePool.Count > 0)
            {
                var gameObject = _inactivePool.First.Value;
                gameObject.Initialize();
                gameObject.Activate();
                activatedObject = gameObject;
                _activePool.AddLast(gameObject);
                _inactivePool.RemoveFirst();
            }
            else
            {
                var gameObject = createNbObjectFn();
                gameObject.Activate();
                activatedObject = gameObject;
                _activePool.AddLast(gameObject);
            }
            return activatedObject;
        }

        public void DeactivateObject(T gameObject, Action<T>postDeactivateFn)
        {
            var activeObject = _activePool.Find(gameObject);
            if (activeObject != null)
            {
                gameObject.Deactivate();
                _activePool.Remove(gameObject);
                _inactivePool.AddLast(gameObject);
            }
            postDeactivateFn(gameObject);
        }

        public void DeactivateObject(T gameObject)
        {
            DeactivateObject(gameObject, _ => { });
        }
        public void DeactivateAllObjects(Action<T> postDeactivateFn)
        {
            foreach (var gameObject in ActiveObjects)
            {
                DeactivateObject(gameObject, postDeactivateFn);
            }
        }
        public void DeactivateAllObjects()
        {
            foreach (var gameObject in ActiveObjects)
            {
                DeactivateObject(gameObject);
            }
        }
    }
}
             

