using System;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OpenTK;
//using OpenTK.Graphics.OpenGL;
//using System.Drawing;
//using System.IO;
//using OpenTK.Input;


namespace SannikovaVika_RayTracing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            glControl1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {

            shader m = new shader();

            Console.WriteLine(m.glslVersion);
            Console.WriteLine(m.glVersion);

            m.InitShaders(glControl1.Width / (float)glControl1.Height);

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);


            GL.EnableVertexAttribArray(m.attribute_vpos);


            Console.WriteLine("OK");
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);


            GL.DisableVertexAttribArray(m.attribute_vpos);

            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }
    }
}
