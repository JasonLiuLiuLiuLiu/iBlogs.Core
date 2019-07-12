using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using iBlogs.Site.Core.Git;
using iBlogs.Site.Core.Option;
using iBlogs.Site.Core.Option.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace iBlogs.Site.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GitController : ControllerBase
    {
        private const string Sha1Prefix = "sha1=";
        private readonly IOptionService _optionService;
        private readonly ILogger<GitController> _logger;
        private readonly IGitEventBus _gitEventBus;

        public GitController(IOptionService optionService, ILogger<GitController> logger, IGitEventBus gitEventBus)
        {
            _optionService = optionService;
            _logger = logger;
            _gitEventBus = gitEventBus;
        }

        public async Task<IActionResult> Index()
        {
            Request.Headers.TryGetValue("X-GitHub-Event", out StringValues eventName);
            Request.Headers.TryGetValue("X-Hub-Signature", out StringValues signature);
            Request.Headers.TryGetValue("X-GitHub-Delivery", out StringValues delivery);

            _logger.LogInformation($"event name:{eventName}");
            _logger.LogInformation($"signature:{signature}");
            _logger.LogInformation($"delivery:{delivery}");

            using (var reader = new StreamReader(Request.Body))
            {
                var txt = await reader.ReadToEndAsync();

                _logger.LogInformation($"message:{txt}");

                if (IsGithubPushAllowed(txt, eventName, signature))
                {
                    _gitEventBus.Publish(txt);
                    return Ok();
                }
            }

            return Unauthorized();
        }

        private bool IsGithubPushAllowed(string payload, string eventName, string signatureWithPrefix)
        {
            if (string.IsNullOrWhiteSpace(payload))
            {
                throw new ArgumentNullException(nameof(payload));
            }
            if (string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentNullException(nameof(eventName));
            }
            if (string.IsNullOrWhiteSpace(signatureWithPrefix))
            {
                throw new ArgumentNullException(nameof(signatureWithPrefix));
            }

            if (signatureWithPrefix.StartsWith(Sha1Prefix, StringComparison.OrdinalIgnoreCase))
            {
                var signature = signatureWithPrefix.Substring(Sha1Prefix.Length);
                var secret = Encoding.ASCII.GetBytes(_optionService.Get(ConfigKey.GithubWebHookSecret, "iblogs"));
                var payloadBytes = Encoding.ASCII.GetBytes(payload);

                using (var hmSha1 = new HMACSHA1(secret))
                {
                    var hash = hmSha1.ComputeHash(payloadBytes);

                    var hashString = ToHexString(hash);

                    if (hashString.Equals(signature))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static string ToHexString(byte[] bytes)
        {
            var builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                builder.AppendFormat("{0:x2}", b);
            }

            return builder.ToString();
        }
    }
}