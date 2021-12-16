using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2
{
    /// <summary>
    /// A rectangle
    /// </summary>
    class Rectangle : IComparable
    {
        #region Fields

        int width;
        int height;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a rectangle with the given width and height
        /// </summary>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the rectangle
        /// </summary>
        /// <value>width of the rectangle</value>
        public int Width
        {
            get { return width; }
        }

        /// <summary>
        /// Gets the height of the rectangle
        /// </summary>
        /// <value>height of the rectangle</value>
        public int Height
        {
            get { return height; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Compares the current instance with another object of the same type 
        /// and returns an integer that indicates whether the current instance 
        /// precedes, follows, or occurs in the same position in the sort order
        /// as the other object.
        /// </summary>
        /// <returns>relative order of this instance and object</returns>
        /// <param name="obj">object to compare to</param>
        public int CompareTo(object obj)
        {
            // this instance is greater than a null object
            if (obj == null)
            {
                return 1;
            }

            // check for same object type
            Rectangle otherRectangle = obj as Rectangle;
            if (otherRectangle != null)
            {
                // return relative order
                int thisArea = width * height;
                int otherArea = otherRectangle.Width * otherRectangle.Height;
                if (thisArea < otherArea)
                {
                    return -1;
                }
                else if (thisArea == otherArea)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                // comparing to wrong object type
                throw new ArgumentException("Object is not a Rectangle");
            }
        }

        /// <summary>
        /// Converts the rectangle to a string
        /// </summary>
        /// <returns>the string for the rectangle</returns>
        public override string ToString()
        {
            return "[Rectangle: Width = " + width + " Height = " + height + "]";
        }

        #endregion
    }
}

