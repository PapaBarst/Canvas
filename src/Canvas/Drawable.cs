using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas
{
    /// <summary>
    /// Represents a drawable object. If an object implements this interface, it must have a <c>Draw()</c> method and be able to be drawn onscreen.
    /// </summary>
    public interface Drawable
    {
        /// <summary>
        /// The internal timer calls this method for each <c>Drawable</c> object in the drawstack. Must be implemented.
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);
    }
}
