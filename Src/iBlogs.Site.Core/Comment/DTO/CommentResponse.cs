using System;
using System.Collections.Generic;
using System.Text;
using iBlogs.Site.Core.Common.Extensions;
using iBlogs.Site.Core.Content;

namespace iBlogs.Site.Core.Comment.DTO
{
    public class CommentResponse
    {

        public int Id { get; set; }

        public bool IsAuthor { get; set; }
        public DateTime Created { get; set; }

        public int Cid { get; set; }

        public Contents Article { get; set; }

        public string Author { get; set; }
        public int OwnerId { get; set; }
        public string Mail { get; set; }
        public string MailPic => Gravatar(Mail);
        public string Url { get; set; }
        public string Ip { get; set; }
        public string Agent { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int Parent { get; set; }

        public int Levels { get; set; }

        public List<CommentResponse> Children { get; set; }

        public string CommentAt(CommentResponse com)
        {
            return "<a href=\"#comment-" + com.Id + "\">@" + com.Author + "</a>";
        }

        private string Gravatar(string email)
        {
            if (email.IsNullOrWhiteSpace())
                return "https://www.Gravatar.com/avatar";
            return $"https://www.Gravatar.com/avatar/{CreateMD5(email).ToLowerInvariant()}?s=60&d=blank";
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}