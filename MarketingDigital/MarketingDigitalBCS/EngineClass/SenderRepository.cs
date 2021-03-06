﻿using MarketingDigitalBCS.EngineClass.Interfaces;
using MarketingDigitalBCS.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MarketingDigitalBCS.EngineClass
{
    public class SenderRepository : ISenderRepository
    {
        public async Task<SBResponse> CreateNewSender(string jsonContent)
        {
            var response = new SBResponse();
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-key", AppConfiguration.SbApiKey);
                HttpResponseMessage request = await client.PostAsync(AppConfiguration.EndPointCreateSender, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
                if (request.IsSuccessStatusCode)
                {
                    respuesta = await request.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<SBResponse>(respuesta);
                    response.exception = false;
                }
                else
                {
                    response.exception = true;
                }
            }

            return response;
        }

        public async Task<SBRecoverSender> GetRecoverSender()
        {
            var response = new SBRecoverSender();
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api-key", AppConfiguration.SbApiKey);
                HttpResponseMessage request = await client.GetAsync(AppConfiguration.EndPointRecoverSender);
                if (request.IsSuccessStatusCode)
                {
                    respuesta = await request.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<SBRecoverSender>(respuesta);
                }
                else
                {
                    response = null;
                }
            }
            return response;
        }
    }
}
