using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;

namespace SpecialOlympics.Utils
{
    public static class Utils
    {

        /// <summary>
        /// Convierte un archivo de un formulario (Imagen, PDF ...) en un array de bytes
        /// </summary>
        /// <param name="formFile">File obtenido del formulario</param>
        /// <returns></returns>
        public static byte[] GetByteArrayFromFile(IFormFile formFile)
        {
            byte[] fileBytes = null;
            if (formFile != null && formFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }
            return fileBytes;
        }

        ///// <summary>
        ///// Convierte un array de bytes en una variable archivo de un formulario (FormFile) (Imagen, PDF ...)
        ///// Usado por ejemplo para la foto de perfil
        ///// </summary>
        ///// <param name="byteArray">Byte array</param>
        ///// <param name="formFileName">Nombre que se le quiere dar al FormFile</param>
        ///// <returns></returns>
        //public static IFormFile GetFormFileFromByteArray(byte[] byteArray, string formFileName)
        //{
        //    FormFile formFile = null;
        //    if (byteArray == null)
        //        return formFile;

        //    var fileMock = new Mock<IFormFile>();
        //    var ms = new MemoryStream(byteArray);
        //    var fileName = formFileName;
        //    //Setup mock file using info from physical file
        //    fileMock.Setup(_ => _.FileName).Returns(fileName);
        //    fileMock.Setup(_ => _.Length).Returns(ms.Length);
        //    fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        //    fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));

        //    return fileMock.Object;
        //}

        /// <summary>
        /// Convierte un archivo local en una variable de archivo de un formulario (FormFile) (Imagen, PDF ...)
        /// Usado por ejemplo para la foto de perfil
        /// </summary>
        /// <param name="localFileName">Nombre del archivo que se quiere convertir a FormFile</param>
        /// <param name="localFileFolder">Nombre de la carpeta donde se encuentra el archivo</param>
        /// <returns></returns>
        public static FormFile GetFormFileFromLocalFile(string localFileName, string localFileFolder)
        {
            FormFile formFile = null;
            if (string.IsNullOrEmpty(localFileName) || string.IsNullOrEmpty(localFileFolder))
                return formFile;

            string path = Path.Combine(localFileFolder, localFileName);
            
            try
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(path)))
                {
                    formFile = new FormFile(stream, 0, stream.Length, localFileName.Substring(8, localFileName.Length - 8), localFileName);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return formFile;
        }

        /// <summary>
        /// Guarda un archivo pasado por parámetro en la ruta establecida en el segundo parámetro
        /// </summary>
        /// <param name="localDirectoryPath">Directorio local dentro de wwwroot donde se guardará el archivo</param>
        /// <param name="file">Archivo dado por el usuario que hay que guardar en la carpeta local</param>
        /// <param name="oldName">Contiene la ruta que tenía el archivo anteriormente para eliminarlo</param>
        /// <returns></returns>
        public static string SaveFileInLocalFolder(IFormFile file, string localDirectoryPath, string oldName)
        {
            if (!Directory.Exists(localDirectoryPath))
            {
                Directory.CreateDirectory(localDirectoryPath);
            }

            // Eliminamos el archivo viejo para sobreescribirlo
            if (!string.IsNullOrEmpty(oldName))
            {
                string pathAnterior = Path.Combine(localDirectoryPath, oldName);
                if (File.Exists(pathAnterior))
                    File.Delete(pathAnterior);
            }
            // TODO: Implementar Encode limpiando carácteres raros como tildes, apóstrofes etc.
            //string fileName = WebUtility.HtmlEncode(Path.GetFileName(file.FileName)); // Codificamos el nombre del archivo dado por el usuario           
            string fileName = Path.GetFileName(file.FileName);
            string uniqueName = Guid.NewGuid().ToString().Substring(0, 8) + fileName;
            string uniquePath = Path.Combine(localDirectoryPath, uniqueName); // Combinamos el directorio de wwwroot con el nombre del archivo

            using (FileStream stream = new FileStream(uniquePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return uniqueName;
        }

        /// <summary>
        /// TODO: Implementar esta función que limpie tildes y carácteres raros de FileNames para poder usar HTMLEncode()
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ReplaceInvalidChars(this string source)
        {
            string result = source;


            return result;
        }
    }
}
