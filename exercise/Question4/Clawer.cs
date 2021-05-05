using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using System.Text;

namespace Question4
{
    class Clawer
    {
        private static readonly string RootUri = "http://www.alexa.com/topsites";
        //use HTML Agility Pack Pase Html
        public async Task<string> GetTopUri(int num)
        {
            var client = new HttpClient();
            using (var response = await client.GetAsync(RootUri))
            {
                using (var content = response.Content)
                {
                    var result = await content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(result);
                    var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'AlexaTable')]//div[contains(@class,'tr') and contains(@class,'site-listing')]");
                    if (num > nodes.Count)
                    {
                        throw new Exception($"Query Index is bigger than table index{nodes.Count}");
                    }
                    var find = nodes.ElementAt(num - 1);

                    var url = find.SelectSingleNode("./div[contains(@class,'td') and contains(@class,'DescriptionCell')]/p/a").InnerText;

                    return url;
                }
            }

            return "";
        }





        public async Task<string> GetCountryTopUri(string country, int max)
        {
            var countryDefined = await GetCountryDefined();
            if (!countryDefined.ContainsKey(country))
            {
                throw new Exception($"Doesn't exist country '{country}'");

            }

            var stringBuilder = new StringBuilder();
var a=$"{RootUri}/{countryDefined[country]}";
            var client = new HttpClient();
            var count=0;
            using (var response = await client.GetAsync($"{RootUri}/{countryDefined[country]}"))
            {
                using (var content = response.Content)
                {
                    var result = await content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(result);
                    var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'AlexaTable')]//div[contains(@class,'tr') and contains(@class,'site-listing')]");
                    foreach (var item in nodes)
                    {
                        var url = item.SelectSingleNode("./div[contains(@class,'td') and contains(@class,'DescriptionCell')]/p/a").InnerText;
                        if (count>=max){return stringBuilder.ToString();}
                        stringBuilder.AppendLine(url);
                        count+=1;

                    }

                }
            }
            
            return stringBuilder.ToString();

        }



        private async Task<Dictionary<string, string>> GetCountryDefined()
        {
            var result = new Dictionary<string, string>();



            var client = new HttpClient();
            using (var response = await client.GetAsync(RootUri + "/countries"))
            {
                using (var content = response.Content)
                {
                    var resultString = await content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(resultString);
                    var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class,'AlexaTable')]//li/a");
                    foreach (var node in nodes)
                    {
                        result.Add(node.FirstChild.InnerText.Replace(" ",""), node.Attributes["href"].Value);
                    }
                }
            }

            return result;
        }



    }
}
