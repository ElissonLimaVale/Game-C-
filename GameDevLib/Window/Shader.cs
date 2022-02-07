using System;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace GameDevlib.Window {
    
    public class Shader {
        public int _handler;
        private string _vertexShaderSource = string.Empty;
        private string _fragmentShaderSource = string.Empty;
        private bool _disposedValue = false;
        public Shader(){ }
        public Shader(string vertexPath, string fragmentPath){
            this.ShadersCompile(vertexPath, fragmentPath);
        }


        private void ShadersCompile(string vertexPath, string fragmentPath) {
            // carrega o código fonte dos shaders em GLSL (OpenGL Shading Language)
            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                _vertexShaderSource = reader.ReadToEnd();
            }
            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                _fragmentShaderSource = reader.ReadToEnd();
            }
            // Gera os sombreadore e vincula  código fonte a eles
            var VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, _vertexShaderSource);

            var FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, _fragmentShaderSource);

            // compila os shaders e valida se gerou exceções
            // Se houver algum erro ao compilar, você poderá obter a string de depuração com a função GL.GetShaderInfoLog
            GL.CompileShader(VertexShader);

            string infoLogVert = GL.GetShaderInfoLog(VertexShader);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(FragmentShader);

            string infoLogFrag = GL.GetShaderInfoLog(FragmentShader);

            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);

            _handler = GL.CreateProgram();

            GL.AttachShader(_handler, VertexShader);
            GL.AttachShader(_handler, FragmentShader);

            GL.LinkProgram(_handler);
            
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(_handler, attribName);
        }

        public void Use()
        {
            GL.UseProgram(_handler);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                GL.DeleteProgram(_handler);
                _disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(_handler);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}

