using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace CurlDebuggerVisualizer
{
    public static class CurlHelper
    {
        public static string ToCurlCommand(HttpRequestMessage request)
        {
            if (request == null) return string.Empty;

            var builder = new StringBuilder();
            builder.Append("curl");
            builder.Append(" -X ").Append(request.Method.Method);

            foreach (var header in request.Headers)
            {
                foreach (var value in header.Value)
                {
                    builder.Append(" -H \"")
                           .Append(header.Key).Append(": ").Append(value).Append("\"");
                }
            }

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                {
                    foreach (var value in header.Value)
                    {
                        builder.Append(" -H \"")
                               .Append(header.Key).Append(": ").Append(value).Append("\"");
                    }
                }

                var content = request.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(content))
                {
                    builder.Append(" --data \"")
                           .Append(content.Replace("\"", "\\\"") )
                           .Append("\"");
                }
            }

            builder.Append(" \"").Append(request.RequestUri).Append("\"");
            return builder.ToString();
        }

        public static string ToRawResponse(HttpResponseMessage response)
        {
            if (response == null) return string.Empty;

            var sb = new StringBuilder();
            sb.Append("HTTP/").Append(response.Version)
              .Append(' ').Append((int)response.StatusCode)
              .Append(' ').Append(response.ReasonPhrase).AppendLine();

            foreach (var header in response.Headers)
            {
                foreach (var value in header.Value)
                {
                    sb.Append(header.Key).Append(": ").Append(value).AppendLine();
                }
            }
            if (response.Content != null)
            {
                foreach (var header in response.Content.Headers)
                {
                    foreach (var value in header.Value)
                    {
                        sb.Append(header.Key).Append(": ").Append(value).AppendLine();
                    }
                }
                sb.AppendLine();
                sb.Append(response.Content.ReadAsStringAsync().Result);
            }

            return sb.ToString();
        }

        public static string ToPrettyJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return string.Empty;
            try
            {
                var obj = JsonConvert.DeserializeObject(json);
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch
            {
                return json;
            }
        }
    }
}
