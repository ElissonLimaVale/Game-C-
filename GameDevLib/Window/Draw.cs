using System;
using OpenTK.Graphics.OpenGL;


namespace GameDevlib.Window {

    public static class Draw {
        public static float[] Triangulo = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f,  //Top vertex
            -0.5f,  0.5f, 0.0f
        };

        public static void Quad(int X, int Y, int Width, int Height){
            GL.Begin(PrimitiveType.Quads);
                GL.Vertex2(-0.5f * Width + X, -0.5f * Height + Y);
                GL.Vertex2(0.5f * Width + X, -0.5f * Height + Y);
                GL.Vertex2(0.5f * Width + X, 0.5f * Height + Y);
                GL.Vertex2(-0.5f * Width + X, 0.5f * Height + Y);
            GL.End();
        }

    }

}