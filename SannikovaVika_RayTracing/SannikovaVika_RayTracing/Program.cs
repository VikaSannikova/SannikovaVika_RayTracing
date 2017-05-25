using System;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;

namespace SannikovaVika_RayTracing
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        static float start_x = 0; static float start_y = 0; static float start_z = 0;

        static void keyget(int key, int x, int y)
        {

            //switch (key)
            //{
            //    case GL.GL_KEY_LEFT: start_x -= 0.9f; break;
            //    case GL.GLUT_KEY_RIGHT: start_x += 0.9f; break;
            //    case GL.GLUT_KEY_UP: start_y += 0.9f; break;
            //    case GL.GLUT_KEY_DOWN: start_y -= 0.9f; break;

            //}
        }

    }
}
