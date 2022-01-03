using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WrapperService.Models;

namespace WrapperService.Controllers
{
    [ApiController]
    [Route("api/[controller]/{countryCode}")]
    public class NewsAppController : ControllerBase
    {
        protected readonly IConfiguration _config;
        [HttpGet]
        public ActionResult<SearchResponseModel> fetchLatestNews(string countryCode)
        {
            //resultsObj lastresp = new resultsObj();
            // responseModel lastresp = new responseModel();
            SearchResponseModel fullresponse = new SearchResponseModel();
            List<resultsObj> resultList = new List<resultsObj>();

            // var result;
            try
            {

                 var client = new RestClient("https://newsdata.io/api/1/news");
                //var client = new RestClient(_config.GetConnectionString("newsApiEndpoint"));
                var request = new RestRequest("?apikey=pub_2865229faf9bf8f0056801d21872022bab81&Country=" + countryCode, Method.GET);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful == true)
                {

                    var resp = response.Content.TrimStart('[').TrimEnd(']');
                    resultsObj newresult = new resultsObj();

                    //var result2 = JsonConvert.DeserializeObject<resultsObj>(resp);
                    //newresult = result2;
                    //resultList.Add(newresult);
                    var result2 = JsonConvert.DeserializeObject<SearchResponseModel>(resp);
                    //fullresponse.status = result2.status;
                    //resultList = fullresponse.results;

                    var result3 = JsonConvert.DeserializeObject<resultsObj>(resp);
                    foreach (var r in result2.results)
                    {
                        newresult = r;
                        resultList.Add(newresult);
                    }

                    //return StatusCode(StatusCodes.Status200OK, new resultsObj()
                    //{
                    //    title = newresult.title,
                    //    link = newresult.link,
                    //    //result3.ForEach((keywords) => newresult.keywords),
                    //    video_url = newresult.video_url,
                    //    description = newresult.description,
                    //    content = newresult.content,
                    //    pubDate = newresult.pubDate,
                    //    image_url = newresult.image_url,
                    //    source_id = newresult.source_id
                    //});

                    //newresult = result3;
                    //resultList.Add(newresult);

                    //resultList.Add(newresult);

                    fullresponse.responseCode = StatusCodes.Status200OK;
                    fullresponse.responseDescription = "Success";
                    fullresponse.results = resultList;
                    fullresponse.status = result2.status;

                    return fullresponse;

                }

                fullresponse.responseCode = StatusCodes.Status400BadRequest;
                fullresponse.responseDescription = "Failed";
                fullresponse.results = resultList;
                fullresponse.status = "";

               

            }
            catch (Exception ex)
            {
                fullresponse.responseCode = StatusCodes.Status417ExpectationFailed;
                fullresponse.responseDescription = "exception";
                fullresponse.results = resultList;
                fullresponse.status = "";
                Console.WriteLine(ex.ToString());
                
            }
            return fullresponse;
        }
    }
}
