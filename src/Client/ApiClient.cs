﻿using Domain.Models;
using Michael.Net.Extensions;
using System.Net.Http.Json;

namespace Client
{
    public class ApiClient
    {
        public HttpClient HttpClient { get; }

        public ApiClient(HttpClient client)
        {
            this.HttpClient = client;
        }

        public Task<Image?> GetImage(long id)
        {
            return HttpClient.GetFromJsonAsync<Image>($"GetImage?id={id}");
        }

        public Task<ImageGroup?> GetImageGroup(long id)
        {
            return HttpClient.GetFromJsonAsync<ImageGroup>($"GetImageGroup?id={id}");
        }

        public async Task<ImageGroup> SaveImageGroup(string imagePath)
        {
            var multipartContent = new MultipartFormDataContent();
            var byteArrayContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
            multipartContent.Add(byteArrayContent, "file", Path.GetFileName(imagePath));

            var response = await HttpClient.PostAsync("SaveImageGroup", multipartContent);
            return await response.FromJson<ImageGroup>();
        }

        public async Task DeleteImageGroup(long id)
        {
            await HttpClient.DeleteAsync($"DeleteImageGroup/{id}");
        }
    }
}
