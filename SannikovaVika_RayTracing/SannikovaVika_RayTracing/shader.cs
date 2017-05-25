using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
//using OpenTK.Input;

namespace SannikovaVika_RayTracing
{
    class shader
    {
        //int a, b, c, d;
        //static float x, y, z;
        //Vector3 delta = new Vector3(-3.0f, -8.0f, -2.0f);

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
       
        public void Refresh(Vector3 delta)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Draw(delta);
        }
        public void Draw(Vector3 delta)
        {
            GL.UseProgram(BasicProgramID);
            GL.Uniform3(GL.GetUniformLocation(BasicProgramID, "delta"), delta);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            GL.DisableVertexAttribArray(0);
            GL.UseProgram(0);
        }

    }        
 }

