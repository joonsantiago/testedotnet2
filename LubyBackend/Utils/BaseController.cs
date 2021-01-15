using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LubyBackend.Utils
{
    public class BaseController : Controller
    {
        IConfiguration configuration;
        public BaseController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ObjectResult CatchError(Exception ex, string action_do)
        {
            var msg = "";
            msg += ex.Message.Contains("inner") ? ex.InnerException.InnerException.Message : ex.Message;
            var log_error = DateTime.Now.ToString("ddMMyyyyHmmss");

            string pathLogs = configuration["paths:logs"];
            string fileName = string.Format("ERRO_#{0}.txt", log_error);
            if (!Directory.Exists(pathLogs))
            {
                Directory.CreateDirectory(pathLogs);
            }
            
            var debug_msg = string.Format("ERROR [{0}][{1}]\n", DateTime.Now.ToString("dd-MM-yyyy H:mm"), action_do); ;
            debug_msg += "StackTrace Mesage :: \n";
            debug_msg += msg + " \n \n";
            debug_msg += "Error Mesage :: \n";
            debug_msg += ex.ToString() + " \n \n";

            string file = string.Format("{0}/{1}", pathLogs, fileName);
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(debug_msg);
            sw.Close();

            return StatusCode(StatusCodes.Status500InternalServerError, new { success = false, data = new { }, messages = string.Format("Inexpected error, contact the suport and informe the code #{0}", log_error) });
        }
    }
}
