using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FortressSurvivor
{
    public class GameObject
    {
        public List<Component> components { get; private set; } = new List<Component>();

        public Transform Transform { get; private set; } = new Transform();

        public string Tag { get; set; }


        public T AddComponent<T>() where T : Component
        {
            Type componentType = typeof(T);
            // Get All constructors
            var constructors = componentType.GetConstructors();
            // Find COnstructor with exacly 1 param that is a GameObject
            var constructor = constructors.FirstOrDefault(c =>
            {
                var parameters = c.GetParameters();
                return parameters.Length == 1 && parameters[0].ParameterType == typeof(GameObject);
            });
            if (constructor != null)
            {
                //Create instance of component using the Activator Class with This GameObject as A parameter.
                T component = (T)Activator.CreateInstance(componentType, this);
                components.Add(component);
                return component;
            }
            else
            {
                //Error handling...
                throw new InvalidOperationException($"Klassen {componentType.Name} skal have en konstruktør med ét parameter af typen GameObject.");
            }
        }

        public T AddComponent<T>(params object[] additionalParams) where T : Component
        {
            Type componentType = typeof(T);
            try
            {
                //Finds the contructer with the correct params
                object[] allParams = new object[1 + additionalParams.Length]; //Sætter alle params in i et array
                allParams[0] = this;
                Array.Copy(additionalParams, 0, allParams, 1, additionalParams.Length);

                T component = (T)Activator.CreateInstance(componentType, allParams);
                components.Add(component);
                return component;
            }
            catch (Exception)
            {
                throw new Exception($"Klassen {componentType.Name} har ikke en contructer som passer med de leverede parameter.");
            }
        }

        public Component GetComponent<T>() where T : Component
        {
            return components.Find(x => x.GetType() == typeof(T));
        }

        public void Awake()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Awake();
            }
        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw(spriteBatch);
            }
        }

        private Component AddComponentWithExistingValues(Component component)
        {
            components.Add(component);
            return component;
        }

        public object Clone()
        {
            GameObject go = new GameObject();

            foreach (Component component in components)
            {
                Component newComponent = go.AddComponentWithExistingValues(component.Clone() as Component);
                newComponent.SetNewGameObject(go);
            }

            return go;
        }

        public void OnCollisionEnter(Collider collider)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].OnCollisionEnter(collider);
            }
        }

    }
}