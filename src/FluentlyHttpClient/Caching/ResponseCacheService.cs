﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FluentlyHttpClient.Caching
{
	public interface IResponseCacheService
	{
		Task<FluentHttpResponse> Get(string hash, FluentHttpRequest request);
		Task Set(string hash, FluentHttpResponse response);
		bool Matcher(FluentHttpRequest request);
		string GenerateHash(FluentHttpRequest request);
	}

	public class MemoryResponseCacheService : IResponseCacheService
	{
		private readonly Dictionary<string, FluentHttpResponse> _cache = new Dictionary<string, FluentHttpResponse>();

		public async Task<FluentHttpResponse> Get(string hash, FluentHttpRequest request)
		{
			_cache.TryGetValue(hash, out var response);
			if (response == null)
				return null;

			var cloned = await Clone(response);
			return cloned;
		}

		public async Task Set(string hash, FluentHttpResponse response)
		{
			var cloned = await Clone(response);

			_cache[hash] = cloned;
		}

		// todo: make global configurable instead?
		public bool Matcher(FluentHttpRequest request)
		{
			return true;
		}

		public string GenerateHash(FluentHttpRequest request)
		{
			var headers = request.Builder.DefaultHeaders.ToDictionary();
			foreach (var requestHeader in request.Headers)
				headers[requestHeader.Key] = string.Join(";", requestHeader.Value);

			var urlHash = request.Uri.IsAbsoluteUri
				? request.Uri
				: new Uri($"{request.Builder.BaseUrl.TrimEnd('/')}/{request.Uri}");
			var headersHash = headers.ToHeadersHashString();

			var hash = $"method={request.Method};url={urlHash};headers={headersHash}";
			return hash;
		}

		// todo: move to be reusable
		private async Task<FluentHttpResponse> Clone(FluentHttpResponse response)
		{
			var contentString = await response.Content.ReadAsStringAsync();
			var contentType = response.Content.Headers.ContentType;
			var encoding = Encoding.GetEncoding(contentType.CharSet);

			var cloned = new FluentHttpResponse(new HttpResponseMessage(response.StatusCode)
			{
				Content = new StringContent(contentString, encoding, contentType.MediaType),
				ReasonPhrase = response.ReasonPhrase,
				StatusCode = response.StatusCode,
				Version = response.Message.Version,
				RequestMessage = response.Message.RequestMessage
			}, response.Items);

			cloned.Headers.CopyFrom(response.Headers);

			return cloned;
		}

	}


}