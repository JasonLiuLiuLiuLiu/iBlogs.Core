namespace iBlogs.Site.Core
{
    public interface IFtpClientService
    {
        void Download(string remoteFile, string localFile);
        void Upload(string remoteFile, string localFile);
        void Delete(string deleteFile);
        void DeleteDir(string deleteFile);
        void Rename(string currentFileNameAndPath, string newFileName);
        void CreateDirectory(string newDirectory);
        string GetFileCreatedDateTime(string fileName);
        string GetFileSize(string fileName);
        string[] DirectoryListSimple(string directory);
        string[] DirectoryListDetailed(string directory);
    }
}