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

        public string glVersion;
        public string glslVersion;
        public int attribute_vpos;
        int BasicProgramID; //Номер дескриптора на графической карте
        int BasicVertexShader; //Адрес вершинного шейдера  
        int BasicFragmentShader; //Адрес фрагментного шейдера
        //int attribute_vpos; //Адрес параметра позиции
        int vboVertexPosition; //Адрес буфера вершин объекта для нашего параметра позиции
        int uniform_pos;
        int uniform_aspect;
        float aspect;
        Vector3 camera_position;
        Vector3[] vertdata; //Массив позиций вершин


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

        public void InitShaders(float character_size)
        {
            BasicProgramID = GL.CreateProgram(); // создание объекта программы
            loadShader("..\\..\\raytracing.vert", ShaderType.VertexShader, BasicProgramID, out BasicVertexShader);
            loadShader("..\\..\\raytracing.frag", ShaderType.FragmentShader, BasicProgramID, out BasicFragmentShader);
            // компановка программы
            GL.LinkProgram(BasicProgramID);

            // Проверяем успех компоновки

            int status = 0;
            GL.GetProgram(BasicProgramID, GetProgramParameterName.LinkStatus, out status);

            //Console.WriteLine("InfoLog:");

            Console.WriteLine(GL.GetProgramInfoLog(BasicProgramID));
            vertdata = new Vector3[] {new Vector3(-1f, -1f, 0f),
                                      new Vector3( 1f, -1f, 0f),
                                      new Vector3( 1f, 1f, 0f),
                                      new Vector3(-1f, 1f, 0f) };

            //рисуем квад
            //создали один буфер, связали с атрибутом и заполнили данными
            //vbo_position
            GL.GenBuffers(1, out vboVertexPosition);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboVertexPosition);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);
            //vbo
            GL.Uniform3(uniform_pos, camera_position); //этого нет
            GL.Uniform1(uniform_aspect, aspect);       //этого тоже
            GL.UseProgram(BasicProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        }        
    }
}
