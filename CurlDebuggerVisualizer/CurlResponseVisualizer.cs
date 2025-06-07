using System.Net.Http;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
    typeof(CurlDebuggerVisualizer.CurlResponseVisualizer),
    typeof(VisualizerObjectSource),
    Target = typeof(HttpResponseMessage),
    Description = "cURL Debugger Visualizer")]

namespace CurlDebuggerVisualizer
{
    public class CurlResponseVisualizer : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            if (windowService == null || objectProvider == null) return;

            HttpResponseMessage response = null;
            if (objectProvider is IVisualizerObjectProvider3 provider3)
            {
                response = provider3.GetObject<HttpResponseMessage>();
            }

            if (response == null) return;

            var request = response.RequestMessage;
            string curl = CurlHelper.ToCurlCommand(request);
            string raw = CurlHelper.ToRawResponse(response);
            string json = string.Empty;

            if (response.Content?.Headers?.ContentType != null &&
                response.Content.Headers.ContentType.MediaType.Contains("application/json"))
            {
                json = CurlHelper.ToPrettyJson(response.Content.ReadAsStringAsync().Result);
            }

            var form = new CurlResponseVisualizerForm(curl, raw, json);
            windowService.ShowDialog(form);
        }

        public static void TestShowVisualizer(HttpResponseMessage response)
        {
            var host = new VisualizerDevelopmentHost(response, typeof(CurlResponseVisualizer));
            host.ShowVisualizer();
        }
    }
}
