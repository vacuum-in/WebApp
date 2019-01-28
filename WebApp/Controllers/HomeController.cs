using System;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            string hostname = System.Environment.MachineName;
            int cpu = System.Environment.ProcessorCount;
            string username = System.Environment.UserName;
            string os = (System.Environment.OSVersion).ToString();
            string domain = System.Environment.UserDomainName;
            string dns = System.Net.Dns.GetHostName();
            ViewBag.hostname = ("Hostname: " + hostname); 
            ViewBag.OS = ("OS Version is " + os);
            ViewBag.ww = (domain + "\\" + username);


            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            
            string[] ips = new string[30];
            var dns1 = System.Net.Dns.GetHostEntry(dns);
            string fqdn = dns1.HostName;
            ViewBag.dns = fqdn;
            int i = 0;
            foreach (var ip in dns1.AddressList)
            {
                ips[i] = ip.ToString();
                i++;
            }
            string filename = "powershell";
            Process proc = new Process();
            proc.StartInfo.FileName = filename;
            proc.StartInfo.Arguments = "-command Get-Location";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            var output = proc.StandardOutput.ReadToEnd();
            ViewBag.outp = output;
            ViewBag.ips = ips;

            return View("MyView");
        }
        [HttpGet]
        public ViewResult SomeTest()
        {
            return View();
        }
        [HttpPost]
        public ViewResult SomeTest(MyModel guestResponse)
        {
            Repository.AddResponse(guestResponse);
            return View("Thanks", guestResponse);
        }
        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(r => r.WillAttend == true));
        }

    }
}
