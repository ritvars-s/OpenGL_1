using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Models
{
    internal class Logo
    {
        private float[] vertices;
        private uint[] indices;

        private Vector2 direction;
        private float velocity = 0.00015f;
        public Vector3 pos = new Vector3(0, 0, 0);

        private int vao, vbo, ebo;

        private Random random = new Random();

        public Logo() {
            this.direction = new Vector2(
                    random.Next(-1000, 1000),
                    random.Next(-1000, 1000)
                );
            this.direction.Normalize();

            setVertices();
            setVAO();
        }


        private void setVertices() {
            this.vertices = // vertex
               [
                0.5f, 0.5f, 0f, 1.0f, 0.0f, 0.0f,      // 0f, -0.5f, 0f
                0.5f, -0.5f, 0f, 0.0f, 1.0f, 0.0f,   // 0.5f, 0.5f, 0f
                -0.5f, -0.5f, 0f, 0.0f, 0.0f, 1.0f,   // -0.5f, 0.5f, 0f
                -0.5f, 0.5f, 0f, 0.0f, 1.0f, 0.0f
                ];
            this.indices =
                [
                0, 1, 2,
                2, 3, 0
                ];
        }

        private void setVAO() {

            this.vao = GL.GenVertexArray();
            this.vbo = GL.GenBuffer();
            this.ebo = GL.GenBuffer();

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                this.vertices.Length * sizeof(float),
                this.vertices,
                BufferUsageHint.StaticDraw
            );
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(
                BufferTarget.ElementArrayBuffer,
                this.indices.Length * sizeof(uint),
                this.indices,
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

        }
        public void Move() {
            if (pos.X <= -0.5 || pos.X >= 0.5) {

                this.direction.X = -direction.X;

                

            }
            if (pos.Y >= 0.5 || pos.Y <= -0.5) {

                this.direction.Y = -direction.Y;

            }
            pos += new Vector3(velocity * direction.X, velocity * direction.Y, 0);
        
        }
        public void Draw() {
            GL.BindVertexArray(this.vao);
            GL.DrawElements(
                PrimitiveType.Triangles,
                this.indices.Length,
                DrawElementsType.UnsignedInt,
                0);
        }

        
    }
}
