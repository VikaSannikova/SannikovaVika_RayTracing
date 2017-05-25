using System;
using OpenTK;
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
        int a, b, c, d;
        static float x, y, z;
        Vector3 delta = new Vector3(-1.0f, -1.0f, -2.0f);
        public Form1()
        {
            InitializeComponent();
            glControl1.Invalidate();
        }

        shader mShaders = new shader();

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {

            Console.WriteLine(mShaders.glslVersion);
            Console.WriteLine(mShaders.glVersion);

            mShaders.InitShaders(glControl1.Width / (float)glControl1.Height);

            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);


            GL.EnableVertexAttribArray(mShaders.attribute_vpos);
            mShaders.Draw(delta);
            Console.WriteLine("OK");
      //      GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            
      //      GL.DisableVertexAttribArray(mShaders.attribute_vpos);

            glControl1.SwapBuffers();
            GL.UseProgram(0);
        }

        private void glControl1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

            switch (e.KeyChar)
            {
                case 'w':
                    delta.Y = delta.Y + 0.4f;
                    break;
                case 'a':
                    delta.X = delta.X - 0.4f;
                    break;
                case 'd':
                    delta.X = delta.X + 0.4f;
                    break;
                case 's':
                    delta.Y = delta.Y - 0.4f;
                    break;
            }
            Refresh();
    }



        //public void Refresh()
        //{
        //    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        //    Draw();
        //}
        //public void Draw()
        //{
        //    GL.UseProgram(BasicProgramID);
        //    GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "delta"), delta);
        //    GL.EnableVertexAttribArray(0);
        //    GL.DrawArrays(BeginMode.Quads, 0, 4);
        //    GL.DisableVertexAttribArray(0);
        //    GL.UseProgram(0);
        //}
    }
}
