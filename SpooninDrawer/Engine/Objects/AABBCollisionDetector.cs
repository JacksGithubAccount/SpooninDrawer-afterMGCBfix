﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Engine.Objects
{
    /// <summary>
    /// AABB stands for Aligned Axis Boundind Box
    ///
    /// Detect collisions using brute force for all bouding boxes between passive objects and active objects. Collisions will
    /// not be detected between passive objects themselves, or between active objects themselves. So 2 passive objects will
    /// never collide.
    /// 
    /// Collisions detected will invoke a continuation function where the passive object *hits* an active object.
    /// Passive objects could be bullets, walls or other things that don't 
    /// </summary>
    public class AABBCollisionDetector<P, A>
        where P : BaseGameObject
        where A : BaseGameObject
    {
        private List<P> _passiveObjects;

        /// <summary>
        /// Create an instance of the collision detector
        /// </summary>
        /// <param name="passiveObjects">passive objects don't react to collisions</param>
        public AABBCollisionDetector(List<P> passiveObjects)
        {
            _passiveObjects = passiveObjects;
        }

        /// <summary>
        /// Detect all collisions and call a handler where a passive object *hits* an active object
        /// </summary>
        /// <param name="activeObject"></param>
        /// <param name="collisionHandler"></param>
        public void DetectCollisions(A activeObject, Action<P, A> collisionHandler, Action collisionUnhandler)
        {
            bool collided = false;
            foreach (var passiveObject in _passiveObjects)
            {
                if (DetectCollision(passiveObject, activeObject))
                {
                    collisionHandler(passiveObject, activeObject);
                    collided = true;
                }                
            }
            if (!collided)
            {
                collisionUnhandler();
            }
        }
        public void DetectCollisions(A activeObject, Action<P,A> collisionHandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                if (DetectCollision(passiveObject, activeObject))
                {
                    collisionHandler(passiveObject, activeObject);
                }
            }
        }
        /// <summary>
        /// Detect all collisions and call a handler where a passive object *hits* an active object
        /// </summary>
        /// <param name="activeObjects"></param>
        /// <param name="collisionHandler"></param>
        public void DetectCollisions(List<A> activeObjects, Action<P, A> collisionHandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                foreach (var activeObject in activeObjects)
                {
                    if (DetectCollision(passiveObject, activeObject))
                    {
                        collisionHandler(passiveObject, activeObject);
                    }
                }
            }
        }

        private bool DetectCollision(P passiveObject, A activeObject)
        {
            foreach (var passiveBB in passiveObject.BoundingBoxes)
            {
                foreach (var activeBB in activeObject.BoundingBoxes)
                {
                    if (passiveBB.CollidesWith(activeBB))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
