using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;

namespace Eagle.Common.Utilities
{
    public class FileDetail
    {
        public string DisplayName { get; set; }
        public string FilePath { get; set; }
    }
    public static class FileUtils
    {
        public static void CreateFileFromStream(string file_path, Stream stream)
        {
            int bufferSize = (int)stream.Length;
            if (bufferSize == 0) return;
            using (FileStream fileStream = System.IO.File.Create(file_path, bufferSize))
            {
                byte[] buffer = new byte[bufferSize];
                stream.Read(buffer, 0, Convert.ToInt32(buffer.Length));
                fileStream.Write(buffer, 0, buffer.Length);
            }
        }

        private static bool CreateByteArrayToFile(string file_path, byte[] buffer)
        {
            bool result = false;
            try
            {
                System.IO.FileStream file_stream = new System.IO.FileStream(file_path, FileMode.Create, FileAccess.Write);
                file_stream.Write(buffer, 0, buffer.Length);
                file_stream.Close();
                result = true;
            }
            catch (Exception ex) { ex.ToString(); result = false; }
            return result;
        }

        public static void DownloadFile(string _URL, string _SaveAs)
        {
            try
            {
                System.Net.WebClient _WebClient = new System.Net.WebClient();
                // Downloads the resource with the specified URI to a local file.
                _WebClient.DownloadFile(_URL, _SaveAs);
            }
            catch (Exception _Exception)
            {
                _Exception.ToString();
            }
        }

        public static string DownloadHTMLPage(string _URL)
        {
            string _PageContent = null;
            try
            {
                // Open a connection
                System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

                // You can also specify additional header values like the user agent or the referer: (Optional)
                _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                _HttpWebRequest.Referer = "http://www.google.com/";

                // set timeout for 10 seconds (Optional)
                _HttpWebRequest.Timeout = 10000;

                // Request response:
                System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

                // Open data stream:
                System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

                // Create reader object:
                System.IO.StreamReader _StreamReader = new System.IO.StreamReader(_WebStream);

                // Read the entire stream content:
                _PageContent = _StreamReader.ReadToEnd();

                // Cleanup
                _StreamReader.Close();
                _WebStream.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                _Exception.ToString();
                return null;
            }

            return _PageContent;
        }

        public static byte[] GetBytesFromFile(string filePath)
        {
            FileStream fileStream;

            byte[] fileByte;

            using (fileStream = File.OpenRead(filePath))
            {
                fileByte = new byte[fileStream.Length];
                fileStream.Read(fileByte, 0, Convert.ToInt32(fileStream.Length));
            }

            return fileByte;
        }
        public static byte[] GetByteFromFileUpload(FileUpload FileUpload1)
        {
            //C1
            //FileInfo file_info = new FileInfo(FileUpload1.PostedFile.FileName);
            //byte[] file_content = null;
            //if (file_info.Exists)
            //{
            //    file_content = new byte[file_info.Length];
            //    FileStream imagestream = file_info.OpenRead();
            //    imagestream.Read(file_content, 0, file_content.Length);
            //    imagestream.Close();
            //}

            //C2:
            byte[] file_content = FileUpload1.FileBytes;
            FileUpload1.PostedFile.InputStream.Read(file_content, 0, file_content.Length);
            //C3:
            //byte[] file_content = new byte[FileUpload1.PostedFile.InputStream.Length];
            //FileUpload1.PostedFile.InputStream.Seek(0, SeekOrigin.Begin);
            //FileUpload1.PostedFile.InputStream.Read(file_content, 0, file_content.Length);
            //FileUpload1.PostedFile.InputStream.Write(file_content,0, file_content.Length);   

            return file_content;
        }
        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(string srcFile)
        {
            // Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;

            // Detect byte order mark if any - otherwise assume default

            byte[] buffer = new byte[5];
            FileStream file = new FileStream(srcFile, FileMode.Open);
            file.Read(buffer, 0, 5);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = Encoding.UTF32;

            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = Encoding.UTF7;

            return enc;
        }

