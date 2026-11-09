using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace OpenGL
{
    internal class Game : GameWindow
    {
        int width, height;
        float[] vertices = { //1 vertex vairaki vertices
            0.5f, 0.5f, 0,
            0.5f, -0.5f, 0,
            -0.5f, -0.5f, 0,
        };
        float[] vertices2 = { 
            -0.5f, -0.5f, 0,
            -0.5f, 0.5f, 0,
            0.5f, 0.5f, 0
        };
        //VBO - vertex buffer object nosaka atminas lielumu
        //VAO - vertex array object pasaka kadas vertiabs padod un pasaka ka tas sadala
        //EBO - element buffer obeject izmanto indeksus

        uint[] indexes = { };
        int vao, vbo, vao2, vbo2;
        int shaderProgram, shaderProgram2;

        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default) { //base ir taka super no javas
            this.width = width;
            this.height = height;

            CenterWindow(new Vector2i(width, height));
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();

            //vertices1
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo); //nodefine kads bus tips
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0); //nodefine ka sadalam
            GL.EnableVertexAttribArray(0);

            //vertices2 =================
            vao2 = GL.GenVertexArray();
            vbo2 = GL.GenBuffer();

            GL.BindVertexArray(vao2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo2); //nodefine kads bus tips
            GL.BufferData(BufferTarget.ArrayBuffer, vertices2.Length * sizeof(float), vertices2, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0); //nodefine ka sadalam
            GL.EnableVertexAttribArray(0);
            //=============
            //Create ShaderProgram
            shaderProgram = GL.CreateProgram();
            shaderProgram2 = GL.CreateProgram();

            //Vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShader("Default.vert"));
            GL.CompileShader(vertexShader);
            string logs = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(logs);
            //Frag shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShader("Default.frag"));
            GL.CompileShader(fragmentShader); 
            string logs1 = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(logs1);
            //vertices2
            //nevajag jaunu vert shaderi jo ieprieksejasi strada ja citu sakuma poz tad vajag jaunu            
            //Frag shader
            int fragmentShader2 = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader2, LoadShader("Default1.frag"));
            GL.CompileShader(fragmentShader2);
            string logs4 = GL.GetShaderInfoLog(fragmentShader2);
            Console.WriteLine(logs4);

            //===========

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);


            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            //vertices2
            GL.AttachShader(shaderProgram2, vertexShader);
            GL.AttachShader(shaderProgram2, fragmentShader2);
            GL.LinkProgram(shaderProgram2);


            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader2);
        }

        public static string LoadShader(string path)
        {
            using (StreamReader reader = new StreamReader("../../../Shaders/" + path))
            {
                return reader.ReadToEnd();
            }

        }
        protected override void OnUnload()//izsaukta kad izledz lai izdzestu atminu lia nav atminas overload
        {
            base.OnUnload();

            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);

            GL.DeleteBuffer(vbo2);
            GL.DeleteVertexArray(vao2);

            GL.DeleteProgram(shaderProgram);
            GL.DeleteProgram(shaderProgram2);

        }
        protected override void OnUpdateFrame(FrameEventArgs args) //izmantot ad kad ivada input piem noklikskina peli unzspiez taustinu
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.05f, 0.2f, 0.75f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(shaderProgram);
            GL.BindVertexArray(vao);
            //GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line); //Ar linijam
       
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            //verteces2
            GL.UseProgram(shaderProgram2);
            GL.BindVertexArray(vao2);
            //GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }



    }
}
