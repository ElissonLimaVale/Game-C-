using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GameDevlib.Window {

    public class Window : GameWindow
    {
        private readonly int _width;
        private readonly int _height;
        private int VertexBufferObject, VertexArrayObject;
        private Shader _shader = new Shader();
        public Window( int width, int height, string title = "GameDev - Bing"
            ) : base(new GameWindowSettings(), 
            new NativeWindowSettings { 
                Title = title, 
                Size = new OpenTK.Mathematics.Vector2i{ X = width, Y = height } 
            }
        )
        {
            this._width = width;
            this._height = height;
        }

        public override void Run()
        {
            base.CenterWindow();
            base.Run();
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            VertexBufferObject = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Draw.Triangulo.Length * sizeof(float), Draw.Triangulo, BufferUsageHint.DynamicDraw);
            _shader = new Shader("shader.vert", "shader.frag");
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _shader.Use();
            
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args){
            GL.Viewport(0,0,ClientSize.X,ClientSize.Y);

            // _shader.Use();
            GL.UseProgram(_shader._handler);
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0,0,ClientSize.X,ClientSize.Y);
            base.OnResize(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if(KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space)){
                Close();
            }
            base.OnUpdateFrame(args);
        }

        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            _shader.Dispose();
            base.OnUnload();
        }
    }
}

