using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL.Models;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGL1_LV
{
    internal class Game2 : GameWindow
    {
        int width, height;
        float[] vertices = // vertex
        {
            0.5f, 0.5f, 0f, 1.0f, 0.0f, 0.0f,      // 0f, -0.5f, 0f
            0.5f, -0.5f, 0f, 0.0f, 1.0f, 0.0f,   // 0.5f, 0.5f, 0f
            -0.5f, -0.5f, 0f, 0.0f, 0.0f, 1.0f,   // -0.5f, 0.5f, 0f
            -0.5f, 0.5f, 0f, 0.0f, 1.0f, 0.0f
        };
        uint[] indices =
        {
            0, 1, 2,
            2, 3, 0
        };

        float[] vertices2 =
        {
            -0.8f, 0.9f, 0f,
            -0.2f, 0.6f, 0f,
            -0.7f, 0f, 0f
        };
        int vao, vbo, ebo;
        int vao2, vbo2;
        int shaderProgram;
        int shaderProgram2;

        Logo logo;
        Logo logo2;
        Logo logo3;
        Logo logo4;


        public Game2(int width, int height)
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
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
            logo = new Logo();
            logo2 = new Logo();
            logo3 = new Logo();
            logo4 = new Logo();

            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * sizeof(float),
                vertices,
                BufferUsageHint.StaticDraw
            );
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                indices.Length * sizeof(uint),
                indices,
                BufferUsageHint.StaticDraw);
            //apos
            GL.VertexAttribPointer(
                 0,
                 3,
                 VertexAttribPointerType.Float,
                 false,
                 6 * sizeof(float),
                 0
             );
            GL.EnableVertexAttribArray(0);
            //acol
            GL.VertexAttribPointer(
                 1,
                 3,
                 VertexAttribPointerType.Float,
                 false,
                 6 * sizeof(float),
                 3 * sizeof(float)
             );
            GL.EnableVertexAttribArray(1);

            vao2 = GL.GenVertexArray();
            vbo2 = GL.GenBuffer();

            GL.BindVertexArray(vao2);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo2);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices2.Length * sizeof(float),
                vertices2,
                BufferUsageHint.StaticDraw
            );
            //apos
            GL.VertexAttribPointer(
                0,
                3,
                VertexAttribPointerType.Float,
                false,
                6 * sizeof(float),
                0
            );
            GL.EnableVertexAttribArray(0);
            //acol
            GL.VertexAttribPointer(
                 0,
                 3,
                 VertexAttribPointerType.Float,
                 false,
                 6 * sizeof(float),
                 3 *  sizeof(float)
             );
            GL.EnableVertexAttribArray(1);

            // Create ShaderProgram
            shaderProgram = GL.CreateProgram();
            shaderProgram2 = GL.CreateProgram();

            // Vertex Shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShader("Default.vert"));
            GL.CompileShader(vertexShader);
            string logs = GL.GetShaderInfoLog(vertexShader);
            Console.WriteLine(logs);

            // Fragment Shader
            int fragmentShader = GL.CreateShader(
                ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShader("Default.frag"));
            GL.CompileShader(fragmentShader);
            logs = GL.GetShaderInfoLog(fragmentShader);
            Console.WriteLine(logs);

            // Fragment Shader
            int fragmentShader2 = GL.CreateShader(
                ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader2, LoadShader("Default1.frag"));
            GL.CompileShader(fragmentShader2);
            logs = GL.GetShaderInfoLog(fragmentShader2);
            Console.WriteLine(logs);

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            GL.AttachShader(shaderProgram2, vertexShader);
            GL.AttachShader(shaderProgram2, fragmentShader2);
            GL.LinkProgram(shaderProgram2);


            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(fragmentShader2);
        }
        
        public static string LoadShader(string path)
        {
            using (StreamReader reader = new StreamReader("../../../Shaders/" + path))
            {
                return reader.ReadToEnd();
            }
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
            GL.DeleteProgram(shaderProgram);
            GL.DeleteProgram(shaderProgram2);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {

            double timeValue = GLFW.GetTime();
            float value = (float)Math.Sin(timeValue);

            //Console.WriteLine(value);//rada frame times

            GL.ClearColor(0.05f, 0.2f, 0.75f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            int colourLocation = GL.GetUniformLocation(shaderProgram2, "Default3"); //krasa
            int xOffSetLocation = GL.GetUniformLocation(shaderProgram2, "offset"); 

            GL.UseProgram(shaderProgram2);

            GL.Uniform4(colourLocation, value, 0.2f, 0.0f, 1.0f);

            GL.Uniform3(xOffSetLocation, logo.pos);
            logo.Move();
            logo.Draw();

            GL.Uniform3(xOffSetLocation, logo2.pos);
            logo2.Move();
            logo2.Draw();

            GL.Uniform3(xOffSetLocation, logo3.pos);
            logo3.Move();
            logo3.Draw();

            GL.Uniform3(xOffSetLocation, logo4.pos);
            logo4.Move();
            logo4.Draw();


            //GL.BindVertexArray(vao);
            //GL.PolygonMode(TriangleFace.FrontAndBack, PolygonMode.Line);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            //GL.DrawElements(
            //    PrimitiveType.Triangles,
            //    indices.Length,
            //    DrawElementsType.UnsignedInt,
            //    0);
            //GL.UseProgram(shaderProgram2);
            //GL.BindVertexArray(vao2);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
