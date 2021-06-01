using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Canvas
{
    internal partial class Window : Form
    {
        private CBase internalApplet;
        private System.Timers.Timer processRunner;
        private int tickRate;
        int newRefreshRate;
        public Window(CBase inApplet)
        {
            InitializeComponent();
            internalApplet = inApplet;

            //sets up window characteristics, like width and height, that cannot be modified after window creation
            internalApplet.Setup();

            //draws objects on the background layer by default, so they will not be redrawn
            internalApplet.DrawOnce();

            //changes the layer to the first non-background layer
            internalApplet.targetedLayer = internalApplet.layers[1];

            //changes the frame rate selector's text to be the user selected value
            frameRateSelector.Text = internalApplet.refreshRate.ToString();

            //sets up and runs the timer for repeatedly refreshing drawings on screen
            newRefreshRate = internalApplet.refreshRate;
            tickRate = (1000 / internalApplet.refreshRate);
            Console.WriteLine("Tick rate: "+tickRate);
            processRunner = new System.Timers.Timer(tickRate);
            processRunner.SynchronizingObject = drawingSurface;
            processRunner.AutoReset = true;
            processRunner.Enabled = true;
            processRunner.Elapsed += this.Tick;
        }

        private void Tick(Object sender, ElapsedEventArgs e)
        {
            internalApplet.Draw();
            if (internalApplet.mouseDown)
            {
                internalApplet.MouseHeld();
            }
            drawingSurface.Refresh();
            if (internalApplet.refreshRate != newRefreshRate) {
                internalApplet.refreshRate = newRefreshRate;
                processRunner.Interval = (1000 / internalApplet.refreshRate);
            }
        }

        private void frameRateSelector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frameRateSelector_TextChanged(object sender, EventArgs e)
        {
            try
            {
                newRefreshRate = int.Parse(frameRateSelector.Text);
                if (newRefreshRate > 1000)
                {
                    newRefreshRate = 1000;
                }
                if (newRefreshRate <= 0)
                {
                    newRefreshRate = 1;
                }
            }
            catch
            {
                frameRateSelector.Text = internalApplet.refreshRate.ToString();
            }
        }

        private void drawingSurface_Paint(object sender, PaintEventArgs e)
        {
            foreach(Layer l in internalApplet.layers)
            {
                for(int i=0; i<l.drawstack.Count; i++)
                {
                    Drawable shape = l.drawstack[i];
                    shape.Draw(e.Graphics);
                    e.Graphics.ResetTransform();
                    if (!l.retain)
                    {
                        l.drawstack.Remove(shape);
                        i--;
                    }
                }
            }
            //g.
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            internalApplet.Button1Pressed();
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            internalApplet.Button2Pressed();
        }

        private void customButton3_Click(object sender, EventArgs e)
        {
            internalApplet.Button3Pressed();
        }

        private void customButton4_Click(object sender, EventArgs e)
        {
            internalApplet.Button4Pressed();
        }

        private void customButton5_Click(object sender, EventArgs e)
        {
            internalApplet.Button5Pressed();
        }

        private void customButton6_Click(object sender, EventArgs e)
        {
            internalApplet.Button6Pressed();
        }

        private void customButton7_Click(object sender, EventArgs e)
        {
            internalApplet.Button7Pressed();
        }

        private void customButton8_Click(object sender, EventArgs e)
        {
            internalApplet.Button8Pressed();
        }

        private void customButton9_Click(object sender, EventArgs e)
        {
            internalApplet.Button9Pressed();
        }

        private void customButton10_Click(object sender, EventArgs e)
        {
            internalApplet.Button10Pressed();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (processRunner.Enabled)
            {
                this.pauseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.pauseButton.Text = "Resume";
            }
            else
            {
                this.pauseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                this.pauseButton.Text = "Pause";
            }
            processRunner.Enabled = !processRunner.Enabled;
        }
    }
}