        public static void GetFileContent(string filepath)
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)HttpWebRequest.Create(filepath);
            System.IO.StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
            string n = sr.ReadLine();
            request.GetResponse().Close();
        }

        public static byte[] GetByteFromFilePath(string file_path)
        {
            //Get byte array of file
            byte[] byteArray = null;

            if (File.Exists(file_path))
            {
                byteArray = File.ReadAllBytes(file_path);
            }
            return byteArray;

            //Write byte array to file
            //C1: Response.BinaryWrite(byteArray);
            //C2: Response.OutputStream.Write(byteArray,0,byteArray.Length);
            //Set Content Type => Response.ContentTyoe="images/png";
        }


        public static byte[] GetByteFromHtmlInputFile(HtmlInputFile InputFile)
        {
            HttpPostedFile myFile = InputFile.PostedFile;
            byte[] file_content = null;
            if (InputFile.PostedFile != null)
            {
                int nFileLen = myFile.ContentLength;
                file_content = new byte[nFileLen];
                myFile.InputStream.Read(file_content, 0, nFileLen);
            }
            return file_content;
        }

        public static string GetFileExtension(string fileName)
        {
            string ext = string.Empty;
            int fileExtPos = fileName.LastIndexOf(".", StringComparison.Ordinal);
            if (fileExtPos >= 0)
                ext = fileName.Substring(fileExtPos, fileName.Length - fileExtPos);

            return ext;
        }

        public static string GetFileName(string FullFileName)
        {
            string ext = string.Empty;
            int fileExtPos = FullFileName.LastIndexOf(".", StringComparison.Ordinal);
            if (fileExtPos >= 0)
                ext = FullFileName.Substring(0, FullFileName.Length - fileExtPos);

            return ext;
        }

        public static string GenerateEncodedFileNameWithDate(string strFileInput, string strKeys)
        {
            //Current Date
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            culture.DateTimeFormat.DateSeparator = string.Empty;
            culture.DateTimeFormat.ShortDatePattern = "yyyyMMdd";
            culture.DateTimeFormat.LongDatePattern = "yyyyMMdd";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            string current_date_yyyymmdd_hhmmss_mmm = DateTime.Now.ToString("yyyyMMdd-hhmmss") + DateTime.Now.Millisecond.ToString();

            strFileInput = new Regex(@"[\s+\\\/:\*\?\@\&\#\$\^\%\--\+\;\,\“\”\""\''<>|]").Replace(StringUtils.ConvertToUnSign(strFileInput), string.Empty);

            string extension = Path.GetExtension(strFileInput);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(strFileInput);
            string encode_result = string.Empty;
            if (strKeys != null && strKeys != string.Empty)
                encode_result = fileNameWithoutExtension + "-" + current_date_yyyymmdd_hhmmss_mmm + "-" + strKeys + extension;
            else
                encode_result = fileNameWithoutExtension + "-" + current_date_yyyymmdd_hhmmss_mmm + extension;
            return encode_result;
        }

        public static string GenerateEncodedFileNameWithDate(string strFileInput)
        {
            //Current Date
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            culture.DateTimeFormat.DateSeparator = string.Empty;
            culture.DateTimeFormat.ShortDatePattern = "yyyyMMdd";
            culture.DateTimeFormat.LongDatePattern = "yyyyMMdd";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            string current_date_yyyymmdd_hhmmss_mmm = DateTime.Now.ToString("yyyyMMdd-hhmmss") + DateTime.Now.Millisecond.ToString();

            strFileInput = new Regex(@"[\s+\\\/:\*\?\@\&\#\$\^\%\--\+\;\,\“\”\""\''<>|]").Replace(StringUtils.ConvertToUnSign(strFileInput), string.Empty);

            string extension = Path.GetExtension(strFileInput);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(strFileInput);
            string encode_result = fileNameWithoutExtension + "-" + current_date_yyyymmdd_hhmmss_mmm + extension;
            return encode_result;
        }

        public static string GenerateEncodedFileNameWithDateGuid(string strFileInput)
        {
            //Current Date
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            culture.DateTimeFormat.DateSeparator = string.Empty;
            culture.DateTimeFormat.ShortDatePattern = "yyyyMMdd";
            culture.DateTimeFormat.LongDatePattern = "yyyyMMdd";
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            string current_date_yyyymmdd_hhmmss_mmm = DateTime.Now.ToString("yyyyMMdd-hhmmss") + DateTime.Now.Millisecond.ToString();

            string extension = Path.GetExtension(strFileInput);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(strFileInput);
            string encode_result = fileNameWithoutExtension + "-" + current_date_yyyymmdd_hhmmss_mmm + "-" + Guid.NewGuid() + extension;
            return encode_result;
        }


        public static bool IsFileUsedbyAnotherProcess(string filePath)
        {
            try
            {
                File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (System.IO.IOException ex)
            {
                ex.ToString();
                return true;
            }
            return false;
        }
        public static bool IsImage(HttpPostedFileBase postedFile)
        {
            bool result = false;
            try  {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                    if (!bitmap.Size.IsEmpty)
                    {
                        result = true;
                        
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public static bool IsImage(HttpPostedFileBase postedFile, out int? width, out int? height)
        {
            bool result = false;
            width = null;
            height = null;
            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                    if (!bitmap.Size.IsEmpty)
                    {
                        result = true;
                        width = bitmap.Width;
                        height = bitmap.Height;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        //public static void WriteResX(string fileName, List<KeyValue> lstResDef)
        //{
        //    try
        //    {
        //        using (ResXResourceWriter writer = new ResXResourceWriter(fileName))
        //        {
        //            foreach (KeyValue obj in lstResDef)
        //                writer.AddResource(obj.Key, obj.Value);
        //            writer.Generate();
        //        }
        //    }
        //    catch { throw new Exception("Error while saving " + fileName); }
        //}


        public static string ReplaceBackSlash(string filepath)
        {
            if (filepath != null)
            {
                filepath = filepath.Replace("\\", "/");
            }
            return filepath;
        }

       
        public static string UploadFile(HttpPostedFileBase FileUpload)
        {
            string fileName = string.Empty;
            string physicalUploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
            if (FileUpload!=null)
            {
                if (FileUpload.ContentLength > 0)
                {                    
                    string extension = Path.GetExtension(FileUpload.FileName);
                    string newFileName = String.Format("{0}{1}", Guid.NewGuid(), extension);

                    if (System.IO.Directory.Exists(physicalUploadPath))
                    {
                        string filePath = System.IO.Path.Combine(physicalUploadPath, newFileName);
                        if (!System.IO.File.Exists(filePath))
                        {
                            FileUpload.SaveAs(filePath);
                            fileName = newFileName;
                        }
                    }
                }                
            }
            return fileName;
        }

        public static string[] UploadFile(HttpPostedFileBase FileUpload, string VirtualUploadPath)
        {
            string[] fileNameArray = new string[2];
            string physicalUploadPath = HttpContext.Current.Server.MapPath(VirtualUploadPath);

            if (FileUpload.ContentLength > 0)
            {
                string extension = Path.GetExtension(FileUpload.FileName);
                string fileName = Path.GetFileNameWithoutExtension(FileUpload.FileName);
                string newFileName = String.Format("{0}{1}{2}", fileName, Guid.NewGuid(), extension);

                if (System.IO.Directory.Exists(physicalUploadPath))
                {
                    string filePath = System.IO.Path.Combine(physicalUploadPath, newFileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        FileUpload.SaveAs(filePath);
                        fileNameArray[0] = fileName;
                        fileNameArray[1] = newFileName;
                    }
                }
            }
            return fileNameArray;
        }

        public static string UploadFile(HttpPostedFile FileUpload, string VirtualUploadPath, bool IsFolderCreatedByDate)
        {
            string result = string.Empty, file_path = string.Empty;
            if (FileUpload != null)
            {
                string physicalUploadPath = HttpContext.Current.Server.MapPath(VirtualUploadPath);
                if (Directory.Exists(physicalUploadPath))
                {
                    string file_name = System.IO.Path.GetFileName(FileUpload.FileName);
                    string new_file_name = FileUtils.GenerateEncodedFileNameWithDateGuid(file_name);

                    if (IsFolderCreatedByDate)
                    {
                        string date = DateTime.Today.Day.ToString();
                        string month = DateTime.Today.Month.ToString();
                        string year = DateTime.Today.Year.ToString();

                        if (!System.IO.Directory.Exists(physicalUploadPath))
                            System.IO.Directory.CreateDirectory(physicalUploadPath);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year + "\\" + month))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year + "\\" + month);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year + "\\" + month + "\\" + date))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year + "\\" + month + "\\" + date);

                        physicalUploadPath = physicalUploadPath + "\\" + year + "\\" + month + "\\" + date;
                        result = year + "/" + month + "/" + date + "/" + new_file_name;
                    }
                    else
                        result = new_file_name;

                    file_path = Path.Combine(physicalUploadPath, HttpContext.Current.Server.HtmlEncode(new_file_name));
                    FileUpload.SaveAs(file_path);
                }
            }
            return result;
        }

        public static string[] UploadFileWithThumbnail(HttpPostedFile FileUpload, string VirtualUploadPath, int? resized_img_width = 120, int? resized_img_height = 120, bool IsFolderCreatedByDate = false)
        {
            //begin xu ly file upload=======================================================================================================            
            string[] result = new string[2];
            if (FileUpload !=null)
            {
                string file_name = System.IO.Path.GetFileNameWithoutExtension(FileUpload.FileName);
                string file_ext = System.IO.Path.GetExtension(FileUpload.FileName).ToLower().Trim();
                string new_file_name = FileUtils.GenerateEncodedFileNameWithDateGuid(file_name);
                string main_image_name = HttpContext.Current.Server.HtmlEncode(new_file_name + file_ext);
                string front_image_name = HttpContext.Current.Server.HtmlEncode(string.Format("{0}_thumb{1}", new_file_name, file_ext));


                string physicalUploadPath = HttpContext.Current.Server.MapPath(VirtualUploadPath);
                if (Directory.Exists(physicalUploadPath))
                {
                    if (IsFolderCreatedByDate)
                    {
                        string date = DateTime.Today.Day.ToString();
                        string month = DateTime.Today.Month.ToString();
                        string year = DateTime.Today.Year.ToString();

                        if (!System.IO.Directory.Exists(physicalUploadPath))
                            System.IO.Directory.CreateDirectory(physicalUploadPath);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year + "\\" + month))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year + "\\" + month);
                        if (!System.IO.Directory.Exists(physicalUploadPath + "\\" + year + "\\" + month + "\\" + date))
                            System.IO.Directory.CreateDirectory(physicalUploadPath + "\\" + year + "\\" + month + "\\" + date);

                        physicalUploadPath = physicalUploadPath + "\\" + year + "\\" + month + "\\" + date;

                        result[0] = (year + "\\" + month + "\\" + date + "\\" + main_image_name).Replace("\\", "/");
                        result[1] = (year + "\\" + month + "\\" + date + "\\" + front_image_name).Replace("\\", "/");
                    }
                    else
                    {
                        result[0] = main_image_name;
                        result[1] = front_image_name;
                    }

                    string main_image_path = Path.Combine(physicalUploadPath, main_image_name);
                    string front_image_path = Path.Combine(physicalUploadPath, front_image_name);

                    // Save main image================================================================================
                    System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload.InputStream);
                    image.Save(main_image_path);

                    System.Drawing.Image thumb = image.GetThumbnailImage((int)resized_img_width, (int)resized_img_height, null, IntPtr.Zero);
                    thumb.Save(front_image_path);
                    image.Dispose();
                    thumb.Dispose();
                }               
            }
            return result;
        }
                
        public static string UploadFileAndRemoveOldFile(HttpPostedFileBase FileUpload, string OldVirtualFilePath)
        {
            string fileName = string.Empty;
            string physicalUploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
            string physicalOldFilePath = HttpContext.Current.Server.MapPath(OldVirtualFilePath);

            if (FileUpload.ContentLength > 0)
            {
                string extension = Path.GetExtension(FileUpload.FileName);
                string newFileName = String.Format("{0}-{1}", Guid.NewGuid(), extension);

                if (System.IO.Directory.Exists(physicalUploadPath))
                {
                    if (OldVirtualFilePath != string.Empty && System.IO.File.Exists(physicalOldFilePath))
                        System.IO.File.Delete(physicalOldFilePath);

                    string filePath = System.IO.Path.Combine(physicalUploadPath, newFileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        FileUpload.SaveAs(filePath);
                        fileName = newFileName;
                    }
                }
            }
            return fileName;
        }

        public static string[] UploadFileAndRemoveOldFile(HttpPostedFileBase FileUpload, string OldVirtualFilePath, string VirtualUploadPath)
        {
            string[] fileNames = new string[2];
            string physicalUploadPath = HttpContext.Current.Server.MapPath(VirtualUploadPath);
            string physicalOldFilePath = HttpContext.Current.Server.MapPath(OldVirtualFilePath);

            if (FileUpload.ContentLength > 0)
            {       
                string extension = Path.GetExtension(FileUpload.FileName);
                string fileName = Path.GetFileNameWithoutExtension(FileUpload.FileName);
                string newFileName = String.Format("{0}-{1}{2}",fileName, Guid.NewGuid(), extension);

                if (System.IO.Directory.Exists(physicalUploadPath))
                {
                    if (OldVirtualFilePath != string.Empty && System.IO.File.Exists(physicalOldFilePath))
                        System.IO.File.Delete(physicalOldFilePath);

                    string filePath = System.IO.Path.Combine(physicalUploadPath, newFileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        FileUpload.SaveAs(filePath);
                        fileNames[0] = fileName;
                        fileNames[1] = newFileName;
                    }
                }
            }
            return fileNames;
        }

        public static string UploadAndResizeImage(HttpPostedFileBase FileUpload, string VirtualUploadPath, int Height, int Width)
        {
            string fileName = string.Empty;
            string physicalUploadPath = HttpContext.Current.Server.MapPath(VirtualUploadPath);

            if (FileUpload.ContentLength > 0)
            {
                string extension = Path.GetExtension(FileUpload.FileName);
                string newFileName = String.Format("{0}{1}", Guid.NewGuid(), extension);

                if (System.IO.Directory.Exists(physicalUploadPath))
                {
                    string filePath = System.IO.Path.Combine(physicalUploadPath, newFileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        FileUpload.SaveAs(filePath);
                        ResizeImage(filePath, 500, 500);
                        fileName = newFileName;
                    }
                }
            }
            return fileName;
        }

        public static string UploadAndResizeImageAndRemoveOldImage(HttpPostedFileBase FileUpload, string OldVirtualFilePath, int Height, int Width)
        {
            string fileName = string.Empty;
            string physicalUploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
            string physicalOldFilePath = HttpContext.Current.Server.MapPath(OldVirtualFilePath);

            if (FileUpload.ContentLength > 0)
            {
                string extension = Path.GetExtension(FileUpload.FileName);
                string newFileName = String.Format("{0}{1}", Guid.NewGuid(), extension);

                if (System.IO.Directory.Exists(physicalUploadPath))
                {
                    if (OldVirtualFilePath != string.Empty && System.IO.File.Exists(physicalOldFilePath))
                        System.IO.File.Delete(physicalOldFilePath);

                    string filePath = System.IO.Path.Combine(physicalUploadPath, newFileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        FileUpload.SaveAs(filePath);
                        ResizeImage(filePath, 500, 500);
                        fileName = newFileName;
                    }
                }
            }
            return fileName;
        }

        public static string UploadAndResizeImageAndRemoveOldImage(HttpPostedFileBase FileUpload, string OldVirtualFilePath, string VirtualUploadPath, int Height, int Width)
        {
            string fileName = string.Empty;
            string physicalUploadPath = HttpContext.Current.Server.MapPath(VirtualUploadPath);
            string physicalOldFilePath = HttpContext.Current.Server.MapPath(OldVirtualFilePath);

            if (FileUpload.ContentLength > 0)
            {
                string extension = Path.GetExtension(FileUpload.FileName);
                string newFileName = String.Format("{0}{1}", Guid.NewGuid(), extension);

                if (System.IO.Directory.Exists(physicalUploadPath))
                {
                    if (OldVirtualFilePath != string.Empty && System.IO.File.Exists(physicalOldFilePath))
                        System.IO.File.Delete(physicalOldFilePath);

                    string filePath = System.IO.Path.Combine(physicalUploadPath, newFileName);
                    if (!System.IO.File.Exists(filePath))
                    {
                        FileUpload.SaveAs(filePath);
                        ResizeImage(filePath, 500, 500);
                        fileName = newFileName;
                    }
                }
            }
            return fileName;
        }

        private static void ResizeImage(string lcFilename, int lnWidth = 150, int lnHeight = 150)
        {
            System.Drawing.Bitmap bmpOut = null;

            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                {
                    loBMP.Dispose();
                    return;
                }
                //return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }


                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return;
            }
            bmpOut.Save(lcFilename);
            bmpOut.Dispose();
        }

      

        #region VIDEO FILE ======================================================================================================
        public static string upload_video(HttpPostedFileBase VideoUpload, string video_dir_path)
        {

            string result = string.Empty;
            if (VideoUpload != null)
            {
                if (!System.IO.Directory.Exists(video_dir_path))
                    System.IO.Directory.CreateDirectory(video_dir_path);

                string file_name = System.IO.Path.GetFileNameWithoutExtension(VideoUpload.FileName);
                string file_ext = System.IO.Path.GetExtension(VideoUpload.FileName).ToLower().Trim();
                string file_type = VideoUpload.ContentType;

                bool allowedType = false;

                switch (file_type)
                {
                    case ("video/avi"):
                        allowedType = true;
                        break;
                    case ("video/flv"):
                        allowedType = true;
                        file_ext = ".flv";
                        break;
                    case ("video/mp4"):
                        allowedType = true;
                        break;
                    case ("video/mpeg"):
                        allowedType = true;
                        break;
                    case ("image/wav"):
                        allowedType = true;
                        break;
                    case ("application/x-shockwave-flash"):
                        allowedType = true;
                        break;
                    default:
                        allowedType = false;
                        break;
                }

                if (allowedType)
                {
                    string new_file_name_ext = StringUtils.GetEncodeString(file_name) + file_ext;
                    string file_path = Path.Combine(video_dir_path, HttpContext.Current.Server.HtmlEncode(new_file_name_ext));

                    if (File.Exists(file_path))
                        result = "0";
                    else
                    {
                        VideoUpload.SaveAs(file_path);
                        result = new_file_name_ext;
                    }
                }
                else
                {
                    result = "-1";
                }
            }
            return result;
        }
        #endregion ==============================================================================================================


        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static System.Drawing.Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] ConvertStreamToByteArray(HttpPostedFileBase FileUpload)
        {
            Stream fs = FileUpload.InputStream;
            BinaryReader br = new BinaryReader(fs);
            Byte[] fileContent = br.ReadBytes((Int32)fs.Length);
            return fileContent;
        }
        public static bool DeleteDirectory(string target_dir)
        {
            bool result = false;

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);

            return result;
        }
        public static bool DeleteDirectoryFiles(string target_dir, string ext_todelete)
        {
            bool result = false;

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);
            string[] ext_arr_todelete = ext_todelete.Split(',');
            foreach (string file in files)
            {
                foreach (string deleteFile in ext_arr_todelete)
                {
                    if (deleteFile.Contains(Path.GetExtension(file)))
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                }
            }

            return result;
        }

       

        /// <summary>
        /// Creates the folder if needed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static bool CreateFolderIfNeeded(string virtual_path)
        {
            bool result = true;
            if (!Directory.Exists(virtual_path))
            {
                try
                {
                    Directory.CreateDirectory(virtual_path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// Opens a stream reader with the appropriate text encoding applied.
        /// </summary>
        /// <param name="srcFile"></param>
        public static StreamReader OpenStreamReaderWithEncoding(string srcFile)
        {
            Encoding enc = GetFileEncoding(srcFile);
            return new StreamReader(srcFile, enc);
        }
        /// <summary>
        /// Returns the full path of a full physical filename
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string JustPath(string path)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            return fi.DirectoryName + "\\";
        }

        /// <summary>
        /// Returns a fully qualified path from a partial or relative
        /// path.
        /// </summary>
        /// <param name="Path"></param>
        public static string GetFullPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Returns a relative path string from a full path based on a base path
        /// provided.
        /// </summary>
        /// <param name="fullPath">The path to convert. Can be either a file or a directory</param>
        /// <param name="basePath">The base path on which relative processing is based. Should be a directory.</param>
        /// <returns>
        /// String of the relative path.
        /// 
        /// Examples of returned values:
        ///  test.txt, ..\test.txt, ..\..\..\test.txt, ., .., subdir\test.txt
        /// </returns>
        public static string GetRelativePath(string fullPath, string basePath)
        {
            // ForceBasePath to a path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slahes
            return relativeUri.ToString().Replace("/", "\\");


            //// Start by normalizing paths
            //fullPath = fullPath.ToLower();
            //basePath = basePath.ToLower();

            //if ( basePath.EndsWith("\\") ) 
            //    basePath = basePath.Substring(0,basePath.Length-1);
            //if ( fullPath.EndsWith("\\") ) 
            //    fullPath = fullPath.Substring(0,fullPath.Length-1);

            //// First check for full path
            //if ( (fullPath+"\\").IndexOf(basePath + "\\") > -1) 
            //    return  fullPath.Replace(basePath,".");

            //// Now parse backwards
            //string BackDirs = string.Empty;
            //string PartialPath = basePath;
            //int Index = PartialPath.LastIndexOf("\\");
            //while (Index > 0) 
            //{
            //    // Strip path step string to last backslash
            //    PartialPath = PartialPath.Substring(0,Index );

            //    // Add another step backwards to our pass replacement
            //    BackDirs = BackDirs + "..\\" ;

            //    // Check for a matching path
            //    if ( fullPath.IndexOf(PartialPath) > -1 ) 
            //    {
            //        if ( fullPath == PartialPath )
            //            // We're dealing with a full Directory match and need to replace it all
            //            return fullPath.Replace(PartialPath,BackDirs.Substring(0,BackDirs.Length-1) );
            //        else
            //            // We're dealing with a file or a start path
            //            return fullPath.Replace(PartialPath+ (fullPath == PartialPath ?  string.Empty : "\\"),BackDirs);
            //    }
            //    Index = PartialPath.LastIndexOf("\\",PartialPath.Length-1);
            //}

            //return fullPath;
        }

        public static string[] PopulateFileList(string[] srcDirs, string[] searchPatterns, SearchOption searchOption = SearchOption.AllDirectories)
        {
            var fileList = from path in srcDirs
                           from searchPattern in searchPatterns
                           from file in Directory.GetFiles(path, searchPattern, searchOption)
                           select file;
            string[] arrayFileList = fileList.ToArray();
            return arrayFileList;
        }


        public static System.IO.FileInfo[] PopulateSortedFileArrayList(string physicalSrcDirs, string[] searchPatterns, SearchOption searchOption = SearchOption.AllDirectories)
        {
            //HashSet<string> hashSet_FileTypes = new HashSet<string>(searchPatterns);
            //string[] fileInfo = Directory.GetFiles(physicalSrcDirs, "*.*", searchOption);
            //IEnumerable<string> fileList = searchPatterns.SelectMany(fileExtension => Directory.GetFiles(physicalSrcDirs, fileExtension).ToArray());
            //DirectoryInfo dirInfo = new DirectoryInfo(physicalSrcDirs);
            //System.IO.FileInfo[] fileInfoList = dirInfo.GetFiles("*.*", searchOption);          
            //IEnumerable<string> fileList = fileInfo.Where(f => hashSet_FileTypes.Contains(new System.IO.FileInfo(f).Extension,StringComparer.OrdinalIgnoreCase));            
            //string[] colection = Directory.GetFiles(physicalSrcDirs).Select(x => new System.IO.FileInfo(x).Name).ToArray();

            if (searchPatterns == null)
                throw new ArgumentNullException("No result found with the file extension" + searchPatterns);
            DirectoryInfo dirInfo = new DirectoryInfo(physicalSrcDirs);
            var allowedExtensions = new HashSet<string>(searchPatterns, StringComparer.OrdinalIgnoreCase);
            System.IO.FileInfo[] fileInfoList = dirInfo.EnumerateFiles().Where(f => allowedExtensions.Contains(f.Extension.ToLower())).ToArray();
            return fileInfoList;
        }

        public static System.IO.FileInfo[] PopulateSortedFileList(string physicalSrcDirs, string[] searchPatterns, SearchOption searchOption = SearchOption.AllDirectories)
        {
            HashSet<string> hashSet_FileTypes = new HashSet<string>(searchPatterns);
            //string[] fileInfo = Directory.GetFiles(physicalSrcDirs, "*.*", SearchOption.AllDirectories);
            //IEnumerable<string> fileList = fileInfo.Where(f => hashSet_FileTypes.Contains(new System.IO.FileInfo(f).Extension,StringComparer.OrdinalIgnoreCase));

            DirectoryInfo dirInfo = new DirectoryInfo(physicalSrcDirs);
            System.IO.FileInfo[] fileInfoList = dirInfo.GetFiles("*.*", searchOption);
            IEnumerable<System.IO.FileInfo> fileList = from file in fileInfoList
                                                       from ext in hashSet_FileTypes
                                                       where string.Equals(ext, file.Extension, StringComparison.OrdinalIgnoreCase)
                                                       orderby file.LastWriteTime
                                                       select file;
            return fileList.ToArray();
        }

        public static System.IO.FileInfo[] GetFileByExtensions(string srcDir, string[] fileExtension)
        {
            if (fileExtension == null) throw new ArgumentNullException("No found with the file extension");
            var allowedFileExtensions = new HashSet<string>(fileExtension, StringComparer.OrdinalIgnoreCase);
            DirectoryInfo dirInfo = new DirectoryInfo(srcDir);
            IEnumerable<System.IO.FileInfo> fileList = dirInfo.GetFiles();
            System.IO.FileInfo[] files = fileList.Where(f => allowedFileExtensions.Contains(f.Extension.ToLower())).ToArray();
            return files;
        }


        //Get File List With Predefined Extension From Directory ================================================================
        public static System.IO.FileInfo[] GetFileListWithPredefinedExtensionFromDirectory(string targetDirectory, string fileExtension)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(targetDirectory);
            string internalExtension = string.Concat("*.", fileExtension);
            System.IO.FileInfo[] fileInfo = directoryInfo.GetFiles(internalExtension, SearchOption.AllDirectories);
            return fileInfo;
        }

        //PopulateFileListWithPredefinedExtensionFromDirectory2DDL ============================================================
        public static void PopulateFileListWithPredefinedExtensionFromDirectory2DDL(string targetDirectory, string fileExtension, System.Web.UI.WebControls.DropDownList dropdownlist, string InsertText, string InsertValue, string SelectedValue, bool _autopostback)
        {
            System.IO.FileInfo[] fileInfo = GetFileListWithPredefinedExtensionFromDirectory(targetDirectory, fileExtension);
            dropdownlist.Items.Clear();
            ListItemCollection lstCollection = new ListItemCollection();
            foreach (System.IO.FileInfo fileInfoTemp in fileInfo)
            {
                ListItem listItem = new System.Web.UI.WebControls.ListItem(fileInfoTemp.Name, fileInfoTemp.Name);
                lstCollection.Add(listItem);
            }

            dropdownlist.DataSource = lstCollection;
            dropdownlist.DataTextField = "Text";
            dropdownlist.DataValueField = "Value";
            dropdownlist.DataBind();
            if (!string.IsNullOrEmpty(InsertText))
                dropdownlist.Items.Insert(0, new ListItem(InsertText, InsertValue));
            if (!string.IsNullOrEmpty(SelectedValue))
                dropdownlist.SelectedValue = SelectedValue;
            else
                dropdownlist.SelectedIndex = 0;
            dropdownlist.AutoPostBack = _autopostback;
        }

        public static void Copy(Stream input, string targetFile, int length)
        {
            byte[] buffer = new byte[8192];
            using (Stream output = File.OpenWrite(targetFile))
            {
                int bytesRead = 1;
                while (length > 0 && bytesRead > 0)
                {
                    bytesRead = input.Read(buffer, 0, Math.Min(length, buffer.Length));
                    output.Write(buffer, 0, bytesRead);
                    length -= bytesRead;
                }
            }
        }

        public static void CopyFile(string sourceFile, string targetFile)
        {
            if (File.Exists(targetFile))
            {
                File.Delete(targetFile);
            }
            Stream input = File.OpenRead(sourceFile);
            Copy(input, targetFile, (int)input.Length);
            System.GC.Collect();
        }

        #region DOWNLOAD FILE ================================================================================
        public static void DownloadFile(string virtual_file_path)
        {
            string filepath = HttpContext.Current.Server.MapPath(virtual_file_path);
            FileStream fStream = null;
            try
            {
                fStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                byte[] byteBuffer = new byte[(int)fStream.Length];
                fStream.Read(byteBuffer, 0, (int)fStream.Length);

                fStream.Close();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Length", byteBuffer.Length.ToString());
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + virtual_file_path);
                HttpContext.Current.Response.BinaryWrite(byteBuffer);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                if (fStream != null)
                {
                    ex.ToString();
                    fStream.Close();
                    fStream.Dispose();
                }
            }
        }

        public static void DownloadFileByFileInfo(string virtual_file_path)
        {
            //HttpContext.Current.Request.PhysicalApplicationPath
            string physical_file_path = HttpContext.Current.Server.MapPath(virtual_file_path);
            System.IO.FileInfo file_info = new System.IO.FileInfo(physical_file_path);
            if (file_info.Exists)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file_info.Name);
                HttpContext.Current.Response.AddHeader("Content-Length", file_info.Length.ToString());
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.WriteFile(file_info.FullName);
                HttpContext.Current.Response.End();
            }
        }
        #endregion ===========================================================================================

        #region CREATE - UPDATE - DELETE FILE ========================================================================
        public static void CreateFile(string filePath, string strContent)
        {
            try
            {
                StreamWriter write;
                StreamReader s;
                if (System.IO.File.Exists(filePath) == false)
                {
                    write = new StreamWriter(filePath);
                    write.WriteLine(strContent);
                    write.Close();
                }
                else
                {
                    s = File.OpenText(filePath);
                    string line = null;
                    while ((line = s.ReadLine()) != null)
                    {
                        strContent += line + System.Environment.NewLine;
                    }
                    s.Close();
                    write = new StreamWriter(filePath);
                    write.WriteLine(strContent);
                    write.Close();
                }
            }
            catch (Exception e) { e.Message.ToString(); }
        }

        public static string ReadFile(string fileName)
        {
            string Content = "";
            StreamReader s;
            if (System.IO.File.Exists(fileName) == false)
            {
                return "";
            }
            else
            {
                s = File.OpenText(fileName);
                string line = null;
                while ((line = s.ReadLine()) != null)
                {
                    Content += line + System.Environment.NewLine;
                }
                s.Close();
                return Content;
            }
        }

        /// <summary>
        /// Cập nhật nhật nôi dung của file
        /// </summary>
        public static void UpDateFile(string fileName, string newConTent)
        {
            StreamWriter write;
            if (System.IO.File.Exists(fileName) == false)
                System.Console.WriteLine("No Have fileName");
            else
            {
                write = new StreamWriter(fileName);
                write.WriteLine(newConTent);
                write.Close();
            }

        }

        public static string[] GetDirectoryFileInfoFromFilePath(string file_path)
        {
            string[] result = new string[2];
            string filename = string.Empty, direction = string.Empty;

            if (file_path.IndexOf("/") > -1)
            {
                result[0] = file_path.Substring(0, file_path.LastIndexOf("/"));//direction 
                result[1] = file_path.Remove(0, file_path.LastIndexOf("/")).Trim('/');//filename
            }
            else
            {
                result[0] = "";//direction 
                result[1] = file_path;//filename
            }

            return result;
        }

        public static void DeleteFileWithFileNameAndPredefinedPath(string file_name, string dir_path)
        {
            if (file_name != null)
            {
                bool exists = System.IO.Directory.Exists(dir_path);
                if (exists == true)
                {
                    string file_path = System.IO.Path.Combine(dir_path, file_name);
                    if (System.IO.File.Exists(file_path))
                        System.IO.File.Delete(file_path);
                }
            }
        }

        public static void DeleteFileWithPredefinedDatePath(string orginal_file_path, string upload_dir_path)
        {
            string[] result = new string[2];
            result = GetDirectoryFileInfoFromFilePath(orginal_file_path);
            string dir_path = upload_dir_path + "/" + result[0].ToString().Replace("/", "\\");
            string file_name = result[1].ToString();
            if (Directory.Exists(upload_dir_path))
            {
                string file_path = System.IO.Path.Combine(dir_path, file_name);
                if (System.IO.File.Exists(file_path))
                    System.IO.File.Delete(file_path);
            }
        }

        /// <summary>
        /// Deletes files based on a file spec and a given timeout.
        /// This routine is useful for cleaning up temp files in 
        /// Web applications.
        /// </summary>
        /// <param name="filespec">A filespec that includes path and/or wildcards to select files</param>
        /// <param name="seconds">The timeout - if files are older than this timeout they are deleted</param>
        public static void DeleteTimedoutFiles(string filespec, int seconds)
        {
            string path = Path.GetDirectoryName(filespec);
            string spec = Path.GetFileName(filespec);
            string[] files = Directory.GetFiles(path, spec);

            foreach (string file in files)
            {
                try
                {
                    if (File.GetLastWriteTimeUtc(file) < DateTime.UtcNow.AddSeconds(seconds * -1))
                        File.Delete(file);
                }
                catch { }  // ignore locked files
            }
        }
        #endregion DELETE FILE ====================================================================================================

        public static List<System.IO.FileInfo> GetFileListOfFixedPath(string physical_path, string[] extensions, string search_option)
        {
            List<System.IO.FileInfo> list = new List<System.IO.FileInfo>();
            foreach (string ext in extensions)
            {
                if (search_option == "AllDirectories")
                    list.AddRange(new DirectoryInfo(physical_path).GetFiles("*" + ext, SearchOption.AllDirectories).Where(p =>
                          p.Extension.Equals(ext, StringComparison.CurrentCultureIgnoreCase))
                          .ToArray());
                else
                    list.AddRange(new DirectoryInfo(physical_path).GetFiles("*" + ext, SearchOption.TopDirectoryOnly).Where(p =>
                          p.Extension.Equals(ext, StringComparison.CurrentCultureIgnoreCase))
                          .ToArray());
            }
            return list;
        }

        public static int RemoveFilesByExtensionsWithFixedPath(string physical_path, string strFileExtensions, string search_option, string str_alloted_time)
        {
            DateTime current_date = System.DateTime.Now;
            int alloted_time = 0;
            int num_file = 0;
            if (!string.IsNullOrEmpty(str_alloted_time))
                alloted_time = Convert.ToInt32(str_alloted_time);

            //FileInfo[] fileInfo = GetFileListWithPredefinedExtensionFromDirectory(physical_dir_path, file_extension);
            string[] file_extensions = strFileExtensions.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            List<System.IO.FileInfo> lst = GetFileListOfFixedPath(physical_path, file_extensions, search_option);
            if (lst.Count > 0)
            {
                foreach (System.IO.FileInfo fi in lst)
                {
                    System.TimeSpan time_span = current_date.Subtract(fi.CreationTime);
                    if (time_span.Days >= alloted_time)
                    {
                        string file_path = System.IO.Path.Combine(physical_path, fi.Name);
                        if (System.IO.File.Exists(file_path))
                        {
                            System.IO.File.Delete(file_path);
                            num_file++;
                        }
                    }
                }
            }
            return num_file;
        }
        //================================================================================
        //Save a Stream to a File
        // readStream is the stream you need to read
        // writeStream is the stream you want to write to
        private static void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();

            //string saveTo = "some path to save";
            //// create a write stream
            //FileStream writeStream = new FileStream(saveTo, FileMode.Create, FileAccess.Write);
            //// write to the stream
            //ReadWriteStream(readStream, writeStream);
        }

        /// <summary>
        /// Copies the content of the one stream to another.
        /// Streams must be open and stay open.
        /// </summary>
        public static void CopyStream(Stream source, Stream dest, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int read;
            while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                dest.Write(buffer, 0, read);
            }
        }

        /// <summary>
        /// Copies the content of one stream to another by appending to the target stream
        /// Streams must be open when passed in.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <param name="bufferSize"></param>
        /// <param name="append"></param>
        public static void CopyStream(Stream source, Stream dest, int bufferSize, bool append)
        {
            if (append)
                dest.Seek(0, SeekOrigin.End);

            CopyStream(source, dest, bufferSize);
            return;
        }

        #region MIME TYPE REGISTRY =======================================
        
        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
            System.UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            System.UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
            System.UInt32 dwMimeFlags,
            out System.UInt32 ppwzMimeOut,
            System.UInt32 dwReserverd
        );

        private static string GetMimeFromRegistry(string filename)
        {
            string mime = "application/octetstream";
            string ext = System.IO.Path.GetExtension(filename).ToLower();
            RegistryKey rk = Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }
        public static string GetMimeTypeFromFileAndRegistry(string filename)
        {
            if (!File.Exists(filename))
                return GetMimeFromRegistry(filename);

            byte[] buffer = new byte[256];
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                if (fs.Length >= 256)
                    fs.Read(buffer, 0, 256);
                else
                    fs.Read(buffer, 0, (int)fs.Length);
            }

            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                if (string.IsNullOrWhiteSpace(mime) || mime == "text/plain" || mime == "application/octet-stream")
                {
                    return GetMimeFromRegistry(filename);
                }
                return mime;
            }
            catch 
            {
                return GetMimeFromRegistry(filename);
            }
        }

        public static string GetMimeType(FileInfo fileInfo)
        {
            string mimeType = "application/unknown";

            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(fileInfo.Extension.ToLower());

            if (regKey != null)
            {
                object contentType = regKey.GetValue("Content Type");

                if (contentType != null)
                    mimeType = contentType.ToString();
            }

            return mimeType;
        }

        #endregion ====================================================================
    }
}
