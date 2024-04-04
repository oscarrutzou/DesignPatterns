using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FortressSurvivor
{
    public abstract class ObjectPool
    {
        protected List<GameObject> active = new List<GameObject>();

        protected Stack<GameObject> inactive = new Stack<GameObject>();

        public GameObject GetObject()
        {
            if (inactive.Count==0)
            {
                return CreateObject();
            }
            GameObject go = inactive.Pop();
            active.Add(go);
            return go;
        }

        public void ReleaseObject(GameObject gameObject)
        {
            active.Remove(gameObject);
            inactive.Push(gameObject);
            GameWorld.Instance.Destroy(gameObject);
            CleanUp(gameObject);
        }

        protected abstract GameObject CreateObject();

        protected abstract void CleanUp(GameObject gameObject);
    }
}
