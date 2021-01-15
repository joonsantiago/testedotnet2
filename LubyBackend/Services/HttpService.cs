using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LubyBackend.Services
{
    public class HttpService
    {
        public static Resposta Get(string url)
        {

            Resposta resposta = new Resposta();

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";

                request.Timeout = 2400000;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                resposta = JsonConvert.DeserializeObject<Resposta>(responseFromServer);
                resposta.httpStatusCode = (int)response.StatusCode;
                
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (WebException ex)
            {
                WebExceptionStatus status = ex.Status;

                using (var stream = ex.Response.GetResponseStream())
                using (var _reader = new StreamReader(stream))
                {
                    var r = _reader.ReadToEnd();
                    resposta = JsonConvert.DeserializeObject<Resposta>(r);
                }

                if (status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                    resposta.httpStatusCode = (int)httpResponse.StatusCode;
                }
            }
            catch (Exception e)
            {

                resposta.httpStatusCode = 500;
                resposta.message = e.InnerException.ToString();
                return resposta;
            }

            return resposta;
        }

    }

    public class Resposta
    {
        public int httpStatusCode { get; set; }
        public string message { get; set; }
    }
}
