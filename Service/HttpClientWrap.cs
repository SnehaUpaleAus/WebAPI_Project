using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Nutrien.AXP.FinanceManagement.Services.Tasks.Oic
{
  public interface IHttpClientWrap : IDisposable
  {
    void SetDefaultAuthorization(AuthenticationHeaderValue authenticationHeaderValue);
    void SetDefaultAcceptedMediaTypeHeader(MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValues);
    Task<HttpResponseMessage> GetAsync(string url);
    Task<HttpResponseMessage> GetAsync(string url, CancellationToken token);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token);
    Task<HttpResponseMessage> PostAsync(string url, StringContent content);
    Task<HttpResponseMessage> PostAsync(string url, StringContent content, CancellationToken cancellationToken);
  }

  public class HttpClientWrap : IHttpClientWrap
  {
    private readonly HttpClient httpClient;

    public HttpClientWrap()
    {
      httpClient = new HttpClient();
    }

    public void SetDefaultAuthorization(AuthenticationHeaderValue authenticationHeaderValue)
    {
      httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
    }

    public void SetDefaultAcceptedMediaTypeHeader(MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValues)
    {
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(mediaTypeWithQualityHeaderValues);
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
      return await httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> GetAsync(string url, CancellationToken token)
    {
      return await httpClient.GetAsync(url, token);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
      return await httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
      return await httpClient.SendAsync(request, token);
    }

    public async Task<HttpResponseMessage> PostAsync(string url, StringContent content)
    {
      return await httpClient.PostAsync(url, content);
    }

    public async Task<HttpResponseMessage> PostAsync(string url, StringContent content, CancellationToken cancellationToken)
    {
      return await httpClient.PostAsync(url, content, cancellationToken);
    }

    public void Dispose()
    {
      httpClient?.Dispose();
    }
  }
}