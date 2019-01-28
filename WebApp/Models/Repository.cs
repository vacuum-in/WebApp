using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public static class Repository
    {
        private static List<MyModel> responses = new List<MyModel>();
        public static IEnumerable<MyModel> Responses
        {
            get
            {
                return responses;
            }
        }
        public static void AddResponse (MyModel response)
        {
            responses.Add(response);
        }
    }
}
