using System.IO;
using System.Reflection;
using System.Windows;
using Microsoft.Web.WebView2.Wpf;

namespace CurlDebuggerVisualizer
{
    public partial class CurlResponseVisualizerForm : Window
    {
        private readonly string _curl;
        private readonly string _raw;
        private readonly string _json;

        public CurlResponseVisualizerForm(string curlCommand, string rawResponse, string prettyJson)
        {
            InitializeComponent();
            _curl = curlCommand;
            _raw = rawResponse;
            _json = prettyJson;
            Loaded += CurlResponseVisualizerForm_Loaded;
        }

        private async void CurlResponseVisualizerForm_Loaded(object sender, RoutedEventArgs e)
        {
            await Browser.EnsureCoreWebView2Async();

            var assembly = Assembly.GetExecutingAssembly();
            var path = Path.Combine(Path.GetDirectoryName(assembly.Location) ?? string.Empty, "Assets", "template.html");
            var html = File.ReadAllText(path);

            html = html.Replace("{{CURL_COMMAND}}", System.Net.WebUtility.HtmlEncode(_curl))
                       .Replace("{{RAW_RESPONSE}}", System.Net.WebUtility.HtmlEncode(_raw))
                       .Replace("{{PRETTY_JSON}}", System.Net.WebUtility.HtmlEncode(_json));

            Browser.NavigateToString(html);
        }
    }
}
