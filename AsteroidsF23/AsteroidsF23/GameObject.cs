using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsF23
{
    public abstract class GameObject
    {
        public PointF Position;
        public float Rotation;

        /* 
        * update
        * Abstract method for updating the object state based on deltaTime
        */
        public abstract void update(float deltaTime);

        /* 
        * draw
        * Abstract method for drawing the object on the provided Graphics object
        */
        public abstract void draw(Graphics g);

        /* 
        * getBounds
        * Abstract method for getting the bounding rectangle of the object
        */
        public abstract RectangleF getBounds();
    }
}
