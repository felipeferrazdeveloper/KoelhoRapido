using IniParser;
using IniParser.Model;
using System;
using System.Web;

namespace KoelhoRapido.Model.Utils
{
    public class IniUtils
    {
        public static IniData LerArquivoIni()
        {
            try
            {
                var dir = Environment.CurrentDirectory;
                var file = dir + "/Config/DbConfig.ini";
                if (HttpContext.Current != null)
                {
                    file = HttpContext.Current.Server.MapPath("/Config/DbConfig.ini").Replace("\\", "/");
                }

                if (!System.IO.File.Exists(file))
                {
                    throw new Exception("Arquivo de configuração inexisten");
                }

                var parser = new FileIniDataParser();

                return parser.ReadFile(file);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível ler o ini.", ex);
            }
        }
    }
}
