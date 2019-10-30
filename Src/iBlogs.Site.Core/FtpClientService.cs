using System;
using System.IO;
using System.Net;

namespace iBlogs.Site.Core
{
    public class FtpClientService : IFtpClientService
    {
        private readonly string _host;
        private readonly string _user;
        private readonly string _pass;
        private FtpWebRequest _ftpRequest;
        private FtpWebResponse _ftpResponse;
        private Stream _ftpStream;
        private const int BufferSize = 2048;

        /* Construct Object */
        public FtpClientService(string hostIp, string userName, string password)
        {
            _host = hostIp;
            _user = userName;
            _pass = password;
        }

        /* Download File */
        public void Download(string remoteFile, string localFile)
        {
            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Get the FTP Server's Response Stream */
            _ftpStream = _ftpResponse.GetResponseStream();
            /* Open a File Stream to Write the Downloaded File */
            FileStream localFileStream = new FileStream(localFile, FileMode.Create);
            /* Buffer for the Downloaded Data */
            byte[] byteBuffer = new byte[BufferSize];
            if (_ftpStream != null)
            {
                int bytesRead = _ftpStream.Read(byteBuffer, 0, BufferSize);
                /* Download the File by Writing the Buffered Data Until the Transfer is Complete */

                while (bytesRead > 0)
                {
                    localFileStream.Write(byteBuffer, 0, bytesRead);
                    bytesRead = _ftpStream.Read(byteBuffer, 0, BufferSize);
                }

            }

            /* Resource Cleanup */
            localFileStream.Close();
            _ftpStream?.Close();
            _ftpResponse.Close();
            _ftpRequest = null;

        }

        /* Upload File */
        public void Upload(string remoteFile, string localFile)
        {


            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            /* Establish Return Communication with the FTP Server */
            _ftpStream = _ftpRequest.GetRequestStream();
            /* Open a File Stream to Read the File for Upload */
            FileStream localFileStream = new FileStream(localFile, FileMode.Create);
            /* Buffer for the Downloaded Data */
            byte[] byteBuffer = new byte[BufferSize];
            int bytesSent = localFileStream.Read(byteBuffer, 0, BufferSize);
            /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */


            while (bytesSent != 0)
            {
                _ftpStream.Write(byteBuffer, 0, bytesSent);
                bytesSent = localFileStream.Read(byteBuffer, 0, BufferSize);
            }

            /* Resource Cleanup */
            localFileStream.Close();
            _ftpStream.Close();
            _ftpRequest = null;

        }

        /* Delete File */
        public void Delete(string deleteFile)
        {


            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + deleteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Resource Cleanup */
            _ftpResponse.Close();
            _ftpRequest = null;

        }

        /* Delete Dir*/
        public void DeleteDir(string deleteFile)
        {


            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + deleteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Resource Cleanup */
            _ftpResponse.Close();
            _ftpRequest = null;

        }

        /* Rename File */
        public void Rename(string currentFileNameAndPath, string newFileName)
        {


            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + currentFileNameAndPath);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.Rename;
            /* Rename the File */
            _ftpRequest.RenameTo = newFileName;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Resource Cleanup */
            _ftpResponse.Close();
            _ftpRequest = null;

        }

        /* Create a New Directory on the FTP Server */
        public void CreateDirectory(string newDirectory)
        {


            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + newDirectory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Resource Cleanup */
            _ftpResponse.Close();
            _ftpRequest = null;

        }

        /* Get the Date/Time a File was Created */
        public string GetFileCreatedDateTime(string fileName)
        {
            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            _ftpStream = _ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(_ftpStream ?? throw new InvalidOperationException());
            /* Store the Raw Response */
            /* Read the Full Response Stream */
            var fileInfo = ftpReader.ReadToEnd();

            /* Resource Cleanup */
            ftpReader.Close();
            _ftpStream.Close();
            _ftpResponse.Close();
            _ftpRequest = null;
            /* Return File Created Date Time */
            return fileInfo;
        }

        /* Get the Size of a File */
        public string GetFileSize(string fileName)
        {


            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            _ftpStream = _ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(_ftpStream ?? throw new InvalidOperationException());
            /* Store the Raw Response */
            string fileInfo = null;
            /* Read the Full Response Stream */
            while (ftpReader.Peek() != -1)
            {
                fileInfo = ftpReader.ReadToEnd();
            }

            /* Resource Cleanup */
            ftpReader.Close();
            _ftpStream.Close();
            _ftpResponse.Close();
            _ftpRequest = null;
            /* Return File Size */
            return fileInfo;

        }

        /* List Directory Contents File/Folder Name Only */
        public string[] DirectoryListSimple(string directory)
        {


            /* Create an FTP Request */
            _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
            /* When in doubt, use these options */
            _ftpRequest.UseBinary = true;
            _ftpRequest.UsePassive = true;
            _ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            _ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            /* Establish Return Communication with the FTP Server */
            _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            _ftpStream = _ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(_ftpStream ?? throw new InvalidOperationException());
            /* Store the Raw Response */
            string directoryRaw = null;
            /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
            while (ftpReader.Peek() != -1)
            {
                directoryRaw += ftpReader.ReadLine() + "|";
            }

            /* Resource Cleanup */
            ftpReader.Close();
            _ftpStream.Close();
            _ftpResponse.Close();
            _ftpRequest = null;
            /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */


            if (directoryRaw != null)
            {
                string[] directoryList = directoryRaw.Split("|".ToCharArray());
                return directoryList;
            }

            /* Return an Empty string Array if an Exception Occurs */
            return new[] { "" };
        }

        /* List Directory Contents in Detail (Name, Size, Created, etc.) */
        public string[] DirectoryListDetailed(string directory)
        {

            {
                /* Create an FTP Request */
                _ftpRequest = (FtpWebRequest)WebRequest.Create(_host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                _ftpRequest.Credentials = new NetworkCredential(_user, _pass);
                /* When in doubt, use these options */
                _ftpRequest.UseBinary = true;
                _ftpRequest.UsePassive = true;
                _ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                _ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                /* Establish Return Communication with the FTP Server */
                _ftpResponse = (FtpWebResponse)_ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                _ftpStream = _ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(_ftpStream ?? throw new InvalidOperationException());
                /* Store the Raw Response */
                string directoryRaw = null;
                /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                while (ftpReader.Peek() != -1)
                {
                    directoryRaw += ftpReader.ReadLine() + "|";
                }

                /* Resource Cleanup */
                ftpReader.Close();
                _ftpStream.Close();
                _ftpResponse.Close();
                _ftpRequest = null;
                /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */


                if (directoryRaw != null)
                {
                    string[] directoryList = directoryRaw.Split("|".ToCharArray());
                    return directoryList;
                }



                /* Return an Empty string Array if an Exception Occurs */
                return new[] { "" };
            }
        }
    }
}
