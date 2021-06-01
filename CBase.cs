using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Canvas
{
    /// <summary>
    /// The base Canvas class. Contains methods for handling user input and drawing objects onscreen.
    /// <para>To create a Canvas instance, extend this class and run <c>CBase.Main</c> with its 
    /// required parameters inside a <c>public static void Main()</c>
    /// </para>
    /// <remarks>For additional documentation, visit [incomplete]</remarks>
    /// </summary>
    public abstract class CBase
    {
        /// <summary>
        /// <para>Controls the rate at which the screen refreshes.</para>
        /// <remarks>Changes to this variable are only applicable during <c>Setup()</c> 
        /// and any changes after will not affect the refresh rate.</remarks>
        /// </summary>
        public int refreshRate = 30;
        /// <summary>
        /// <para>The width of the window in pixels.</para>
        /// <remarks>Changes to this variable are only applicable during <c>Setup()</c> 
        /// and any changes after will not affect the window's width.</remarks>
        /// </summary>
        public int Width = 1200;
        /// <summary>
        /// <para>The height of the window in pixels.</para>
        /// <remarks>Changes to this variable are only applicable during <c>Setup()</c> 
        /// and any changes after will not affect the window's height.</remarks>
        /// </summary>
        public int Height = 600;
        /// <summary>
        /// <para>Controls whether the window can be resized after creation.</para>
        /// <remarks>Changes to this variable are only applicable during <c>Setup()</c> 
        /// and any changes after will not affect the window's scalability</remarks>
        /// </summary>
        public bool rescaling = false;
        /// <summary>
        /// An internal variable used to track whether the mouse is being held down.
        /// </summary>
        internal bool mouseDown = false;
        /// <summary>
        /// The current X position of the mouse. Automatically changes when the mouse is moved.
        /// </summary>
        public float MouseX
        {
            private set;
            get;
        }
        /// <summary>
        /// The current Y position of the mouse. Automatically changes when the mouse is moved.
        /// </summary>
        public float MouseY
        {
            private set;
            get;
        }

        /// <summary>
        /// For use in conjunction with <c>MouseClicked</c> and similar methods. Stores the last clicked mouse button.
        /// </summary>
        /// <remarks>Use static variables of <c>MouseButtons</c> to compare to.</remarks>
        public MouseButtons MouseSide
        {
            private set;
            get;
        }

        /// <summary>
        /// <para>
        /// The list of layers to be drawn. Layers are drawn bottom-up, with the top layer being draw over all other layers.
        /// </para>
        /// <remarks>If removing layers from this list, take care to set the <c>targetedLayer</c> property as to not break the program</remarks>
        /// </summary>
        public List<Layer> layers;
        /// <summary>
        /// <para>The layer that objects are added to by <c>Draw(c)</c> and other similar methods</para>
        /// <remarks>If changing the layer stack and this property directly, make sure to have this property always set to an existent layer</remarks>
        /// </summary>
        internal Layer targetedLayer;
        /// <summary>
        /// <para>The background layer. By default on the bottom of the layer stack.</para>
        /// <remarks>Objects in this layer are not removed when the window is redrawn. </remarks>
        /// </summary>
        public Layer background
        {
            get;
            internal set;
        }
        /// <summary>
        /// <para>This method is called before window creation. Set parameters for the window here, like <c>Width</c>, <c>Height</c>, and <c>rescaling</c>.</para>
        /// </summary>
        public abstract void Setup();
        /// <summary>
        /// <para>This method is called once after window creation. Unless <c>targetedLayer</c> is otherwise changed, objects drawn in this method will be drawn to the background layer.</para>
        /// </summary>
        public abstract void DrawOnce();
        /// <summary>
        /// <para>This method is called each time the window refreshes.</para>
        /// </summary>
        public abstract void Draw();

        //event handlers for additional user control. These are broken.

        //public event MouseEventHandler MouseMove;

        //public MouseEventHandler MouseClick
        //{
        //    private set;
        //    get;
        //}

        //public MouseEventHandler MouseDoubleClick
        //{
        //    private set;
        //    get;
        //}

        //public MouseEventHandler MouseDown
        //{
        //    private set;
        //    get;
        //}

        //public MouseEventHandler MouseUp
        //{
        //    private set;
        //    get;
        //}

        //public MouseEventHandler MouseWheel
        //{
        //    private set;
        //    get;
        //}


        //Polar rectangle methods

        //The default rectangle method, adds a polar rectangle to the drawstack, all other polar rectangle methods should call this
        /// <summary>
        /// The default rectangle method. Adds a <c>RectangleP</c> to the drawstack of the current layer.
        /// </summary>
        /// <param name="rectangle"></param>
        public void Rectangle(RectangleP rectangle)
        {
            targetedLayer.Add(rectangle);
        }
        /// <summary>
        /// <para>Creates a new <c>RectangleP</c>, adds it to the drawstack, and returns it for potential use later.</para>
        /// <remarks>Defines a new rectangle by its x, y, width, and height</remarks>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public RectangleP Rectangle(float x, float y, float width, float height)
        {
            RectangleP toReturn = null;
            toReturn = new RectangleP(x, y, width, height);
            this.Rectangle(toReturn);
            return toReturn;
        }
        /// <summary>
        /// <para>Creates a new <c>RectangleP</c>, adds it to the drawstack, and returns it for potential use later.</para>
        /// <remarks>Defines a new rectangle by its x, y, width, and height</remarks>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public RectangleP Rectangle(float x, float y, float width, float height, Color fillColor)
        {
            RectangleP toReturn = new RectangleP(x, y, width, height);
            toReturn.FillColor = fillColor;
            this.Rectangle(toReturn);
            return toReturn;
        }
        /// <summary>
        /// <para>Creates a new <c>RectangleP</c>, adds it to the drawstack, and returns it for potential use later.</para>
        /// <remarks>Defines a new rectangle by its x, y, width, and height</remarks>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public RectangleP Rectangle(float x, float y, float width, float height, Color fillColor, Color borderColor)
        {
            RectangleP toReturn = new RectangleP(x, y, width, height);
            toReturn.FillColor = fillColor;
            toReturn.BorderColor = borderColor;
            this.Rectangle(toReturn);
            return toReturn;
        }
        /// <summary>
        /// <para>Creates a new <c>RectangleP</c>, adds it to the drawstack, and returns it for potential use later.</para>
        /// <remarks>Defines a new rectangle by two points serving as bounds</remarks>
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public RectangleP Rectangle(PointF p1, PointF p2, Color fillColor, Color borderColor)
        {
            RectangleP toReturn = new RectangleP(p1,p2);
            toReturn.FillColor = fillColor;
            toReturn.BorderColor = borderColor;
            this.Rectangle(toReturn);
            return toReturn;
        }
        /// <summary>
        /// Creates a new <c>RectangleP</c>, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines a new rectangle by two points serving as bounds</remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public RectangleP Rectangle(PointF p1, PointF p2)
        {
            RectangleP toReturn = new RectangleP(p1, p2);
            this.Rectangle(toReturn);
            return toReturn;
        }

        //ellipse methods
        /// <summary>
        /// The default ellipse method. Adds a <c>EllipseC</c> to the drawstack of the current layer.
        /// </summary>
        /// <param name="ellipse"></param>
        public void Ellipse(EllipseC ellipse)
        {
            targetedLayer.Add(ellipse);
        }
        /// <summary>
        /// Creates a new <c>EllipseC</c>, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines a new ellipse by its x, y, width, and height</remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public EllipseC Ellipse(float x, float y, float width, float height)
        {
            EllipseC toReturn = new EllipseC(x, y, width, height);
            this.Ellipse(toReturn);
            return toReturn;
        }
        /// <summary>
        /// Creates a new <c>EllipseC</c>, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines a new ellipse by its x, y, width, and height</remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public EllipseC Ellipse(float x, float y, float width, float height, Color fillColor)
        {
            EllipseC toReturn = new EllipseC(x, y, width, height);
            toReturn.FillColor = fillColor;
            this.Ellipse(toReturn);
            return toReturn;
        }
        /// <summary>
        /// Creates a new <c>EllipseC</c>, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines a new ellipse by its x, y, width, and height</remarks>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public EllipseC Ellipse(float x, float y, float width, float height, Color fillColor, Color borderColor)
        {
            EllipseC toReturn = new EllipseC(x, y, width, height);
            toReturn.FillColor = fillColor;
            toReturn.BorderColor = borderColor;
            this.Ellipse(toReturn);
            return toReturn;
        }
        /// <summary>
        /// Creates a new <c>EllipseC</c>, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines a new ellipse by two points serving as bounds</remarks>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public EllipseC Ellipse(PointF p1, PointF p2, Color fillColor, Color borderColor)
        {
            EllipseC toReturn = new EllipseC(p1, p2);
            toReturn.FillColor = fillColor;
            toReturn.BorderColor = borderColor;
            this.Ellipse(toReturn);
            return toReturn;
        }
        /// <summary>
        /// Creates a new <c>EllipseC</c>, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines a new ellipse by two points serving as bounds</remarks>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public EllipseC Ellipse(PointF p1, PointF p2)
        {
            EllipseC toReturn = new EllipseC(p1, p2);
            this.Ellipse(toReturn);
            return toReturn;
        }
        /// <summary>
        /// The default image method. Adds a <c>DrawableImage</c> to the drawstack of the current layer.
        /// </summary>
        /// <param name="img"></param>
        public void Image(DrawableImage img)
        {
            targetedLayer.Add(img);
        }
        /// <summary>
        /// Creates a new <c>DrawableImage</c> with the given filepath, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public DrawableImage Image(string FilePath)
        {
            DrawableImage toReturn = new DrawableImage(FilePath);
            this.Image(toReturn);
            return toReturn;
        }
        /// <summary>
        /// Creates a new <c>DrawableImage</c> with the given filepath, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines an image with the image's default width and height at the given x and y coordinates.</remarks>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public DrawableImage Image(float x, float y, string FilePath)
        {
            DrawableImage toReturn = new DrawableImage(x,y,FilePath);
            this.Image(toReturn);
            return toReturn;
        }
        /// <summary>
        /// Creates a new <c>DrawableImage</c> with the given filepath, adds it to the drawstack, and returns it for potential use later.
        /// </summary>
        /// <remarks>Defines an image with the given width and height at the given x and y coordinates.</remarks>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public DrawableImage Image(float x, float y, int width, int height, string FilePath)
        {
            DrawableImage toReturn = new DrawableImage(x,y,width,height,FilePath);
            this.Image(toReturn);
            return toReturn;
        }

        /// <summary>
        /// A generic method for adding a given <c>Drawable</c> object to the drawstack. 
        /// </summary>
        /// <remarks>Useful for adding custom <c>Drawable</c> objects</remarks>
        /// <param name="d"></param>
        public void Draw(Drawable d)
        {
            targetedLayer.Add(d);
        }

        //end drawing methods


        //begin user overridden methods

        /// <summary>
        /// This method is called when Button 1 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button1Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 2 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button2Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 3 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button3Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 4 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button4Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 5 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button5Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 6 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button6Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 7 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button7Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 8 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button8Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 9 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button9Pressed()
        {

        }
        /// <summary>
        /// This method is called when Button 10 is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void Button10Pressed()
        {

        }

        //Internal handlers for improving user experience
        /// <summary>
        /// Internal handler for mouse movement. Sets MouseX and MouseY and calls <c>MouseMoved</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InternalMouseMoved(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
            MouseMoved();
        }
        /// <summary>
        /// Internal handler for mouse clicks. Sets MouseX, MouseY, and MouseSide as well as calls <c>MouseMoved</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InternalMouseClicked(object sender, MouseEventArgs e)
        {
            MouseSide = e.Button;
            MouseClicked();
        }
        /// <summary>
        /// Internal handler for mouse double clicks. Sets MouseSide and calls <c>MouseDoubleClicked</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InternalMouseDoubleClicked(object sender, MouseEventArgs e)
        {
            MouseSide = e.Button;
            MouseDoubleClicked();
        }
        /// <summary>
        /// Internal handler the mouse being pressed down. Sets MouseSide, MouseDown, and calls <c>MousePressed</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InternalMouseDown(object sender, MouseEventArgs e)
        {
            MouseSide = e.Button;
            mouseDown = true;
            MousePressed();
        }
        /// <summary>
        /// Internal handler for the mouse being released. Sets MouseDown and calls <c>MouseReleased</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InternalMouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            MouseReleased();
        }
        /// <summary>
        /// Internal handler for mouse scrolls. Calls <c>MouseScrolled</c>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InternalMouseWheel(object sender, MouseEventArgs e)
        {
            MouseScrolled();
        }

        //Simplified event handlers for improved user experience
        /// <summary>
        /// This method is called when the mouse is moved. Override it to add custom functionality.
        /// </summary>
        /// <remarks>By default, the mouse's location is saved to the mouseX and mouseY variables.</remarks>
        public virtual void MouseMoved()
        {

        }

        /// <summary>
        /// This method is called when either mouse button is clicked. Override it to add custom functionality.
        /// </summary>
        public virtual void MouseClicked()
        {

        }

        /// <summary>
        /// This method is called when either mouse button is double clicked. Override it to add custom functionality.
        /// </summary>
        public virtual void MouseDoubleClicked()
        {

        }

        /// <summary>
        /// This method is called when either mouse button is pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void MousePressed()
        {

        }

        /// <summary>
        /// This method is called continuously while either mouse button is held. Override it to add custom functionality.
        /// </summary>
        public virtual void MouseHeld()
        {

        }

        /// <summary>
        /// This method is called when either mouse button is released from being pressed. Override it to add custom functionality.
        /// </summary>
        public virtual void MouseReleased()
        {

        }

        /// <summary>
        /// This method is called when the mouse is scrolled. Override it to add custom functionality.
        /// </summary>
        public virtual void MouseScrolled()
        {

        }

        //TODO: Implement a separate Main with an Assembly parameter

        /// <summary>
        /// A static method that initializes an instance of Canvas.
        /// <para>
        /// 
        /// </para>
        /// </summary>
        /// <param name="Namespace">The namespace of your extension of the <c>CBase</c> class</param>
        /// <param name="AppletName">The class name of your extension of the <c>CBase</c> class</param>
        public static void Main(string Namespace, string AppletName)
        {
            Type t =  Assembly.GetCallingAssembly().GetType(Namespace+"."+AppletName);
            //try
            //{
            var internalApplet = Activator.CreateInstance(t) as CBase;
            internalApplet.layers = new List<Layer>();
            internalApplet.layers.Add(new Layer());
            internalApplet.layers.Add(new Layer());
            internalApplet.targetedLayer = internalApplet.layers[0];
            internalApplet.background = internalApplet.targetedLayer;
            internalApplet.background.retain = true;
                

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Window appletWindow = new Window(internalApplet);
            appletWindow.Width = internalApplet.Width+80;
            appletWindow.Height = internalApplet.Height;
            if (internalApplet.rescaling)
            {
                appletWindow.FormBorderStyle = FormBorderStyle.Sizable;
            }
            else
            {
                appletWindow.FormBorderStyle = FormBorderStyle.FixedSingle;
                appletWindow.MaximizeBox = false;
            }
            appletWindow.MinimizeBox = false;

            //user input event handlers;
            appletWindow.drawingSurface.MouseMove += internalApplet.InternalMouseMoved;
            appletWindow.drawingSurface.MouseClick += internalApplet.InternalMouseClicked;
            appletWindow.drawingSurface.MouseDoubleClick += internalApplet.InternalMouseDoubleClicked;
            appletWindow.drawingSurface.MouseDown += internalApplet.InternalMouseDown;
            appletWindow.drawingSurface.MouseUp += internalApplet.InternalMouseUp;
            appletWindow.drawingSurface.MouseWheel += internalApplet.InternalMouseWheel;


            Application.Run(appletWindow);
            //}
            //catch
            //{
            //    Console.WriteLine("Something fucked up!");
            //}

        }

        
    }
}
