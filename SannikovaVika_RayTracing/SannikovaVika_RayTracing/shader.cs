using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.IO;
using OpenTK.Input;
//using OpenTK.Input;

namespace SannikovaVika_RayTracing
{
    class shader
    {
        int BasicProgramID; //Номер дескриптора на графической карте
        int BasicVertexShader; //Адрес вершинного шейдера  
        int BasicFragmentShader; //Адрес фрагментного шейдера

        //float angelX = 0.0f; // 0 to 360
       // float angelY = 0.0f; //-90 to 90
       // int mouseX = 0;
       // int mouseY = 0;
       // float dx = 0, dy = 0, dz = 0;
       // int Wx = 0, Wy = 0;

        int attribute_vpos; //Адрес параметра позиции
        int vboVertexPosition; //Адрес буфера вершин объекта для нашего параметра позиции
        int uniform_pos;
        int uniform_aspect;
        float aspect;
        Vector3 camera_position;
        Vector3[] vertdata; //Массив позиций вершин

        //void loadShader(String filename, ShaderType type, int program, out int address)
        //{
        //    address = GL.CreateShader(type);
        //    using (System.IO.StreamReader sr = new StreamReader(filename))
        //    {
        //        GL.ShaderSource(address, sr.ReadToEnd());
        //    }
        //    GL.CompileShader(address);
        //    GL.AttachShader(program, address);
        //    Console.WriteLine(GL.GetShaderInfoLog(address));
        //}

        void loadShader(String filename, ShaderType type, int program, out int address)

        {

            address = GL.CreateShader(type);

            using (System.IO.StreamReader sr = new StreamReader(filename))

            {

                GL.ShaderSource(address, sr.ReadToEnd());

            }

            GL.CompileShader(address);

            GL.AttachShader(program, address);

            Console.WriteLine(GL.GetShaderInfoLog(address));

        }

        private void InitShaders()
        {
            //// создание объекта программы 
            //BasicProgramID = GL.CreateProgram();
            //loadShader("C:\\Users\\Виктория\\Documents\\Visual Studio 2015\\Projects\\SannikovaVika_RayTracing\\raytraсing.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            //loadShader("C:\\Users\\Виктория\\Documents\\Visual Studio 2015\\Projects\\SannikovaVika_RayTracing\\raytracing.frag", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
            ////Компоновка программы
            //GL.LinkProgram(BasicProgramID);

            //// Проверить успех компоновки
            //int status = 0;
            //GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);

            //Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));

            // attribute_vpos = GL.GetAttribLocation(BasicProgramID, "vPosition");
            // uniform_pos = GL.GetUniformLocation(BasicProgramID, "camera_position");
            // uniform_aspect = GL.GetUniformLocation(BasicProgramID, "aspect");

            // GL.GenBuffers(1, out vboVertexPosition);

            BasicProgramID = GL.CreateProgram(); // создание объекта программы

            loadShader("..\\..\\raytracing.vert", ShaderType.VertexShader, BasicProgramID,

            out BasicVertexShader);

            loadShader("..\\..\\raytracing.frag", ShaderType.FragmentShader, BasicProgramID,

            out BasicFragmentShader);

            GL.LinkProgram(BasicProgramID);

            // Проверяем успех компоновки

            int status = 0;

            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);

            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));

            vertdata = new Vector3[] {new Vector3(-1f, -1f, 0f),
                                      new Vector3( 1f, -1f, 0f),
                                      new Vector3( 1f, 1f, 0f),
                                      new Vector3(-1f, 1f, 0f) };
            GL.GenBuffers(1, out vboVertexPosition);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertexPosition);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.Uniform3(uniform_pos, camera_position);
            GL.Uniform1(uniform_aspect, aspect);
            GL.UseProgram(BasicProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }






    }
}
