﻿using Newtonsoft.Json;
using System.Threading.Tasks;
using FirstApp.Core.Models;
using FirstApp.Core.Interfaces;
using System.Json;
using Xamarin.Auth;
using System;
using System.Net.Http;
using Java.Net;
using Java.IO;
using Android.Graphics;
using Android.Util;

namespace FirstApp.Core.Services
{
    public class FacebookService : IFacebookService
    {
        public async Task<FacebookModel> GetUserDataAsync(string accessToken)
        {
            var httpClient = new HttpClient();
            var json = httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,first_name,last_name,id,picture&access_token={accessToken}").Result;
            var user = JsonConvert.DeserializeObject<FacebookModel>(json);
            return user;

        }

        public async Task<string> GetImageFromUrlToBase64(string url)
        {
            String userPhoto = null;
            var httpClient = new HttpClient();
            try
            {
                var uri = new Uri(url);
                var response = httpClient.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        userPhoto = Base64.EncodeToString(imageBytes, Base64Flags.Default);
                    }
                }
            }
            catch (Exception e)
            {
            }

            return userPhoto;
        }
    }
}