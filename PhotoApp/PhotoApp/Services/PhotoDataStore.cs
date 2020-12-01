using PhotoApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhotoApp.Services
{
    class PhotoDataStore : IDataStore<Photo>
    {
         List<Photo> photos;
        public const string HostUrl = "https://api.unsplash.com";
        public const string client_id = "jdtlYDWNT6TZ-f08cWgjH88o86zx4cKHr-Dhc-ODSAg";
        private readonly string BaseUrl = $"{HostUrl}/api?client_id={client_id}";
        HttpClient client;

        public PhotoDataStore()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<bool> AddItemAsync(Photo item)
        {
            photos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Photo item)
        {
            var oldItem = photos.Where((Photo arg) => arg.Id == item.Id).FirstOrDefault();
            photos.Remove(oldItem);
            photos.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = photos.Where((Photo arg) => arg.Id == id).FirstOrDefault();
            photos.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Photo> GetItemAsync(string id)
        {
            Photo photo = await Task.FromResult(photos.FirstOrDefault(s => s.Id == id));
            await DownloadImageForPhoto(photo);
            return photo;
         }



        public async Task DownloadImageForPhoto(Photo photo)
        {
            Uri uri;
            photo.Urls.TryGetValue("thumb", out uri);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();

                    photo.MainImage = ImageSource.FromStream(() => new MemoryStream(content));

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

        }

        private string GetUri(Search search)
        {
            string q = "";
            if (search != null)
            {
                q = !string.IsNullOrEmpty(search.Query) ? $"{q}query={search.Query}&" : q;
                q = search.color != Search.Color.def ? $"{q}color={search.color.ToString()}&" : q;
                q = search.orientation!=Search.Orientation.all ? $"{q}orientation={search.orientation.ToString()}&" : q;
            }

            return q;
        }
        public async Task<IEnumerable<Photo>> GetItemsAsync(Search search = null,bool forceRefresh = false)
        {
            var items = new List<Photo>();
            string _uri = GetUri(search);
            Uri uri = string.IsNullOrEmpty(_uri) ?
                new Uri($"{HostUrl}/photos?client_id={client_id}") :
                new Uri($"{HostUrl}/search/photos?{_uri}client_id={client_id}");
           try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    SearchAnswer temp = new SearchAnswer();
                    items = !string.IsNullOrEmpty(_uri) ?  JsonConvert.DeserializeObject<SearchAnswer>(content).Results : JsonConvert.DeserializeObject<List<Photo>>(content); 
                    photos = items;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return await Task.FromResult(items);
        }
    }
}
